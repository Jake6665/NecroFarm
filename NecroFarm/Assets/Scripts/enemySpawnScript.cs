using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class enemySpawnScript : MonoBehaviour
{
    private NavMeshAgent myAgent;

    [SerializeField]
    GameObject knight;
    [SerializeField]
    GameObject ranger;
    [SerializeField]
    GameObject soldier;
    [SerializeField]
    GameObject boss;

    List<GameObject> enemyList = new List<GameObject>();

    [SerializeField]
    List<GameObject> spawnPointList;

    DateTime gameStart;
    DateTime firstWave;
    DateTime secondWave;
    DateTime thirdWave;
    DateTime bossWave;
    DateTime minorWave;
    DateTime majorWave;

    DateTime nextWaveTime;
    wave waveNum;

    enum wave
    {
        NoWave, FirstWave, SecondWave, ThirdWave, BossWave, MinorWave, MajorWave
    }

    // Start is called before the first frame update
    void Start()   {
        enemyList.Add(knight);
        enemyList.Add(ranger);
        enemyList.Add(soldier);
        

        DateTime gameStart = DateTime.Now;
        waveNum = wave.NoWave;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)){
            waveNum = wave.FirstWave;
            spawnWave(waveNum);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            waveNum = wave.SecondWave;
            spawnWave(waveNum);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            waveNum = wave.ThirdWave;
            spawnWave(waveNum);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            waveNum = wave.BossWave;
            spawnWave(waveNum);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            waveNum = wave.MinorWave;
            spawnWave(waveNum);
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            waveNum = wave.MajorWave;
            spawnWave(waveNum);
        }
    }

    //
    void spawnWaveOne()
    {
        //Number of units to spawn for wave 1
        int spawnCount = 4;
        for(int i = 0;  i < spawnCount; i++)
        {
            GameObject spawnEnemy = enemyList[UnityEngine.Random.Range(0, 3)];
            Instantiate(spawnEnemy, spawnPointList[i].transform.position, Quaternion.identity);
        }        

    }

    void spawnWaveTwo()
    {
        //Number of units to spawn for wave 2
        int spawnCount = 6;
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject spawnEnemy = enemyList[UnityEngine.Random.Range(0, 3)];
            Instantiate(spawnEnemy, spawnPointList[i].transform.position, Quaternion.identity);
        }
    }

    void spawnWaveThree()
    {
        //Number of units to spawn for wave 3
        int spawnCount = 10;
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject spawnEnemy = enemyList[UnityEngine.Random.Range(0, 3)];
            Instantiate(spawnEnemy, spawnPointList[i].transform.position, Quaternion.identity);
        }
    }

    void spawnBossWave()
    {
        //Number of units to spawn with boss
        int spawnCount = 8;
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject spawnEnemy = enemyList[UnityEngine.Random.Range(0, 3)];
            Instantiate(spawnEnemy, spawnPointList[i].transform.position, Quaternion.identity);
        }
        Instantiate(boss, spawnPointList[spawnCount + 1].transform.position, Quaternion.identity);
    }

    //Post boss wave // Random Wave
    void spawnMinorWave()
    {
        //Number of units to spawn for wave 1
        int spawnCount = 8;
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject spawnEnemy = enemyList[UnityEngine.Random.Range(0, 3)];
            Instantiate(spawnEnemy, spawnPointList[i].transform.position, Quaternion.identity);
        }
    }

    void spawnMajorWave()
    {
        //Number of units to spawn for a major wave
        int spawnCount = 12;
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject spawnEnemy = enemyList[UnityEngine.Random.Range(0, 3)];
            Instantiate(spawnEnemy, spawnPointList[i].transform.position, Quaternion.identity);
        }
    }

    void spawnWave(wave waveNum)
    {
        ShuffleWave(spawnPointList);

        switch (waveNum)
        {
            case wave.NoWave:
                break;
            case wave.FirstWave:
                spawnWaveOne();
                break;
            case wave.SecondWave:
                spawnWaveTwo();
                break;
            case wave.ThirdWave:
                spawnWaveThree();
                break;
            case wave.BossWave:
                spawnBossWave();
                break;
            case wave.MinorWave:
                spawnMinorWave();
                break;
            case wave.MajorWave:
                spawnMajorWave();
                break;
            default:
                break;
        }
    }

    //Shuffles spawn point order for each wave
    public static List<GameObject> ShuffleWave(List<GameObject> aList)
    {
        System.Random _random = new System.Random();
        GameObject myGO;

        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }
        return aList;
    }
}
