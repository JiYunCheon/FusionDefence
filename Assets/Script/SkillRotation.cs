using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRotation : MonoBehaviour
{
    

    void Update()
    {
        transform.Rotate(100f * Time.deltaTime, 0, 0);
    }

   

}
