using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseBehavior : MonoBehaviour
{
    public GameObject unit;
    public TileMap map;
    public Canvas playerSelectionMenu;
    public Camera cam;
    //public CrateBehavior crate;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Mouse item selection
        if (Input.GetMouseButtonUp(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
                determineClickedObject();
        }

        //if (unit != null)
        //    playerSelectionMenu.transform.GetChild(0).gameObject.SetActive(true);
        //    //playerSelectionMenu.enabled = true;
        //else
        //    playerSelectionMenu.transform.GetChild(0).gameObject.SetActive(false);
        //    //playerSelectionMenu.enabled = false;
    }


    void determineClickedObject()
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity);
        if (hit)
        {
            //Debug.Log("Hit " + hitInfo.transform.gameObject.name);

            if (hitInfo.transform.gameObject.name.Equals("Rebel"))
            {
                //EraseOldMoves();

                unit = hitInfo.transform.root.gameObject;
                map.SelectedUnit = unit;
                map.GenerateUnitSpaces(unit.GetComponent<Unit>().tileX, unit.GetComponent<Unit>().tileY, Mathf.CeilToInt(unit.GetComponent<Unit>().MaxMovemment), true, false);
            }
            if (hitInfo.transform.gameObject.name.Equals("Imperial"))
            {
                //EraseOldMoves();

                unit = hitInfo.transform.root.gameObject;
                map.SelectedUnit = unit;
                map.GenerateUnitSpaces(unit.GetComponent<Unit>().tileX, unit.GetComponent<Unit>().tileY, Mathf.CeilToInt(unit.GetComponent<Unit>().MaxMovemment), true, false);
            }

            //if (hitInfo.transform.gameObject.name.Equals("Crate Green"))
            //{
            //    crate.test();
            //}

        }
        else
        {
            //Debug.Log("No hit");
            //map.SelectedUnit = null;
            //unit = null;
        }
    }

    //private void EraseOldMoves()
    //{
    //    GameObject[] oldMove = GameObject.FindGameObjectsWithTag("MoveSpace");
    //    foreach (GameObject old in oldMove)
    //        GameObject.Destroy(old);
    //}
}
