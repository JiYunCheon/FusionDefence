using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    RtsUnitController rts;
    private void Awake()
    {
        rts = GetComponent<RtsUnitController>();
    }


    public void Onclick()
    {
        SceneManager.LoadScene("Main");
    }

    public void Onclick_Start()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void Onclick_Info()
    {
        SceneManager.LoadScene("Info");
    }

    public void MakeHero()
    {
        rts.Fusion();
    }

}
