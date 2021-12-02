using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    [SerializeField]
    Text timerText;


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
    String croppedTime;
    wave waveNum;

    bool first;
    bool second;
    bool third;
    bool fourth;
    bool fifth;
    bool sixth;


    enum wave
    {
        NoWave, FirstWave, SecondWave, ThirdWave, BossWave, MinorWave, MajorWave
    }

    // Start is called before the first frame update
    void Start()   {
        //Wave times
        gameStart = DateTime.Now;
        firstWave = gameStart.AddMinutes(.15);
        secondWave = firstWave.AddMinutes(.25);
        thirdWave = secondWave.AddMinutes(5);
        bossWave = thirdWave.AddMinutes(8);
        minorWave = bossWave.AddMinutes(5);
        majorWave = minorWave.AddMinutes(5);


        enemyList.Add(knight);
        enemyList.Add(ranger);
        enemyList.Add(soldier);     

        waveNum = wave.NoWave;
        first = true;

    }

    // Update is called once per frame
    void Update()
    {
        nextWaveTime = DateTime.Now;
        //Timed wave spawn
        if(nextWaveTime < firstWave)        {

            croppedTime = String.Format("{0:m mm}", firstWave.Subtract(nextWaveTime).ToString());                       
            timerText.text = croppedTime.Remove(0, 3); 

        }else if(nextWaveTime < secondWave)
        {
            croppedTime = String.Format("{0:m mm}", secondWave.Subtract(nextWaveTime).ToString());
            timerText.text = croppedTime.Remove(0, 3);
        }else if(nextWaveTime < thirdWave)
        {
            croppedTime = String.Format("{0:m mm}", thirdWave.Subtract(nextWaveTime).ToString());
            timerText.text = croppedTime.Remove(0, 3);
        }else if(nextWaveTime < bossWave)
        {
            croppedTime = String.Format("{0:m mm}", bossWave.Subtract(nextWaveTime).ToString());
            timerText.text = croppedTime.Remove(0, 3);
        }else if(nextWaveTime < minorWave)
        {
            croppedTime = String.Format("{0:m mm}", minorWave.Subtract(nextWaveTime).ToString());
            timerText.text = croppedTime.Remove(0, 3);
        }else if(nextWaveTime < majorWave)
        {
            croppedTime = String.Format("{0:m mm}", majorWave.Subtract(nextWaveTime).ToString());
            timerText.text = croppedTime.Remove(0, 3);
        }
  

        if (nextWaveTime >= firstWave && first)
        {
            Debug.Log("Spawn 1");
            waveNum = wave.FirstWave;
            spawnWave(waveNum);
            first = false;
            second = true;
        }

        if (nextWaveTime >= secondWave && second)
        {
            Debug.Log("Spawn 2");
            waveNum = wave.SecondWave;
            spawnWave(waveNum);
            second = false;
            third = true;
        }

        if (nextWaveTime >= thirdWave && third)
        {
            Debug.Log("Spawn 3");
            waveNum = wave.ThirdWave;
            spawnWave(waveNum);
            third = false;
            fourth = true;
        }

        if (nextWaveTime >= bossWave && fourth)
        {
            Debug.Log("Spawn 4");
            waveNum = wave.BossWave;
            spawnWave(waveNum);
            fourth = false;
            fifth = true;
        }

        if (nextWaveTime >= minorWave && fifth)
        {
            Debug.Log("Spawn 5");
            waveNum = wave.FirstWave;
            spawnWave(waveNum);
            majorWave = minorWave.AddMinutes(.5);
            fifth = false;
            sixth = true;
        }

        if (nextWaveTime >= majorWave && sixth)
        {
            Debug.Log("Spawn 6");
            waveNum = wave.MajorWave;
            spawnWave(waveNum);
            minorWave = majorWave.AddMinutes(.5);
            sixth = false;
            fifth = true;
        }

        //Manually Spawn Waves

        /**

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

        **/
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
