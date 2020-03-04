using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerEndState : GameState
{
    Unit player;
    bool ready;

    public override void Enter()
    {
        if (owner.gameType == 1)
        {
            player = owner.selectedUnit.GetComponent<Unit>();
            Debug.Log("Entered " + player.name + "End state");
        }
        else
        {
            player = map.SelectedUnit.GetComponent<Unit>();
            Debug.Log("Entered " + player.name + "End state");
        }

        // Update hero activation
        for (int i = 0; i < owner.heroes.Count; i++)
        {
            if (owner.heroes[i].name.Equals(player.name))
                owner.activated[i] = 1;
        }

        if (player.weaken)
            player.weaken = false;
        ready = true;
    }

    public override void Update()
    {
        // if unit is selected change to unit state
        if (ready)
            changeTo();
    }

    public override void Exit()
    {
        player.movement = player.MaxMovemment;
        player.actions = 2;
    }

    void changeTo()
    {
        ready = false;
        // Enter enemy turn
        Debug.Log("Player End state calling Enemy state...");
        owner.ChangeState<EnemyMainState>();
    }

}
