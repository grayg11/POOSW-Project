using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AttackState : GameState
{
    Unit player;
    GameObject attackUI;

    public override void Enter()
    {
        owner.attacking = true;

        

        if (owner.gameType == 1)
        {
            player = owner.selectedUnit.GetComponent<Unit>();
            Debug.Log("Entered " + player.name + " Attack state");
            findRanges();
            owner.path.GenerateUnitSpaces(player.tileX, player.tileY, player.minRange, false, true);
        }
        else
        {
            player = map.SelectedUnit.GetComponent<Unit>();
            Debug.Log("Entered " + player.name + " Attack state");
            findRanges();
            map.GenerateUnitSpaces(player.tileX, player.tileY, getMaxRange(), false, true);
        }


        player.currentPath = null;

        //TODO:  add ranges to UI

        attackUI = owner.gameUI.transform.GetChild(9).gameObject;
        attackUI.transform.GetChild(0).GetComponent<Button>().interactable = false;
        attackUI.SetActive(true);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        owner.attacking = false;
        attackUI.SetActive(false);
    }

    public int getMinRange()
    {
        if (owner.gameType == 1)
            player = owner.selectedUnit.GetComponent<Unit>();
        else
            player = map.SelectedUnit.GetComponent<Unit>();

        if (player.name.Equals("Diala"))
        {
            return 1;
        }
        if (player.name.Equals("Fenn"))
        {
            return 3;
        }
        if (player.name.Equals("Gaarkhan"))
        {
            return 0;
        }
        if (player.name.Equals("Gideon"))
        {
            return 2;
        }
        if (player.name.Equals("Jyn"))
        {
            return 2;
        }
        if (player.name.Equals("Mak"))
        {
            return 4;
        }
        else return 0;
    }

    public int getMaxRange()
    {
        if (owner.gameType == 1)
            player = owner.selectedUnit.GetComponent<Unit>();
        else
            player = map.SelectedUnit.GetComponent<Unit>();

        if (player.name.Equals("Diala"))
        {
            return 5;
        }
        if (player.name.Equals("Fenn"))
        {
            return 8;
        }
        if (player.name.Equals("Gaarkhan"))
        {
            return 2;
        }
        if (player.name.Equals("Gideon"))
        {
            return 7;
        }
        if (player.name.Equals("Jyn"))
        {
            return 6;
        }
        if (player.name.Equals("Mak"))
        {
            return 10;
        }
        else return 0;
    }

    void findRanges()
    {
        int min = 0, mid = 0, max = 0;

        foreach (int die in player.weapon.weaponDice)
        {
            if (die == 1)
            {
                mid += 1;
                max += 2;
            }
            if (die == 2)
            {
                min += 1;
                mid += 1;
                max += 3;
            }
            if (die == 3)
            {
                min += 2;
                mid += 3;
                max += 5;
            }
        }

        if (min == 0)
            min = 1;
        if (mid == 0)
            mid = 1;
        if (max == 0)
            max = 1;

        player.minRange = min;
        player.midRange = mid;
        player.maxRange = max;
    }
}
