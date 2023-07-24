using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Uimanager : MonoBehaviour
{
    [Header("텍스트")]
    public Text Wave;
    public Text Monster_Count;
    public Text time;
    public Text maxUnit;
    public Text gameOver;
    public Text imcomming_boss;

    [Header("이미지")]    
    public Image Hero;
    public Image Gandalf;
    public Image Queen;
    public Image King;
    public Image God;
    


    RtsUnitController rts;
    void Start()
    {
        rts=FindObjectOfType<RtsUnitController>();
    }

    void Update()
    {
        

        GameManager.inst.totalTime += Time.deltaTime;
        

        time.text = "Time : " + (int)GameManager.inst.totalTime;

        Monster_Count.text="Monster Count : " + GameManager.inst.Monster_Count;
        Wave.text = "Wave : " + GameManager.inst.Wave_Level;

        if(GameManager.inst.max_Unit==true)
        {
            Debug.Log(GameManager.inst.max_Unit);
            GameManager.inst.max_Unit = false;
            StartCoroutine(max_unit());
        }
        if(GameManager.inst.incomming_boss==true)
        {
            StartCoroutine(bosstime());
            GameManager.inst.incomming_boss=false;
        }
        if(GameManager.inst.IsGameClear==true)
        {
            SceneManager.LoadScene("GameClear");
        }
        if (GameManager.inst.IsGameOver == true)
        {
            gameOver.gameObject.SetActive(true);
            gameObject.SetActive(false);
            return;
        }
        


    }

    IEnumerator max_unit()
    {
        maxUnit.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        maxUnit.gameObject.SetActive(false);
    }
    IEnumerator bosstime()
    {
        imcomming_boss.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        imcomming_boss.gameObject.SetActive(false);
    }
    
    public void OnFusion()
    {
        if (rts.selectedUnitList.Count == 2)
        {
            if (rts.selectedUnitList[0].tag == "dogKnight" && rts.selectedUnitList[1].tag == "dogKnight")
            {
                Hero.gameObject.SetActive(true);
                rts.Fusion();
                return;
            }
            if (rts.selectedUnitList[0].tag == "Hero" && rts.selectedUnitList[1].tag == "Hero")
            {
                King.gameObject.SetActive(true);
                rts.Fusion();
                return;
            }
            if (rts.selectedUnitList[0].tag == "Wizard" && rts.selectedUnitList[1].tag == "Wizard")
            {
                Gandalf.gameObject.SetActive(true);
                rts.Fusion();
                return;
            }
            if (rts.selectedUnitList[0].tag == "Gandalf" && rts.selectedUnitList[1].tag == "Gandalf")
            {
                Queen.gameObject.SetActive(true);
                rts.Fusion();
                return;
            }
            if (rts.selectedUnitList[0].tag == "King" && rts.selectedUnitList[1].tag == "Queen" ||
                rts.selectedUnitList[0].tag == "Queen" && rts.selectedUnitList[1].tag == "King")
            {
                God.gameObject.SetActive(true);
                rts.Fusion();
                return;
            }

        }
        

    }
    public void Image_delete()
    {
        Hero.gameObject.SetActive(false);
        King.gameObject.SetActive(false);
        Queen.gameObject.SetActive(false);
        Gandalf.gameObject.SetActive(false);
        God.gameObject.SetActive(false);
    }


}
