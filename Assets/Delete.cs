using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    
    void Update()
    {
        if (GameManager.inst.IsGameOver)
        {
            gameObject.SetActive(false);
            return;
        }
    }
}
