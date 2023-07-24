using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [SerializeField]
    private GameObject unitMarker;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private MouseClick mouseClick;

    bool IsUnit_Move = false;
    Vector3 Destination= Vector3.zero;

    public Transform DeathZone;

    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator= GetComponent<Animator>();
        mouseClick=FindObjectOfType<MouseClick>();
    }

    private void Update()
    {
        if (GameManager.inst.IsGameOver)
        {
            gameObject.SetActive(false);
            return;
        }

        if (IsUnit_Move)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
        if (Vector3.Distance(transform.position, Destination) <= 0.5f)
        {
            IsUnit_Move = false;
            animator.SetBool("Move", false);
        }
    }


    public void SelectUnit()
    {
        unitMarker.SetActive(true);
    }
    public void DeSelectUnit()
    {
        unitMarker.SetActive(false);
    }
    public void MoveTO(Vector3 end)
    {
        IsUnit_Move = true;
        Destination = end;
        navMeshAgent.SetDestination(end);
    }


   


}
