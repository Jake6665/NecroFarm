using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player_Movement : MonoBehaviour
{
    public LayerMask whatCanBeClickedOn;

    private NavMeshAgent myAgent;

    public bool isMoveable;

    public bool isUI;

    // Start is called before the first frame update
    void Start()
    {
        isMoveable = false;
        isUI = false;
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown (0))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            var myRaycast = Physics.Raycast(myRay, out hitInfo, 100);

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (hitInfo.transform.gameObject.tag == "UI")
                {
                    isUI = true;
                    myRaycast = Physics.Raycast(myRay, out hitInfo, 0);
                    Debug.Log("UI is: " + isUI);
                }
                else if (myRaycast && isMoveable && isUI == false && (hitInfo.transform.gameObject.tag == "Ground" || hitInfo.transform.gameObject.tag == "Well" ))
                {
                    myAgent.SetDestination(hitInfo.point);
                    Debug.Log("Moving");
                }
            }
        }
    }
}
