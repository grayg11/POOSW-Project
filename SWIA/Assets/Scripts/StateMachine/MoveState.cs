using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveState : GameState
{
    Unit player;
    GameObject moveUI;

    public override void Enter()
    {
        if (owner.gameType == 1)
        {
            player = owner.selectedUnit.GetComponent<Unit>();
            Debug.Log("Entered " + player.name + "Move state new from " + player.tileX + ", " + player.tileY + " range: " + player.movement);
            owner.path.GenerateUnitSpaces(player.tileX, player.tileY, player.movement, true, false);
        }
        else
        {
            player = map.SelectedUnit.GetComponent<Unit>();
            Debug.Log("Entered " + player.name + "Move state");
            map.GenerateUnitSpaces(player.tileX, player.tileY, player.movement, true, false);
        }
        player.currentPath = null;
        moveUI = owner.gameUI.transform.GetChild(2).gameObject;
        moveUI.SetActive(true);

        //testing
        owner.moving = true;
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        //testing
        owner.moving = false;

        moveUI.SetActive(false);
    }

}
