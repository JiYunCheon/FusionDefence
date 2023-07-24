using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static UnitManager instance = null;
    public static UnitManager inst
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UnitManager>();
                if (instance == null)
                {
                    instance = new GameObject("UnitManager").AddComponent<UnitManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    public float Attack_Range=3f;
    public float Attac_Interval = 0.2f;
    public float attackPower = 1f;

    public int Dog_Damage = 1;
    public int Hero_Damage = 2;
    public int Wizard_Damage = 1;
    public int Gandalf_Damage = 2;

    public int Queen_Damage = 1;
    public int Queen_Skill_Damage = 2;

    public int King_Damage = 3;
    public int King_Skill_Damage = 1;
    public int god_Skill_Damage = 2;


    public bool OnDogFusion=false;

    


    private void Awake()
    {
       
    }


    private void Update()
    {
        

    }

}
