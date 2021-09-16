using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cropGrowth : MonoBehaviour
{
    [SerializeField]
    private double growthTime = 10;
    DateTime plantTime;
    DateTime midGrowth;
    DateTime fullGrown;

    private void Start()
    {
        plantTime = DateTime.Now;
        midGrowth = plantTime.AddSeconds(5);
        fullGrown = plantTime.AddSeconds(10);

        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Debug.Log(plantTime);
        Debug.Log(midGrowth);
        Debug.Log(fullGrown);


    }

    private void Update()
    {
        plantTime = DateTime.Now;
        if (plantTime > fullGrown)
        {
            Debug.Log("full grown");
            transform.localScale = new Vector3(1f, 1f, 1f);
        }else if (plantTime > midGrowth)
        {
            Debug.Log("mid grown");
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

    }
}
