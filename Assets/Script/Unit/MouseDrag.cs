using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    [SerializeField]
    //pivot�� ��ũ���� transform
    private RectTransform dragRectAngle;
    private Rect dragRect;
    private Vector2 start= Vector2.zero;
    private Vector2 end= Vector2.zero;

    private Camera mainCamera;
    private RtsUnitController rtsUnitController;
    private UnitSpawner unitSpawner;

    private void Awake()
    {
        mainCamera=Camera.main;
        rtsUnitController = GetComponent<RtsUnitController>();
        unitSpawner = FindObjectOfType<UnitSpawner>();
        DrawDragRectangle();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.inst.IsGameOver == true) return;

        if (Input.GetMouseButtonDown(0))
        {
            start=Input.mousePosition;
            dragRect = new Rect();
        }
        if(Input.GetMouseButton(0))
        {
            end=Input.mousePosition;
            //���콺�� Ŭ���� ���·� �巡�� �ϴ� ���� �巡�� ������ �̹����� ǥ��
            DrawDragRectangle();
        }
        if(Input.GetMouseButtonUp(0))
        {
            //���콺 Ŭ���� ������ �� �巡�� ���� ���� �ִ� ���� ����
            CalculateDragRect();
            SelecDog();
            SelecHero();
            SelecKing();
            SelecWizard();
            SelecGandalf();
            SelecQueen();
            SelecGod();

            //���콺 Ŭ���� ������ �� �巡�� ������ ������ �ʵ��� ��ġ�� �ʱ�ȭ
            start = end = Vector2.zero;
            DrawDragRectangle();
        }
    }

    void DrawDragRectangle()
    {
        
        //�巡�� ������ ��ġ
        dragRectAngle.position = (start + end) * 0.5f;
        //�巡�� ������ ��Ÿ���� �̹����� ũ�� 
        //��Ʈ Ʈ�������� ����� �����Ϸ��� sizeDelta�� ���
        dragRectAngle.sizeDelta=new Vector2(Mathf.Abs(start.x-end.x),Mathf.Abs(start.y-end.y));

    }

    void CalculateDragRect()
    {
        if(Input.mousePosition.x<start.x)
        {
            dragRect.xMin=Input.mousePosition.x;
            dragRect.xMax = start.x;
        }
        else
        {
            dragRect.xMin = start.x;
            dragRect.xMax=Input.mousePosition.x;
        }

        if(Input.mousePosition.y<start.y)
        {
            dragRect.yMin = Input.mousePosition.y;
            dragRect.yMax = start.y;
        }
        else
        {
            dragRect.yMin = start.y;
            dragRect.yMax = Input.mousePosition.y;
        }

    }
    void SelecDog()
    {
        //��ũ�� �� �ִ� ������ ã�� ���� 
        foreach(UnitController unit in rtsUnitController.unitList)
        {
            if(dragRect.Contains(mainCamera.WorldToScreenPoint(unit.transform.position)))
            {
                rtsUnitController.DragSelectUnit(unit);
            }
        }
        

    }
    void SelecHero()
    {
        foreach (UnitController hero in rtsUnitController.heroList)
        {
            if (dragRect.Contains(mainCamera.WorldToScreenPoint(hero.transform.position)))
            {
                rtsUnitController.DragSelectUnit(hero);
            }
        }


    }

    void SelecKing()
    {
        foreach (UnitController king in rtsUnitController.KingList)
        {
            if (dragRect.Contains(mainCamera.WorldToScreenPoint(king.transform.position)))
            {
                rtsUnitController.DragSelectUnit(king);
            }
        }
    }
    void SelecWizard()
    {
        foreach (UnitController Wizard in rtsUnitController.WizardList)
        {
            if (dragRect.Contains(mainCamera.WorldToScreenPoint(Wizard.transform.position)))
            {
                rtsUnitController.DragSelectUnit(Wizard);
            }
        }
    }
    void SelecGandalf()
    {
        foreach (UnitController Gandalf in rtsUnitController.GandalfList)
        {
            if (dragRect.Contains(mainCamera.WorldToScreenPoint(Gandalf.transform.position)))
            {
                rtsUnitController.DragSelectUnit(Gandalf);
            }
        }
    }
    void SelecQueen()
    {
        foreach (UnitController Queen in rtsUnitController.QueenList)
        {
            if (dragRect.Contains(mainCamera.WorldToScreenPoint(Queen.transform.position)))
            {
                rtsUnitController.DragSelectUnit(Queen);
            }
        }
    }
    void SelecGod()
    {
        foreach (UnitController God in rtsUnitController.GodList)
        {
            if (dragRect.Contains(mainCamera.WorldToScreenPoint(God.transform.position)))
            {
                rtsUnitController.DragSelectUnit(God);
            }
        }
    }

}
