using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player_Movement : MonoBehaviour
{
    public LayerMask whatCanBeClickedOn;

    private NavMeshAgent myAgent;

    public bool isMoveable, isUI;

    private Animator anim;

    public Rigidbody rb;

    public Vector3Int mousePos, unitPos;

    private string unitName, lastUnit;

    void Walk()
    {
        //Debug.Log("Walking");
        anim.SetBool("isWalking", true);
    }

    void Idle()
    {
        //Debug.Log("Idle");
        anim.SetBool("isWalking", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        lastUnit = "NecroFarmer";
        isMoveable = false;
        isUI = false;
        myAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        unitPos.x = Mathf.RoundToInt(myAgent.transform.position.x);
        unitPos.z = Mathf.RoundToInt(myAgent.transform.position.z);

        //unitName = GameObject.Find(hitInfo.transform.name).GetComponent<Switch_Character>().name;

        if (Input.GetMouseButtonDown (0))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            rb = gameObject.GetComponent<Rigidbody>();

            var myRaycast = Physics.Raycast(myRay, out hitInfo, 100);

            mousePos.x = Mathf.RoundToInt(hitInfo.point.x);
            mousePos.z = Mathf.RoundToInt(hitInfo.point.z);

            unitName = hitInfo.transform.name;

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (hitInfo.transform.gameObject.tag == "UI")
                {
                    isUI = true;
                    myRaycast = Physics.Raycast(myRay, out hitInfo, 0);
                    Debug.Log("UI is: " + isUI);
                }
                else if (myRaycast && isMoveable && isUI == false && (hitInfo.transform.gameObject.tag == "Ground" || hitInfo.transform.gameObject.tag == "Well" || hitInfo.transform.gameObject.tag == "Crop"))
                {
                    myAgent.SetDestination(hitInfo.point);
                    Debug.Log("Moving");
                }
            }

            //Debug.Log("name: " + unitName);
            if ((mousePos.x, mousePos.z) != (unitPos.x, unitPos.z) && isMoveable)
            {
                Walk();
            }
        }
        if ((mousePos.x, mousePos.z) == (unitPos.x, unitPos.z))
        {
            if (lastUnit != unitName)
            {
                isMoveable = false;
            }
            Idle();
        }

        lastUnit = unitName;
    }  
}
