using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 2f;
    public float panBoarderThickness = 10f;
    public Vector2 panLimitX;
    public Vector2 panLimitY;
    public float scrollSpeed = 20f;
    public float minZ = -20f;
    public float maxZ = -120f;

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBoarderThickness)
        {
            position.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBoarderThickness)
        {
            position.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBoarderThickness)
        {
            position.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBoarderThickness)
        {
            position.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("q"))
        {
            position.z -= scrollSpeed * Time.deltaTime;
        }
        if (Input.GetKey("e"))
        {
            position.z += scrollSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        //position.z -= scroll * scrollSpeed * 100f * Time.deltaTime;
        //position.z = Mathf.Clamp(position.z, minZ, maxZ);

        position.x = Mathf.Clamp(position.x, -panLimitX.x, panLimitX.y);
        position.y = Mathf.Clamp(position.y, -panLimitY.x, panLimitY.y);

        transform.position = position;


    }
}
