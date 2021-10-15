using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn_UnitA : MonoBehaviour
{
    //public LayerMask whatCanBeClickedOn;

    public GameObject unitToSpawn;

    //public int clickCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown (0))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            var myRaycast = Physics.Raycast(myRay, out hitInfo, 100);

            if (myRaycast && hitInfo.transform.gameObject.tag == "Spawn")
            {
                Debug.Log("Spawning...");
                Instantiate(unitToSpawn, transform.position, unitToSpawn.transform.rotation);
                unitToSpawn.name = ("Capsule" + Time.deltaTime);
            } 
        }
        //clickCount++;
    }
}
