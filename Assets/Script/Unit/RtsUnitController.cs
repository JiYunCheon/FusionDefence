using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RtsUnitController : MonoBehaviour
{
    [SerializeField]
    private UnitSpawner unitSpawner;
    public List<UnitController> selectedUnitList;
    public List<UnitController> unitList { private set; get; }
    public List<UnitController> heroList { private set; get; }
    public List<UnitController> KingList { private set; get; }
    public List<UnitController> WizardList { private set; get; }
    public List<UnitController> GandalfList { private set; get; }
    public List<UnitController> QueenList { private set; get; }
    public List<UnitController> GodList { private set; get; }

    Uimanager uimgr;

    int random_Unit=0;
    int dog_spawn=0;
    int Wizard_spawn = 0;

    int unit_spawn=1;

    private void Awake()
    {
        selectedUnitList = new List<UnitController>();
        uimgr = FindObjectOfType<Uimanager>();

        //유닛을 미리생성
        unitList = unitSpawner.SpawnDog();
        heroList = unitSpawner.SpawnHero();
        KingList = unitSpawner.SpawnKing();
        WizardList = unitSpawner.SpawnWizard();
        GandalfList = unitSpawner.SpawnGandalf();
        QueenList = unitSpawner.SpawnQueen();
        GodList= unitSpawner.SpawnGod();

        //첫 랜덤 유닛 생성
        random_Unit = Random.Range(0, 2);
        if (random_Unit == 0)
        {
            unitSpawner.dogs[dog_spawn].transform.position = transform.position;
            unitSpawner.Kings[0].transform.position = transform.position;
            unitSpawner.Queens[0].transform.position = transform.position;
            unitSpawner.God[0].transform.position = transform.position;
            next_God = 1;
        }
        else if (random_Unit == 1)
            unitSpawner.Wizards[Wizard_spawn].transform.position = transform.position;


    }

    
    private void Update()
    {
        
        if (selectedUnitList.Count == 2)
        {
            Debug.Log(selectedUnitList.Count);
            uimgr.OnFusion();
        }
        else if(selectedUnitList.Count != 2)
        {
            //최적화 필요
            uimgr.Image_delete();
        }

        if (GameManager.inst.Wave_Level> unit_spawn)
        {
            unit_spawn = GameManager.inst.Wave_Level;// 반복 제한

            if (GameManager.inst.Wave_Level % 1 == 0)//라운드별 스폰 조정
            {
                if (GameManager.inst.Wave_Level == 1) return;
                random_Unit = Random.Range(0, 2);
                if (random_Unit == 0)
                {
                    if (Wizard_spawn == unitSpawner.dogs.Count) return;
                    dog_spawn++;
                    unitSpawner.dogs[dog_spawn].transform.position = transform.position;
                }

                if (random_Unit == 1)
                {
                    if (Wizard_spawn == unitSpawner.Wizards.Count) return;
                    Wizard_spawn++;
                    unitSpawner.Wizards[Wizard_spawn].transform.position = transform.position;
                    
                }

            }
        }


        



    }

    //클릭을 하면 나머지 클릭을 해제 하고 유닛을 선택함
    public void ClickSelectUnit(UnitController newUnit)
    {
        DeSelectAll();

        SelectUnit(newUnit);
    }
    //
    public void ShiftClickSelectUnit(UnitController newUnit)
    {
        if(selectedUnitList.Contains(newUnit))
        {
            DeSelectUnit(newUnit);

        }
        else
        {
            SelectUnit(newUnit);
        }
    }
    //
    public void DragSelectUnit(UnitController newUnit)
    {
        if(selectedUnitList.Contains(newUnit)==false)
        {
            SelectUnit(newUnit);
        }
    }

    public void MoveSelectedUnits(Vector3 end)
    {
       
        for (int i = 0; i < selectedUnitList.Count; i++)
        {
            selectedUnitList[i].MoveTO(end);
        }
    }

    public void DeSelectAll()
    {
        for(int i = 0; i < selectedUnitList.Count; i++)
        {
            selectedUnitList[i].DeSelectUnit();

        }

        selectedUnitList.Clear();
    }

    void SelectUnit(UnitController newUnit)
    {
        //유닛이 선택 되었을 때 호출하는 메소드 
        newUnit.SelectUnit();
        //선택된 유닛을 리스트에 저장 
        selectedUnitList.Add(newUnit);


    }

    void DeSelectUnit(UnitController newUnit)
    {
        newUnit.DeSelectUnit();
        selectedUnitList.Remove(newUnit);
    }


    public Transform Tr;
    int next_Hero=0;
    int next_king = 0;
    int next_Gandalf = 0;
    int next_Queen = 0;
    int next_God = 0;

    int god_spawn_point=0;

    public void Fusion()
    {
        if(selectedUnitList.Count==2)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (selectedUnitList[0].tag == "dogKnight" && selectedUnitList[1].tag == "dogKnight")
                {
                    if (unitSpawner.Heros.Count == next_Hero)
                    {
                        GameManager.inst.max_Unit = true;
                        return;
                    }
                       

                    selectedUnitList[0].transform.position = Tr.position;
                    selectedUnitList[1].transform.position = Tr.position;
                    selectedUnitList[0].GetComponent<NavMeshAgent>().enabled = false;
                    selectedUnitList[0].GetComponent<NavMeshAgent>().enabled = true;
                    selectedUnitList[1].GetComponent<NavMeshAgent>().enabled = false;
                    selectedUnitList[1].GetComponent<NavMeshAgent>().enabled = true;

                    unitSpawner.Heros[next_Hero].transform.position = transform.position;
                    next_Hero++;
                    selectedUnitList.Clear();
                    uimgr.Image_delete();
                    return;
                }
                if (selectedUnitList[0].tag == "Hero" && selectedUnitList[1].tag == "Hero")
                {
                    if (unitSpawner.Kings.Count == next_king)
                    {
                        GameManager.inst.max_Unit = true;
                        return;
                    }

                    selectedUnitList[0].transform.position = Tr.position;
                    selectedUnitList[1].transform.position = Tr.position;
                    selectedUnitList[0].GetComponent<NavMeshAgent>().enabled = false;
                    selectedUnitList[0].GetComponent<NavMeshAgent>().enabled = true;
                    selectedUnitList[1].GetComponent<NavMeshAgent>().enabled = false;
                    selectedUnitList[1].GetComponent<NavMeshAgent>().enabled = true;

                    unitSpawner.Kings[next_king].transform.position = transform.position;
                    next_king++;
                    selectedUnitList.Clear();
                    uimgr.Image_delete();
                    return;

                }

                if (selectedUnitList[0].tag == "Wizard" && selectedUnitList[1].tag == "Wizard")
                {
                    if (unitSpawner.Gandalfs.Count == next_Gandalf)
                    {
                        GameManager.inst.max_Unit = true;
                        return;
                    }

                    selectedUnitList[0].transform.position = Tr.position;
                    selectedUnitList[1].transform.position = Tr.position;
                    selectedUnitList[0].GetComponent<NavMeshAgent>().enabled = false;
                    selectedUnitList[0].GetComponent<NavMeshAgent>().enabled = true;
                    selectedUnitList[1].GetComponent<NavMeshAgent>().enabled = false;
                    selectedUnitList[1].GetComponent<NavMeshAgent>().enabled = true;

                    unitSpawner.Gandalfs[next_Gandalf].transform.position = transform.position;
                    next_Gandalf++;
                    selectedUnitList.Clear();
                    uimgr.Image_delete();
                    return;
                }
                if (selectedUnitList[0].tag == "Gandalf" && selectedUnitList[1].tag == "Gandalf")
                {
                    if (unitSpawner.Queens.Count == next_Queen)
                    {
                        GameManager.inst.max_Unit = true;
                        return;
                    }

                    selectedUnitList[0].transform.position = Tr.position;
                    selectedUnitList[1].transform.position = Tr.position;
                    selectedUnitList[0].GetComponent<NavMeshAgent>().enabled = false;
                    selectedUnitList[0].GetComponent<NavMeshAgent>().enabled = true;
                    selectedUnitList[1].GetComponent<NavMeshAgent>().enabled = false;
                    selectedUnitList[1].GetComponent<NavMeshAgent>().enabled = true;

                    unitSpawner.Queens[next_Queen].transform.position = transform.position;
                    next_Queen++;
                    selectedUnitList.Clear();
                    uimgr.Image_delete();
                    return;
                }
                if (selectedUnitList[0].tag == "King" && selectedUnitList[1].tag == "Queen" ||
                selectedUnitList[0].tag == "Queen" && selectedUnitList[1].tag == "King")
                {
                    if (unitSpawner.God.Count == next_God)
                    {
                        GameManager.inst.max_Unit = true;
                        return;
                    }

                    selectedUnitList[0].transform.position = Tr.position;
                    selectedUnitList[1].transform.position = Tr.position;
                    selectedUnitList[0].GetComponent<NavMeshAgent>().enabled = false;
                    selectedUnitList[0].GetComponent<NavMeshAgent>().enabled = true;
                    selectedUnitList[1].GetComponent<NavMeshAgent>().enabled = false;
                    selectedUnitList[1].GetComponent<NavMeshAgent>().enabled = true;

                    unitSpawner.God[next_God].transform.position = transform.position;
                    next_God++;
                    selectedUnitList.Clear();
                    uimgr.Image_delete();
                    return;
                }

            }

        }

       

    }




}
