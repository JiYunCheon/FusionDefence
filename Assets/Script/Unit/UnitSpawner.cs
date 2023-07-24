using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject unitPrefab;
    [SerializeField]
    private GameObject HeroPrefab;
    [SerializeField]
    private GameObject KingPrefab;
    [SerializeField]
    private GameObject WizardPrefab;
    [SerializeField]
    private GameObject GandalfPrefab;
    [SerializeField]
    private GameObject QueenPrefab;
    [SerializeField]
    private GameObject GodPrefab;

    //������ ���� ��Ű�鼭 �ű� ��
    public Transform TR;


    public List<GameObject> dogs=new List<GameObject>();
    public List<GameObject> Heros = new List<GameObject>();
    public List<GameObject> Kings = new List<GameObject>();
    public List<GameObject> Wizards = new List<GameObject>();
    public List<GameObject> Gandalfs = new List<GameObject>();
    public List<GameObject> Queens = new List<GameObject>();
    public List<GameObject> God = new List<GameObject>();

    //���� ��Ʈ�ѷ��� �������ִ� �������� �̸� ���� �� �޼���
    public List<UnitController> SpawnDog()
    {
        List<UnitController> unitList = new List<UnitController>();

        for (int i = 0; i < 20; i++)
        {
            GameObject clone = Instantiate(unitPrefab, TR.transform.position, Quaternion.identity);
            UnitController unit = clone.GetComponent<UnitController>();
            //������ ���� �� ����� ���� �Ǻ� �ϱ� ���� �ʿ��� ����Ʈ 
            dogs.Add(clone);
            //Ŭ���� �巡�׷� ������ ������ �� ������Ʈ�� �Ǻ� �� ����Ʈ
            unitList.Add(unit);
        }

        return unitList;
    }

    public List<UnitController> SpawnHero()
    {
        List<UnitController> HeroList = new List<UnitController>();

        for (int i = 0; i < 10; i++)
        {
            GameObject clone = Instantiate(HeroPrefab, TR.transform.position, Quaternion.identity);
            UnitController unit = clone.GetComponent<UnitController>();
            Heros.Add(clone);
            HeroList.Add(unit);
        }

        return HeroList;
    }

    public List<UnitController> SpawnKing()
    {
        List<UnitController> KingList = new List<UnitController>();

        for (int i = 0; i < 10; i++)
        {
            GameObject clone = Instantiate(KingPrefab, TR.transform.position, Quaternion.identity);
            UnitController unit = clone.GetComponent<UnitController>();
            Kings.Add(clone);
            KingList.Add(unit);
        }

        return KingList;
    }

    public List<UnitController> SpawnWizard()
    {
        List<UnitController> WizardList = new List<UnitController>();

        for (int i = 0; i < 20; i++)
        {
            GameObject clone = Instantiate(WizardPrefab, TR.transform.position, Quaternion.identity);
            UnitController unit = clone.GetComponent<UnitController>();
            Wizards.Add(clone);
            WizardList.Add(unit);
        }

        return WizardList;
    }

    public List<UnitController> SpawnGandalf()
    {
        List<UnitController> GandalfList = new List<UnitController>();

        for (int i = 0; i < 10; i++)
        {
            GameObject clone = Instantiate(GandalfPrefab, TR.transform.position, Quaternion.identity);
            UnitController unit = clone.GetComponent<UnitController>();
            Gandalfs.Add(clone);
            GandalfList.Add(unit);
        }

        return GandalfList;
    }

    public List<UnitController> SpawnQueen()
    {
        List<UnitController> QueenList = new List<UnitController>();

        for (int i = 0; i < 10; i++)
        {
            GameObject clone = Instantiate(QueenPrefab, TR.transform.position, Quaternion.identity);
            UnitController unit = clone.GetComponent<UnitController>();
            Queens.Add(clone);
            QueenList.Add(unit);
        }
        return QueenList;
    }

    public List<UnitController> SpawnGod()
    {
        List<UnitController> GodList = new List<UnitController>();

        for (int i = 0; i < 1; i++)
        {
            GameObject clone = Instantiate(GodPrefab, TR.transform.position, Quaternion.identity);
            UnitController unit = clone.GetComponent<UnitController>();
            God.Add(clone);
            GodList.Add(unit);
        }
        return GodList;
    }

}
