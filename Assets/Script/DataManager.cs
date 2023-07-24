using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    Dog_Kngiht, Orc
}

public class DataManager : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static DataManager instance = null;
    public static DataManager inst
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataManager>();
                if (instance == null)
                {
                    instance = new GameObject("DataManager").AddComponent<DataManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    public Character Current_Character;

}
