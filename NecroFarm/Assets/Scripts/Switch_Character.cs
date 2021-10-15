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

    void Start()
    {
        //myCharacter = "Capsule";
        selectedCharacter = "Capsule";
        deselectedCharacters[0] = "Not Empty";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown (1))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            var myRaycast = Physics.Raycast(myRay, out hitInfo, 100);

            if (myRaycast && hitInfo.transform.gameObject.tag == "Zombie")
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);

                myCharacter = hitInfo.transform.gameObject.name;

                deselectedCharacters.Add(selectedCharacter);

                deselectedCharacters = deselectedCharacters.Distinct().ToList();

                selectedCharacter = myCharacter;

                for (int i = 0; i < deselectedCharacters.Count; i++)
                {
                    if (selectedCharacter == deselectedCharacters[i])
                    {
                        deselectedCharacters.RemoveAt(i);
                    }
                    GameObject.Find(deselectedCharacters[i]).GetComponent<Player_Movement>().isMoveable = false;
                }
            }
            GameObject.Find(selectedCharacter).GetComponent<Player_Movement>().isMoveable = true;
        }
    }
}
