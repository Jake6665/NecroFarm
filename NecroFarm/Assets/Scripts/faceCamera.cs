using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceCamera : MonoBehaviour
{
    //public Transform target;
    public Camera target;

    void Start()
    {
        target = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(transform.position + target.transform.rotation * Vector3.forward, target.transform.rotation * Vector3.up);
    }
}
