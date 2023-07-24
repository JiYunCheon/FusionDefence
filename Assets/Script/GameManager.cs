using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static GameManager instance = null;
    public static GameManager inst
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    public bool IsGameOver { get; set; } = false;
    public bool IsGameClear { get; set; } = false;
    public int Monster_Count { get; set; } = 0;
    public int Wave_Level { get; set; } = 1;
    public float totalTime { get; set; } = 0;

    public bool max_Unit { get; set; }=false;


    public bool incomming_boss { get; set; } = false;
    public bool OnGodSkill { get; set; } = false;

    private void Update()
    {
        if (Monster_Count == 50)
        {
            IsGameOver = true;
            Debug.Log("GameOver");
            return;
        }
        if(totalTime>=40f)
        {
            IsGameOver = true;
            Debug.Log("GameOver");
            return;
        }



    }
}
