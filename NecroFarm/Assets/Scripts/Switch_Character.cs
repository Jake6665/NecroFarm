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

    public Behaviour selectedHalo;
    public Behaviour selectedAudioSource;

    void Start()
    {
        //myCharacter = "ZombieSoldier2";
        selectedCharacter = "NecroFarmer";
        deselectedCharacters[0] = "Not Empty";

        selectedHalo = (Behaviour)GameObject.Find(selectedCharacter).GetComponent("Halo");
        selectedHalo.enabled = true;

        selectedAudioSource = (Behaviour)GameObject.Find(selectedCharacter).GetComponent<AudioSource>();
        selectedAudioSource.enabled = true;
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

                for (int i = 0; i < deselectedCharacters.Count; i++)
                {
                    if (selectedCharacter == deselectedCharacters[i])
                    {
                        deselectedCharacters.RemoveAt(i);
                    }
                    GameObject.Find(deselectedCharacters[i]).GetComponent<Player_Movement>().isMoveable = false;
                    selectedHalo = (Behaviour)GameObject.Find(deselectedCharacters[i]).GetComponent("Halo");
                    selectedHalo.enabled = false;

                    selectedAudioSource = (Behaviour)GameObject.Find(deselectedCharacters[i]).GetComponent<AudioSource>();
                    selectedAudioSource.enabled = false;
                }

                GameObject.Find(selectedCharacter).GetComponent<Player_Movement>().isMoveable = true;

                selectedHalo = (Behaviour)GameObject.Find(selectedCharacter).GetComponent("Halo");
                selectedHalo.enabled = true;

                selectedAudioSource = (Behaviour)GameObject.Find(selectedCharacter).GetComponent<AudioSource>();
                selectedAudioSource.enabled = true;
            }
            else
            {
                GameObject.Find(selectedCharacter).GetComponent<Player_Movement>().isMoveable = false;
                selectedHalo = (Behaviour)GameObject.Find(selectedCharacter).GetComponent("Halo");
                selectedHalo.enabled = false;

                selectedAudioSource = (Behaviour)GameObject.Find(selectedCharacter).GetComponent<AudioSource>();
                selectedAudioSource.enabled = false;

                myCharacter = "NecroFarmer";

                deselectedCharacters.Add(selectedCharacter);

                deselectedCharacters = deselectedCharacters.Distinct().ToList();

                GameObject.Find(selectedCharacter).GetComponent<Player_Movement>().isMoveable = false;
                selectedHalo = (Behaviour)GameObject.Find(selectedCharacter).GetComponent("Halo");
                selectedHalo.enabled = false;

                selectedAudioSource = (Behaviour)GameObject.Find(selectedCharacter).GetComponent<AudioSource>();
                selectedAudioSource.enabled = false;

                selectedCharacter = myCharacter;
            }

            for (int i = 0; i < deselectedCharacters.Count; i++)
            {
                if (selectedCharacter == deselectedCharacters[i])
                {
                    deselectedCharacters.RemoveAt(i);
                }
                GameObject.Find(deselectedCharacters[i]).GetComponent<Player_Movement>().isMoveable = false;
                selectedHalo = (Behaviour)GameObject.Find(deselectedCharacters[i]).GetComponent("Halo");
                selectedHalo.enabled = false;

                selectedAudioSource = (Behaviour)GameObject.Find(deselectedCharacters[i]).GetComponent<AudioSource>();
                selectedAudioSource.enabled = false;
            }

            deselectedCharacters.Add(selectedCharacter);

            deselectedCharacters = deselectedCharacters.Distinct().ToList();

            GameObject.Find(selectedCharacter).GetComponent<Player_Movement>().isMoveable = true;
            selectedHalo = (Behaviour)GameObject.Find(selectedCharacter).GetComponent("Halo");
            selectedHalo.enabled = true;

            selectedAudioSource = (Behaviour)GameObject.Find(selectedCharacter).GetComponent<AudioSource>();
            selectedAudioSource.enabled = true;

        }

        if (Input.GetMouseButtonDown(2))
        {
            GameObject.Find(selectedCharacter).GetComponent<Player_Movement>().isMoveable = false;
            selectedHalo = (Behaviour)GameObject.Find(selectedCharacter).GetComponent("Halo");
            selectedHalo.enabled = false;

            selectedAudioSource = (Behaviour)GameObject.Find(selectedCharacter).GetComponent<AudioSource>();
            selectedAudioSource.enabled = false;
        }
    }
}
