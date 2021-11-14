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

    private Animator anim;

    public bool isWalking;

    public Rigidbody rb;

    public Vector3 lastPos;

    void Walk()
    {
        Debug.Log("Walking");
        anim.SetBool("isWalking", true);
    }

    void Idle()
    {
        Debug.Log("Idle");
        anim.SetBool("isWalking", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        isMoveable = false;
        isUI = false;
        myAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown (0))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            rb = gameObject.GetComponent<Rigidbody>();

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
        if (rb.transform.position != lastPos)
        {
            Debug.Log("Velocity: " + rb.velocity.magnitude);
            Walk();
        }
        else
        {
            Idle();
        }
        lastPos = rb.transform.position;
    }  
}
