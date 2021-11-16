using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.UI;

public class Switch_Character : MonoBehaviour
{
    public string myCharacter;

    public List<string> deselectedCharacters = new List<string>();
    public string selectedCharacter = "";

    public Behaviour selected;

    void Start()
    {
        //myCharacter = "ZombieSoldier2";
        selectedCharacter = "NecroFarmer";
        deselectedCharacters[0] = "Not Empty";
        selected = (Behaviour)GameObject.Find(selectedCharacter).GetComponent("Halo");
        selected.enabled = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown (1))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            var myRaycast = Physics.Raycast(myRay, out hitInfo, 1000);

            if (myRaycast && hitInfo.transform.gameObject.tag == "Zombie")
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);

                myCharacter = hitInfo.transform.gameObject.name;

                deselectedCharacters.Add(selectedCharacter);

                deselectedCharacters = deselectedCharacters.Distinct().ToList();

                selectedCharacter = myCharacter;

                GameObject.Find(selectedCharacter).GetComponent<Player_Movement>().isMoveable = true;
                selected = (Behaviour)GameObject.Find(selectedCharacter).GetComponent("Halo");
                selected.enabled = true;
            }

            else
            {
                GameObject.Find(selectedCharacter).GetComponent<Player_Movement>().isMoveable = false;
                selected = (Behaviour)GameObject.Find(selectedCharacter).GetComponent("Halo");
                selected.enabled = false;

                myCharacter = "NecroFarmer";

                deselectedCharacters.Add(selectedCharacter);

                deselectedCharacters = deselectedCharacters.Distinct().ToList();

                GameObject.Find(selectedCharacter).GetComponent<Player_Movement>().isMoveable = false;
                selected = (Behaviour)GameObject.Find(selectedCharacter).GetComponent("Halo");
                selected.enabled = false;

                selectedCharacter = myCharacter;
            }

            for (int i = 0; i < deselectedCharacters.Count; i++)
            {
                if (selectedCharacter == deselectedCharacters[i])
                {
                    deselectedCharacters.RemoveAt(i);
                }
                GameObject.Find(deselectedCharacters[i]).GetComponent<Player_Movement>().isMoveable = false;
                selected = (Behaviour)GameObject.Find(deselectedCharacters[i]).GetComponent("Halo");
                selected.enabled = false;
            }

            deselectedCharacters.Add(selectedCharacter);

            deselectedCharacters = deselectedCharacters.Distinct().ToList();

            GameObject.Find(selectedCharacter).GetComponent<Player_Movement>().isMoveable = true;
            selected = (Behaviour)GameObject.Find(selectedCharacter).GetComponent("Halo");
            selected.enabled = true;
            
        }

        if (Input.GetMouseButtonDown (2))
        {
            GameObject.Find(selectedCharacter).GetComponent<Player_Movement>().isMoveable = false;
            selected = (Behaviour)GameObject.Find(selectedCharacter).GetComponent("Halo");
            selected.enabled = false;
        }
    }
}
