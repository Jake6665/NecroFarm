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
    private bool finshedGrowing = false;

    private void Start()
    {
        plantTime = DateTime.Now;
        midGrowth = plantTime.AddSeconds(growthTime / 2);
        fullGrown = plantTime.AddSeconds(growthTime);

        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    public bool isGrown()
    {
        return finshedGrowing;
    }

    private void Update()
    {
        plantTime = DateTime.Now;
        if (plantTime > fullGrown)
        {
            finshedGrowing = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }else if (plantTime > midGrowth)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

    }
}
