using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{

    public GameObject Wizard_skillprefab;
    public GameObject Gandalf_skillprefab;
    public GameObject Queen_skillprefab;
    public GameObject God_skillprefab;


    int Queen_mana=0;
    int King_mana = 0;
    int God_mnna = 0;


    public Transform targetSlime=null;
    Animator animator;
    public BoxCollider MyCollider;
    Vector3 moveDir=Vector3.zero;

    float totaTime;
    public Slime slime;
    public MonsterSpawner Mspw;
    GameObject attack_target;

    //원거리 공격 풀링을 위해 오브젝트를 저장 할 리스트 
    public List<GameObject> wizadAttack = new List<GameObject>();
    public List<GameObject> GandalfAttack = new List<GameObject>();
    public List<GameObject> QueenfAttack = new List<GameObject>();

    AnimationEventReceiver animReceiver = null;
    private void Awake()
    {
        Mspw = FindObjectOfType<MonsterSpawner>();
        animator =GetComponent<Animator>();
        animReceiver = GetComponentInChildren<AnimationEventReceiver>();

        animReceiver.callbackEndEvent = reAttack;
        animReceiver.callbackAttackEvent= onAttack;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.inst.IsGameOver == true) return;

        if (other.tag=="Slime"|| other.tag == "Turtle"|| other.tag == "Orc"|| other.tag == "OrcHero")
        {
            MyCollider.enabled = false;
            attack_target = other.gameObject;

            StartCoroutine(Rotate(other, UnitManager.inst.Attack_Range));
            animator.SetTrigger("Attack");
            

        }
    }

    






    IEnumerator Rotate(Collider other,float Range)
    {
        //다른 other가 들어오면 브레이크 
        
        while(true)
        {
            Vector3 moveDir = (other.transform.position - transform.position).normalized;
            Quaternion from = transform.rotation;
            Quaternion to = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Lerp(from, to, Time.fixedDeltaTime);
            yield return new WaitForSeconds(0.1f*Time.deltaTime);

            if (other== null) yield break;
            
            else if (Vector3.Distance(other.transform.position, transform.position) > UnitManager.inst.Attack_Range)
            {
                yield break;
            }
        }
    }

    public void reAttack()
    {
        MyCollider.enabled = true;
    }

    //애니메이션 이벤트 onAttack 호출 시 실행되는 함수
    public void onAttack()
    {
        if (attack_target == null) return;
        if (MyCollider.enabled == false && gameObject.tag == "dogKnight")
        {
            MyCollider.enabled = true;
            attack_target.SendMessage("transHp", UnitManager.inst.Dog_Damage, SendMessageOptions.DontRequireReceiver);
        }
        if (MyCollider.enabled == false && gameObject.tag == "Hero")
        {
            MyCollider.enabled = true;
            attack_target.SendMessage("transHp", UnitManager.inst.Hero_Damage, SendMessageOptions.DontRequireReceiver);
        }
        if (MyCollider.enabled == false && gameObject.tag == "King")
        {
            King_mana++;
            if(King_mana==10)
            {
                King_mana = 0;
                animator.SetTrigger("ManaSkill");
                MonsterManager.inst.OnKingSkill();
                return;
            }


            MyCollider.enabled = true;
            attack_target.SendMessage("transHp", UnitManager.inst.King_Damage, SendMessageOptions.DontRequireReceiver);
        }
        if (MyCollider.enabled == false && gameObject.tag == "Wizard")
        {
            MyCollider.enabled = true;
            {
                if (wizadAttack.Count == 0)
                {
                    GameObject Eenergeball = new GameObject();
                    Eenergeball = Instantiate(Wizard_skillprefab, transform.position, Quaternion.identity);
                    StartCoroutine(WizardAttack(attack_target, Eenergeball));
                }
                else
                {
                    for (int i = 0; i < wizadAttack.Count; i++)
                    {
                        wizadAttack[i].SetActive(true);
                        wizadAttack[i].transform.position = transform.position;
                        StartCoroutine(WizardAttack(attack_target, wizadAttack[i]));
                        wizadAttack.Remove(wizadAttack[i]);
                    }
                }

            }

        }
        if (MyCollider.enabled == false && gameObject.tag == "Gandalf")
        {
            MyCollider.enabled = true;
            {
                if (GandalfAttack.Count == 0)
                {
                    GameObject Eenergeball = new GameObject();
                    Eenergeball = Instantiate(Gandalf_skillprefab, transform.position+new Vector3(0,5f,0), Quaternion.identity);
                    StartCoroutine(Gandalf_Attack(attack_target, Eenergeball));
                }
                else
                {
                    for (int i = 0; i < GandalfAttack.Count; i++)
                    {
                        GandalfAttack[i].SetActive(true);
                        GandalfAttack[i].transform.position = transform.position + new Vector3(0, 5f, 0);   
                        StartCoroutine(Gandalf_Attack(attack_target, GandalfAttack[i]));
                        GandalfAttack.Remove(GandalfAttack[i]);
                    }
                }

            }

        }
        if (MyCollider.enabled == false && gameObject.tag == "Queen")
        {
            MyCollider.enabled = true;
            {
                if (QueenfAttack.Count == 0)
                {
                    GameObject Eenergeball = new GameObject();
                    Eenergeball = Instantiate(Queen_skillprefab, transform.position, Quaternion.identity);
                    StartCoroutine(Queen_Attack(attack_target, Eenergeball));
                }
                else
                {
                    for (int i = 0; i < QueenfAttack.Count; i++)
                    {
                        QueenfAttack[i].SetActive(true);
                        QueenfAttack[i].transform.position = transform.position;
                        StartCoroutine(Queen_Attack(attack_target, QueenfAttack[i]));
                        QueenfAttack.Remove(QueenfAttack[i]);
                    }
                }

            }

        }
        if(MyCollider.enabled == false && gameObject.tag == "God")
        {
            God_mnna++;
            if (God_mnna == 5)
            {
                God_mnna = 0;
                MonsterManager.inst.OnGodSkill();
                StartCoroutine(onGodSkill());
                return;
            }
            MyCollider.enabled = true;
            MonsterManager.inst.OnGodmanadrane();
            attack_target.SendMessage("transHp", 0f, SendMessageOptions.DontRequireReceiver);
        }


    }

    //Wizard 공격 코루틴 
    IEnumerator WizardAttack(GameObject taget, GameObject attack)
    {
        while (true)
        {
            if (taget == null)
            {
                wizadAttack.Add(attack);
                attack.SetActive(false);
                yield break;
            }
                
            if (Vector3.Distance(taget.transform.position, attack.transform.position) <= 0.2f)
            {
                taget.SendMessage("transHp", UnitManager.inst.Wizard_Damage, SendMessageOptions.DontRequireReceiver);
                wizadAttack.Add(attack);
                attack.SetActive(false);
                yield break;
            }
            if (Vector3.Distance(taget.transform.position, attack.transform.position) > 25f)
            {
                wizadAttack.Add(attack);
                attack.SetActive(false);
                yield break;
            }

            Vector3 moveDir = (taget.transform.position - attack.transform.position).normalized;
            Quaternion to = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, to, Time.fixedDeltaTime);

            attack.transform.rotation = Quaternion.LookRotation(moveDir);
            attack.transform.Translate(Vector3.forward * 10f * Time.deltaTime);

            yield return null;
        }

    }

    IEnumerator Gandalf_Attack(GameObject taget, GameObject attack)
    {
        while (true)
        {
            if (taget == null)
            {
                wizadAttack.Add(attack);
                attack.SetActive(false);
                yield break;
            }
            if (Vector3.Distance(taget.transform.position, attack.transform.position) <= 0.2f)
            {
                taget.SendMessage("transHp", UnitManager.inst.Gandalf_Damage, SendMessageOptions.DontRequireReceiver);
                GandalfAttack.Add(attack);
                attack.SetActive(false);

                yield break;
            }
            if (Vector3.Distance(taget.transform.position, attack.transform.position) > 25f)
            {
                wizadAttack.Add(attack);
                attack.SetActive(false);
                yield break;
            }
            Vector3 moveDir = (taget.transform.position - attack.transform.position).normalized;
            Quaternion to = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, to, Time.fixedDeltaTime);

            attack.transform.rotation = Quaternion.LookRotation(moveDir);
            attack.transform.Translate(Vector3.forward * 10f * Time.deltaTime);

            yield return null;
        }

    }

    IEnumerator Queen_Attack(GameObject taget, GameObject attack)
    {
        while (true)
        {
            if (taget == null)
            {
                QueenfAttack.Add(attack);
                attack.SetActive(false);
                yield break;
            }
            if (Vector3.Distance(taget.transform.position, attack.transform.position) > 25f)
            {
                QueenfAttack.Add(attack);
                attack.SetActive(false);
                yield break;
            }
            if (Vector3.Distance(taget.transform.position, attack.transform.position) <= 0.2f)
            {
                taget.SendMessage("transHp", UnitManager.inst.Queen_Damage, SendMessageOptions.DontRequireReceiver);
                QueenfAttack.Add(attack);
                attack.SetActive(false);

                Queen_mana++;
                if(Queen_mana == 5)
                {
                    Queen_mana = 0;
                    animator.SetTrigger("ManaSkill");
                    MonsterManager.inst.OnQueenSkill();
                    //taget.SendMessage("OnQueenSkill", SendMessageOptions.DontRequireReceiver);
                    yield break;
                }
                
                if (Vector3.Distance(taget.transform.position, attack.transform.position) > 25f)
                {
                    QueenfAttack.Add(attack);
                    attack.SetActive(false);
                    yield break;
                }

                yield break;
            }

            Vector3 moveDir = (taget.transform.position - attack.transform.position).normalized;
            Quaternion to = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, to, Time.fixedDeltaTime);

            attack.transform.rotation = Quaternion.LookRotation(moveDir);
            attack.transform.Translate(Vector3.forward * 10f * Time.deltaTime);

            yield return null;
        }

    }

    IEnumerator onGodSkill()
    {
        God_skillprefab.SetActive(true);
        yield return new WaitForSeconds(6.5f);
        God_skillprefab.SetActive(false);
    }



}
