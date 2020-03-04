using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionState : GameState
{
    Unit player;
    GameObject actionUI;

    public override void Enter()
    {

        player = owner.selectedUnit.GetComponent<Unit>();
        Debug.Log("Entered " + player.name + "Action state");

        actionUI = owner.gameUI.transform.GetChild(11).gameObject;

        if (player.stun)
        {
            actionUI.transform.GetChild(0).gameObject.SetActive(true);
            actionUI.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
            actionUI.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            actionUI.transform.GetChild(0).gameObject.SetActive(false);
            actionUI.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
            actionUI.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = true;
        }

        actionUI.SetActive(true);

    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        actionUI.SetActive(false);
    }

    void changeTo()
    {

        Debug.Log("Leaving Player Rest State");

        if (owner.gameType == 1)
        {
            if (player.unit == 0)
            {
                owner.ChangeState<DialaState>();
            }
            if (player.unit == 1)
            {
                owner.ChangeState<FennState>();
            }
            if (player.unit == 2)
            {
                owner.ChangeState<GaarkhanState>();
            }
            if (player.unit == 3)
            {
                owner.ChangeState<GideonState>();
            }
            if (player.unit == 4)
            {
                owner.ChangeState<JynState>();
            }
            if (player.unit == 5)
            {
                owner.ChangeState<MakState>();
            }
        }
        else
        {
            if (map.SelectedUnit.name.Equals("Diala"))
            {
                owner.ChangeState<DialaState>();
            }
            if (map.SelectedUnit.name.Equals("Fenn"))
            {
                owner.ChangeState<FennState>();
            }
            if (map.SelectedUnit.name.Equals("Gaarkhan"))
            {
                owner.ChangeState<GaarkhanState>();
            }
            if (map.SelectedUnit.name.Equals("Gideon"))
            {
                owner.ChangeState<GideonState>();
            }
            if (map.SelectedUnit.name.Equals("Jyn"))
            {
                owner.ChangeState<JynState>();
            }
            if (map.SelectedUnit.name.Equals("Mak"))
            {
                owner.ChangeState<MakState>();
            }
        }

    }
}
