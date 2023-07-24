using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("[몬스터프리팹]")]
    public GameObject slimePrefab;
    public GameObject tutlePrefab;
    public GameObject OrcPrefab;
    public GameObject OrcHeroPrefab;


    float wave_time;

    public GameObject []Monster;
    
    private void OnEnable()
    {
        
        if (GameManager.inst.Wave_Level>=1&& GameManager.inst.Wave_Level <4|| 
            GameManager.inst.Wave_Level >= 11 && GameManager.inst.Wave_Level < 14)
        {
            WaveState(MonsterManager.inst.Slime_Wave);
            StartCoroutine(MonsterSpawn(slimePrefab));
        }
        else if (GameManager.inst.Wave_Level >= 4 && GameManager.inst.Wave_Level < 7||
            GameManager.inst.Wave_Level >= 14 && GameManager.inst.Wave_Level < 17)
        {
            WaveState(MonsterManager.inst.Turtle_Wave);
            StartCoroutine(MonsterSpawn(tutlePrefab));
        }
        else if (GameManager.inst.Wave_Level >= 7 && GameManager.inst.Wave_Level < 10||
            GameManager.inst.Wave_Level >= 17 && GameManager.inst.Wave_Level < 20)
        {
            WaveState(MonsterManager.inst.Orc_Wave);
            StartCoroutine(MonsterSpawn(OrcPrefab));
        }
        else if (GameManager.inst.Wave_Level >=10 && GameManager.inst.Wave_Level < 11||
            GameManager.inst.Wave_Level >= 20 && GameManager.inst.Wave_Level < 21)
        {
            WaveState(MonsterManager.inst.boss_Wave);
            StartCoroutine(MonsterSpawn(OrcHeroPrefab));
            GameManager.inst.incomming_boss = true;
        }
        wave_time = 5f;
    }
    
    void Update()
    {
        if (GameManager.inst.IsGameOver) return;

        if (GameManager.inst.Wave_Level == 20) return;
       
        if(GameManager.inst.Wave_Level%10==0)
        {
            wave_time = 40f;
        }

        if(GameManager.inst.totalTime>=wave_time)
        {
            gameObject.SetActive(false);
            GameManager.inst.totalTime = 0;
            GameManager.inst.Wave_Level++;
            gameObject.SetActive(true);
        }
    }


    //REMOVE하지말고 위치를 옮긴다음 리스트에 넣기?
    IEnumerator MonsterSpawn(GameObject prefab)
    {
        int wave=1;
        if (GameManager.inst.IsGameOver) yield break;
       

        for (int i = 0; i < Monster.Length; i++)
        {
            if(GameManager.inst.Wave_Level>wave)
            {
                wave = GameManager.inst.Wave_Level;
                for (int j = 0; j < MonsterManager.inst.monList.Count; j++)
                {
                    MonsterManager.inst.monList.Remove(MonsterManager.inst.monList[j]);
                }
            }
            if (MonsterManager.inst.monList.Count==0)
            {
                GameManager.inst.Monster_Count++;
                Monster[i] = Instantiate(prefab, new Vector3(-9, 0f, 10), transform.rotation);
            }
            else if(MonsterManager.inst.monList.Count != 0)
            {
                if(GameManager.inst.Wave_Level==4|| GameManager.inst.Wave_Level == 5|| GameManager.inst.Wave_Level == 6)
                {
                    for (int j = 0; j < MonsterManager.inst.monList.Count; j++)
                    {
                        if (MonsterManager.inst.monList[j].tag == "Slime")
                        {
                            MonsterManager.inst.monList.Remove(MonsterManager.inst.monList[j]);
                        }
                    }
                    
                }
                if (GameManager.inst.Wave_Level == 7 || GameManager.inst.Wave_Level == 8 || GameManager.inst.Wave_Level == 9)
                {
                    for (int j = 0; j < MonsterManager.inst.monList.Count; j++)
                    {
                        if (MonsterManager.inst.monList[j].tag == "Turtle")
                        {
                            MonsterManager.inst.monList.Remove(MonsterManager.inst.monList[j]);
                        }
                    }
                }
                if (GameManager.inst.Wave_Level ==10)
                {
                    for (int j = 0; j < MonsterManager.inst.monList.Count; j++)
                    {
                        if (MonsterManager.inst.monList[j].tag == "Orc")
                        {
                            MonsterManager.inst.monList.Remove(MonsterManager.inst.monList[j]);
                        }
                    }
                }
                if (MonsterManager.inst.monList.Count == 0)
                {
                    GameManager.inst.Monster_Count++;
                    Monster[i] = Instantiate(prefab, new Vector3(-9, 0f, 10), transform.rotation);
                }
                else if(MonsterManager.inst.monList.Count != 0)
                {
                    GameManager.inst.Monster_Count++;
                    MonsterManager.inst.monList[0].gameObject.SetActive(true);
                    MonsterManager.inst.monList[0].transform.position = new Vector3(-9, 0f, 10);
                    MonsterManager.inst.monList.Remove(MonsterManager.inst.monList[0]);
                }
            }

            yield return new WaitForSeconds(3f);
        }

        yield break;
    }

    void WaveState(int Monster_Wave)
    {
        Monster = new GameObject[Monster_Wave];
    }
    

}
