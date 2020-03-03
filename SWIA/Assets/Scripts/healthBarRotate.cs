using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class healthBarRotate : MonoBehaviour
{
    public Camera my_camera;
    public GameStateController GSC;
    RectTransform rectTransform;
    //float x;
    
    // Start is called before the first frame update
    void Start()
    {
        GSC = FindObjectOfType<GameStateController>();
        my_camera = GSC.cam;
        rectTransform = GetComponent<RectTransform>();
        //rectTransform.Rotate(new Vector3(0, 90, 0));
        //x = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(transform.position + my_camera.transform.rotation * Vector3.back, my_camera.transform.rotation * Vector3.up);
        //transform.LookAt(my_camera);
        //rectTransform.yrotation = 90;
        
        //Debug.Log(Camera.main.transform.eulerAngles.z);
        //rectTransform.Rotate(new Vector3(0, 90-(20* Camera.main.transform.rotation.z), 0));
        //rectTransform.rotation = new Vector3(0, 90 - (20 * Camera.main.transform.rotation.z),0);
        //rectTransform.rotation = Quaternion.Euler(0, 90 - (20 * Camera.main.transform.rotation.z), 0);
        rectTransform.LookAt(Camera.main.transform);
        rectTransform.Rotate(0, 0, -Camera.main.transform.eulerAngles.z);
    }
}
