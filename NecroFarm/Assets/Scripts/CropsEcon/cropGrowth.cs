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

    [SerializeField]
    GameObject firstStage;
    [SerializeField]
    GameObject secondStage;
    [SerializeField]
    GameObject thirdStage;



    private bool finshedGrowing = false;
    private List<GameObject> ObjectsInRange = new List<GameObject>();
    [SerializeField]
    private bool initialPlacement;
    [SerializeField]
    public ScriptableObjects selfRef;
    private GameObject gridObj;
    public bool hasRestored = true;


    private void Start()
    {

        Debug.Log("Inital Placement: "+initialPlacement.ToString());
        gridObj = GameObject.FindGameObjectWithTag("GBS");
        if (initialPlacement == true)
        {
            plantTime = DateTime.Now;
            midGrowth = plantTime.AddSeconds(growthTime / 2);
            fullGrown = plantTime.AddSeconds(growthTime);


            initialPlacement = false;
        }
        else{
            Debug.Log("Has Restored: " + hasRestored.ToString());
            if (hasRestored == true)
            {
                hasRestored = false;
                Debug.Log("Has Restored: " + hasRestored.ToString());
                //Create clone refrence
                selfRef.currentPrefab = this.transform;

                //Add self to grid
                gridObj.GetComponent<GridBuildingSystem>().RestoreToGrid(this.gameObject);
                Debug.Log("Restroing " + this.gameObject.name.ToString());

                //Reser refrence to self
                selfRef.currentPrefab = selfRef.defaultPrefab;

                //Kill self
                Debug.Log("DESTROY DESTORY");
                GameObject.Destroy(this.gameObject);
            }

        }
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
            thirdStage.SetActive(true);
            firstStage.SetActive(false);
            secondStage.SetActive(false);

        }else if (plantTime > midGrowth)
        {
            secondStage.SetActive(true);
            firstStage.SetActive(false);
        }
    }
    /**
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Well")
        {
            midGrowth = midGrowth.AddSeconds((-1 * ((growthTime / 2) / 2)));
            fullGrown = fullGrown.AddSeconds(-1 * (growthTime / 2));
        }

    }
    **/
}
