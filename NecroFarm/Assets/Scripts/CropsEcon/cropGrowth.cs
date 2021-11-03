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
    private List<GameObject> ObjectsInRange = new List<GameObject>();
    [SerializeField]
    private bool initialPlacement;
    [SerializeField]
    public ScriptableObjects selfRef;
    private GameObject gridObj;


    private void Start()
    {
        Debug.Log(initialPlacement.ToString());
        gridObj = GameObject.FindGameObjectWithTag("GBS");
        if (initialPlacement == true)
        {
            plantTime = DateTime.Now;
            midGrowth = plantTime.AddSeconds(growthTime / 2);
            fullGrown = plantTime.AddSeconds(growthTime);

            transform.localScale = new Vector3(1f, 0.1f, 1f);
            initialPlacement = false;
        }else{
            selfRef.prefab = this.transform;
            gridObj.GetComponent<GridBuildingSystem>().RestoreToGrid(this.gameObject);
            Debug.Log("Restroing " + this.gameObject.name.ToString());
            GameObject.Destroy(this.gameObject);
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
            transform.localScale = new Vector3(1f, 1f, 1f);
        }else if (plantTime > midGrowth)
        {
            transform.localScale = new Vector3(1f, 0.5f, 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Well")
        {
            midGrowth = midGrowth.AddSeconds((-1 * ((growthTime / 2) / 2)));
            fullGrown = fullGrown.AddSeconds(-1 * (growthTime / 2));
        }
    }

}
