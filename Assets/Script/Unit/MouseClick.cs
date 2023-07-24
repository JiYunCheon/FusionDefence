using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    [SerializeField] private LayerMask layerUnit;
    [SerializeField] private LayerMask layerGroud;

    private Camera mainCamera;
    private RtsUnitController rtsUnitController;

    private void Awake()
    {
        mainCamera=Camera.main;
        rtsUnitController=GetComponent<RtsUnitController>();
    }
    void Update()
    {
       
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out hit,Mathf.Infinity,layerUnit))
            {
                if (hit.transform.GetComponent<UnitController>() == null) return;

                if(Input.GetKey(KeyCode.LeftShift))
                {
                    rtsUnitController.ShiftClickSelectUnit(hit.transform.GetComponent<UnitController>());
                }
                else
                {
                    rtsUnitController.ClickSelectUnit(hit.transform.GetComponent<UnitController>());
                }
            }
            else
            {
                if(Input.GetKey(KeyCode.LeftShift)==false)
                {
                    rtsUnitController.DeSelectAll();
                }
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGroud))
            {
                
                rtsUnitController.MoveSelectedUnits(hit.point);
            }


        }






    }
}
