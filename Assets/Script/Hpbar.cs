using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hpbar : MonoBehaviour
{
    [SerializeField] GameObject Hp_prefab = null;

    Camera mainCamera=null;
    GameObject hpbar_TR = null;
    void Start()
    {
        mainCamera = Camera.main;
        hpbar_TR = Instantiate(Hp_prefab,transform.position, Quaternion.identity, transform);
        
    }

    void Update()
    {
        hpbar_TR.transform.position = mainCamera.WorldToScreenPoint(transform.position + new Vector3(0, 1.15f, 0));
    }
}
