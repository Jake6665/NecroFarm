using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimitX;
    public Vector2 panLimitZ;
    public float scrollSpeed = 20f;
    private void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 110f*  Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, panLimitX.x, panLimitX.y);
        pos.z = Mathf.Clamp(pos.z, panLimitZ.x, panLimitZ.y);
        transform.position = pos;
    }
}
