using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orc : MonoBehaviour
{
    int Dir; //방향
    int Hp;
    public float curHp;

    public Image healthBar;
    public Transform healthBar_Rotation;
    public GameObject QueenSkill_prefab;
    public GameObject KingSkill_prefab;

    public GameObject Manadrane;
    GameObject GOD;

    private void Awake()
    {
        MonsterManager.inst.queen_skill += OnQueenSkill;
        MonsterManager.inst.king_skill += OnKingSkill;
        MonsterManager.inst.god_skill += OnGodSkill;
        MonsterManager.inst.drane += manadrane;

        
        //방향은 0으로 시작 
        Dir = 0;
       
    }

    private void OnEnable()
    {
        if (gameObject.tag == "Orc")
        {
            Hp = MonsterManager.inst.Orc_Hp;
        }
        else if (gameObject.tag == "OrcHero")
        {
            Hp = MonsterManager.inst.OrcHero_Hp;
        }

        curHp = Hp;
        //생성 될 때 
        healthBar.fillAmount = curHp / Hp;
        gameObject.transform.position = new Vector3(-9f, 0f, 10);
        gameObject.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        MonsterManager.inst.Orc_Speed = 0.5f;
        QueenSkill_prefab.gameObject.SetActive(false);
        KingSkill_prefab.gameObject.SetActive(false);

    }
    private void OnDisable()
    {
        Dir = 0;
        if (curHp <= 0)
        {
            MonsterManager.inst.queen_skill -= OnQueenSkill;
            MonsterManager.inst.king_skill -= OnKingSkill;
            MonsterManager.inst.king_skill -= OnGodSkill;
            MonsterManager.inst.drane -= manadrane;
            MonsterManager.inst.Orc_Speed = 0.5f;
        }
    }
   

    void Update()
    {
        if (GameManager.inst.IsGameOver)
        {
            gameObject.SetActive(false);
            return;
        }
        #region [오크 방향전환]
        if (Dir == 0) Down(gameObject);
        else if (Dir == 1) Right(gameObject);
        else if (Dir == 2) Up(gameObject);
        else if (Dir == 3) Left(gameObject);
        #endregion
    }

    #region [오크 이동]
    public void Down(GameObject Monseter)
    {
        Dir = 0;
        Monseter.transform.rotation = Quaternion.Euler(0, 180f, 0);
        healthBar_Rotation.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (Monseter.transform.position.z <= -8f)
        {

            Dir = 1;

            return;
        }
        Monseter.transform.Translate(0, 0, MonsterManager.inst.Orc_Speed * Time.deltaTime);

    }
    public void Right(GameObject Monseter)
    {
        Dir = 1;
        Monseter.transform.rotation = Quaternion.Euler(0, 90f, 0);
        healthBar_Rotation.transform.rotation = Quaternion.Euler(0, 180f, 0);
        if (Monseter.transform.position.x >= 9f)
        {
            Dir = 2;
            return;
        }

        Monseter.transform.Translate(0, 0, MonsterManager.inst.Orc_Speed * Time.deltaTime);
    }
    public void Up(GameObject Monseter)
    {
        Dir = 2;
        Monseter.transform.rotation = Quaternion.Euler(0, 0f, 0);
        healthBar_Rotation.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (Monseter.transform.position.z >= 9f)
        {
            Dir = 3;
            return;
        }
        Monseter.transform.Translate(0, 0, MonsterManager.inst.Orc_Speed * Time.deltaTime);
    }
    public void Left(GameObject Monseter)
    {
        Dir = 3;
        Monseter.transform.rotation = Quaternion.Euler(0, -90f, 0);
        healthBar_Rotation.transform.rotation = Quaternion.Euler(0, 180f, 0);
        if (Monseter.transform.position.x <= -9f)
        {
            Dir = 0;
            return;
        }
        Monseter.transform.Translate(0, 0, MonsterManager.inst.Orc_Speed * Time.deltaTime);
    }
    #endregion

    void transHp(int damage)
    {
        curHp-=damage;
        healthBar.fillAmount = curHp / Hp;

        if (curHp <= 0)
        {
            GameManager.inst.Monster_Count--;
            //MonsterManager.inst.monList.Enqueue(gameObject);
            //gameObject.SetActive(false);
            if(gameObject.tag=="OrcHero")
            {
                GameManager.inst.IsGameClear = true;
                
            }
            MonsterManager.inst.monList.Add(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnQueenSkill()
    {
        if (curHp <= 0)
            return;
        StartCoroutine(On_Queenskill());
    }

    public IEnumerator On_Queenskill()
    {
        QueenSkill_prefab.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        curHp -= UnitManager.inst.Queen_Skill_Damage;
        healthBar.fillAmount = curHp / Hp;
        if (curHp <= 0)
        {
            GameManager.inst.Monster_Count--;
            MonsterManager.inst.monList.Add(gameObject);
            gameObject.SetActive(false);
            QueenSkill_prefab.gameObject.SetActive(false);
        }
        QueenSkill_prefab.gameObject.SetActive(false);
    }

    private void OnKingSkill()
    {
        if (curHp <= 0)
            return;
        StartCoroutine(On_KingSkill());
    }

    public IEnumerator On_KingSkill()
    {
        KingSkill_prefab.gameObject.SetActive(true);
        MonsterManager.inst.Orc_Speed = 0f;
        yield return new WaitForSeconds(2f);
        curHp -= UnitManager.inst.King_Skill_Damage;
        healthBar.fillAmount = curHp / Hp;
        MonsterManager.inst.Orc_Speed = 0.5f;
        if (curHp <= 0)
        {
            GameManager.inst.Monster_Count--;
            MonsterManager.inst.monList.Add(gameObject);
            gameObject.SetActive(false);
            KingSkill_prefab.gameObject.SetActive(false);
        }
        KingSkill_prefab.gameObject.SetActive(false);
    }

    int god_Power = 0;
    private void OnGodSkill()
    {
        if (curHp <= 0)
            return;
        StartCoroutine(On_GodSkill());
    }

    public IEnumerator On_GodSkill()
    {
        while (true)
        {
            KingSkill_prefab.gameObject.SetActive(true);
            QueenSkill_prefab.gameObject.SetActive(true);
            MonsterManager.inst.Orc_Speed = 0.5f;

            yield return new WaitForSeconds(1f);
            god_Power++;
            curHp -= UnitManager.inst.King_Skill_Damage;
            healthBar.fillAmount = curHp / Hp;
            if (curHp <= 0)
            {
                GameManager.inst.Monster_Count--;
                MonsterManager.inst.monList.Add(gameObject);
                gameObject.SetActive(false);
                KingSkill_prefab.gameObject.SetActive(false);
                QueenSkill_prefab.gameObject.SetActive(false);
            }
            if (god_Power == 10)
            {
                god_Power = 0;
                MonsterManager.inst.Orc_Speed = 3f;
                KingSkill_prefab.gameObject.SetActive(false);
                QueenSkill_prefab.gameObject.SetActive(false);
                yield break;
            }
        }

    }

    void manadrane()
    {
        if (gameObject == null) return;
        GOD = GameObject.FindWithTag("God");
        Manadrane.SetActive(true);
        StartCoroutine(Drane(GOD, Manadrane));
    }

    IEnumerator Drane(GameObject taget, GameObject mana)
    {
        while (true)
        {
            if (taget == null)
            {
                yield break;
            }
            if (Vector3.Distance(taget.transform.position, mana.transform.position) <= 0.2f)
            {
                mana.transform.position = transform.position + new Vector3(0, 1.2f, 0);
                mana.SetActive(false);
                yield break;
            }

            Vector3 moveDir = (taget.transform.position - mana.transform.position).normalized;
            mana.transform.rotation = Quaternion.LookRotation(moveDir);
            mana.transform.Translate(Vector3.forward * 10f * Time.deltaTime);

            yield return null;
        }

    }

}

