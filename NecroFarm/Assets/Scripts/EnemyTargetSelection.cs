using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetSelection : MonoBehaviour
{
    public int walkSpeed;
    public int turnSpeed;
    Dictionary<float, GameObject> targets = new Dictionary<float, GameObject>(); //dictionary with distance to objects as a key, and the object as the value
    GameObject[] objects;   //list of all objects which are tagged "destructable"
    float[] distances;  //list of distances, used to find closest object
    GameObject targetActual;    //the closest gameobject to the script's attached object


    // Start is called before the first frame update
    void Start()
    {
        //find all destructable objects and add to list "targets"
        objects = GameObject.FindGameObjectsWithTag("destructable");  //returns GameObject[]
        distances = new float[objects.Length];

        int temp = 0;
        foreach (GameObject i in objects)
        {
            float distance = Vector3.Distance(i.transform.position, this.transform.position);
            targets.Add(distance, i);

            //populate distances list
            distances[temp] = distance;
            temp++;
        }

        Sort(distances);
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while(distances[i] == null && i<distances.Length)
        {
            i++;
        }
        targetActual = targets[distances[i]];

        /////////////////turn towards and move to object/////////////////////////////
        Vector3 rotDir = targetActual.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(rotDir);
        Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Calculate direction vector.
        Vector3 posDir = this.transform.position - targetActual.transform.position;

        // Normalize resultant vector to unit Vector.
        posDir = -posDir.normalized;

        // Move in the direction of the direction vector every frame.
        this.transform.position += posDir * Time.deltaTime * walkSpeed;

    }

    void Sort(float[] input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            var min = i;
            for (var j = i + 1; j < input.Length; j++)
            {
                if (input[min] > input[j])
                {
                    min = j;
                }
            }

            if (min != i)
            {
                var lowerValue = input[min];
                input[min] = input[i];
                input[i] = lowerValue;
            }
        }
    }
}
