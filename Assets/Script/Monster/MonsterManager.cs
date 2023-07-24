using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Queen_Skill();
public delegate void King_Skill();
public delegate void God_Skill();
public delegate void God_manaDrane();

public class MonsterManager : MonoBehaviour
{
    #region 싱글톤
    private static MonsterManager instance = null;
    public static MonsterManager inst
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MonsterManager>();
                if (instance == null)
                {
                    instance = new GameObject("MonsterManager").AddComponent<MonsterManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    [Header("[몬스터 정보_Slime]")]
    public float Slime_Speed=3f;
    public int Slime_Hp = 15;
    public int Slime_Wave = 10;
    [Header("[몬스터 정보_Turtle]")]
    public float Turtle_Speed = 2f;
    public int Turtle_Hp = 25;
    public int Turtle_Wave = 10;

    [Header("[몬스터 정보_Orc]")]
    public float Orc_Speed = 0.5f;
    public int Orc_Hp = 35;
    public int Orc_Wave = 10;
    public int OrcHero_Hp = 50;
    public int boss_Wave = 1;
    public int State = 0;

    public Queen_Skill queen_skill;
    public King_Skill king_skill;
    public God_Skill god_skill;
    public God_manaDrane drane;


    public List<GameObject> monList=new List<GameObject>();


    public void Death(GameObject Monster)
    {
        GameManager.inst.IsGameOver = true;
        Monster.SetActive(false);
    }

    public void OnQueenSkill()
    {
        queen_skill();
    }
    public void OnKingSkill()
    {
        king_skill();
    }
    public void OnGodSkill()
    {
        god_skill();
    }
    public void OnGodmanadrane()
    {
        drane();
    }

}
