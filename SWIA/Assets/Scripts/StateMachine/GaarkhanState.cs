using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GaarkhanState : GameState
{
    Unit player;

    public override void Enter()
    {
        Debug.Log("Entered Gaarkhan state");
        if (owner.gameType == 1)
        {
            player = owner.selectedUnit.GetComponent<Unit>();

            Vector3 playerPos = new Vector3(owner.selectedUnit.transform.position.x, owner.selectedUnit.transform.position.y, owner.rig.transform.position.z);
            owner.rig.GetComponent<MyCamera>().newPosition = playerPos;
        }
        else
        {
            player = map.SelectedUnit.GetComponent<Unit>();

            Vector3 playerPos = new Vector3(map.SelectedUnit.transform.position.x, map.SelectedUnit.transform.position.y, owner.rig.transform.position.z);
            owner.rig.GetComponent<MyCamera>().newPosition = playerPos;
        }
        Debug.Log("Gaarkhan has " + player.actions + " actions " + player.movement + " movement");

        owner.gameUI.transform.GetChild(0).GetChild(2).GetComponent<Button>().interactable = true;
        owner.gameUI.transform.GetChild(0).GetChild(3).GetComponent<Button>().interactable = true;

        if (player.actions <= 0)
        {
            owner.gameUI.transform.GetChild(0).GetChild(2).GetComponent<Button>().interactable = false;
            owner.gameUI.transform.GetChild(0).GetChild(3).GetComponent<Button>().interactable = false;

            // If player has remaining movement
            if (player.movement > 0)
            {
                // Prompt if user wants to use movement, if so enter movestate
                owner.gameUI.GetComponent<GameUI>().endTurn();
            }
            else
            {
                owner.gameUI.GetComponent<GameUI>().dontMove();
            }
        }


        // Enable UI
        owner.gameUI.transform.GetChild(0).GetComponent<Image>().sprite = player.playerCard;
        owner.gameUI.transform.GetChild(0).GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = player.maxHealth.ToString();
        owner.gameUI.transform.GetChild(0).GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = player.maxEndurance.ToString();
        string move = string.Format(player.MaxMovemment + " / " + player.MaxMovemment);
        owner.gameUI.transform.GetChild(0).GetChild(6).GetComponent<TMPro.TextMeshProUGUI>().text = move;
        owner.gameUI.transform.GetChild(0).gameObject.SetActive(true);

        if (player.stun && player.actions > 0)
        {
            player.actions--;
            player.stun = false;
        }
    }

    public override void Update()
    {
        if (player.name.Equals("Gaarkhan"))
        {
            owner.gameUI.transform.GetChild(0).GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = player.health.ToString();
            owner.gameUI.transform.GetChild(0).GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = player.endurance.ToString();
            string move = string.Format(player.movement + " / " + player.MaxMovemment);
            owner.gameUI.transform.GetChild(0).GetChild(6).GetComponent<TMPro.TextMeshProUGUI>().text = move;
        }
    }

    public override void Exit()
    {
        // Disable UI
        owner.gameUI.transform.GetChild(0).gameObject.SetActive(false);
    }

}
