using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableTile : MonoBehaviour
{

    public int tileX;
    public int tileY;
    public int type;
    public TileMap map;

    //testing
    GameStateController GSC;

    private void Start()
    {
        //testing
        GSC = GameObject.FindObjectOfType<GameStateController>();
    }

    void OnMouseUp()
    {
       if (GSC.gameType == 1)
        {
            //when not selected/another is clicked
            if (GSC.data.clickedTile != null && GSC.moving)
                GSC.data.clickedTile.GetComponent<Renderer>().material = GSC.data.moveTileUnselected;
            else if (GSC.data.clickedTile != null && GSC.attacking)
                GSC.data.clickedTile.GetComponent<Renderer>().material = GSC.data.attackTile;

            if (GSC.selectedUnit != null && !EventSystem.current.IsPointerOverGameObject())
            {
                //testing/
                if (GSC.attacking)
                {
                    GSC.data.clickedTile = GSC.data.attackSpaces[new Vector2(tileX, tileY)];
                    // Can change if you want to but would be attackTileSelected or whatever.
                    GSC.data.clickedTile.GetComponent<Renderer>().material = GSC.data.moveTileSelected;
                    GSC.path.GeneratePathTo(tileX, tileY, false, false);
                    //    // minus 1 because path counts the players tile too
                    GSC.selectedUnit.GetComponent<Unit>().attackRange = GSC.selectedUnit.GetComponent<Unit>().currentPath.Count - 1;
                    //    // Clear current path because it can lead to problems. Im just printing out the attack range, but you can do whatever here
                    GSC.selectedUnit.GetComponent<Unit>().currentPath = null;
                }
                else
                //testing
                if (GSC.moving)
                {
                    GSC.data.clickedTile = GSC.data.moveSpaces[new Vector2(tileX, tileY)];
                    GSC.data.clickedTile.GetComponent<Renderer>().material = GSC.data.moveTileSelected;
                    GSC.path.GeneratePathTo(tileX, tileY, false, false);
                }
            }
        }
        else
        {
            //when not selected/another is clicked
            if (map.clickedTile != null && GSC.moving)
                map.clickedTile.GetComponent<Renderer>().material = map.moveTileUnselected;
            else if (map.clickedTile != null && GSC.attacking)
                map.clickedTile.GetComponent<Renderer>().material = map.attackTile;

            if (map.SelectedUnit != null && !EventSystem.current.IsPointerOverGameObject())
            {
                //testing/
                if (GSC.attacking)
                {
                    map.clickedTile = map.attackSpaces[new Vector2(tileX, tileY)];
                    // Can change if you want to but would be attackTileSelected or whatever.
                    map.clickedTile.GetComponent<Renderer>().material = map.moveTileSelected;
                    map.GeneratePathTo(tileX, tileY, false, false);
                    //    // minus 1 because path counts the players tile too
                    map.SelectedUnit.GetComponent<Unit>().attackRange = map.SelectedUnit.GetComponent<Unit>().currentPath.Count - 1;
                    //    // Clear current path because it can lead to problems. Im just printing out the attack range, but you can do whatever here
                    map.SelectedUnit.GetComponent<Unit>().currentPath = null;
                }
                else
                //testing
                if (GSC.moving)
                {
                    map.clickedTile = map.moveSpaces[new Vector2(tileX, tileY)];
                    map.clickedTile.GetComponent<Renderer>().material = map.moveTileSelected;
                    map.GeneratePathTo(tileX, tileY, false, false);
                }
            }
        }

       
    }

}