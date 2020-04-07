using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour
{
    MouseBehavior mouseBehavior;
    public GameStateController GSC;
    public Unit player;

    public TMPro.TextMeshProUGUI restText;
    public TMPro.TextMeshProUGUI endText;

    public AudioManager audioManager;

    // Start is called before the first frame update
    public void Start()
    {
        GSC = GameObject.FindObjectOfType<GameStateController>();
        //mouseBehavior = GameObject.FindObjectOfType<MouseBehavior>();
        
        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("NO AUDIO MANAGER FOUND IN SCENE");
        }
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void selectPlayer(int i)
    {
        if (GSC.gameType == 1)
        {
            GSC.selectedUnit = GSC.heroes[i];
            player = GSC.selectedUnit.GetComponent<Unit>();
            Debug.Log(player.name + " Unit# " + player.unit);
        }
        else
        {
            // Delete later
            GSC.map.SelectedUnit = GSC.heroes[i];
            player = GSC.map.SelectedUnit.GetComponent<Unit>();
        }


        findNextState();
    }

    public void moveButton()
    {
        audioManager.Play("MenuInteract");
        if (player.actions == 2 || (player.actions == 1 && player.movement <= 0))
            player.movement = player.MaxMovemment;

        if (player.movement == player.MaxMovemment)
            player.actions--;

        GSC.gameUI.transform.GetChild(4).gameObject.SetActive(false);
        GSC.ChangeState<MoveState>();
    }

    public void confirmMove()
    {
        audioManager.Play("MenuInteract");
        player.NextTurn();

        if (player.actions <= 0 && (player.movement <= 0 || player.movement == player.MaxMovemment))
            GSC.ChangeState<PlayerEndState>();
        else
            findNextState();
    }

    public void Rest()
    {
        player.actions--;
        GSC.ChangeState<RestState>();
    }

    public void changeRestText(string text)
    {
        restText.text = text;
    }

    public void attackButton()
    {
        player.actions--;
        GSC.ChangeState<AttackState>();
    }


    public void Heal()
    {
        audioManager.Play("MenuInteract");
        if (player.health < player.maxHealth - 5)
            player.health += 5;
        else
            player.health = player.maxHealth;

        if (GSC.items["medpack"] > 0)
            GSC.items["medpack"]--;

        GSC.gameUI.transform.GetChild(3).gameObject.SetActive(false);

        if (player.actions <= 0 && (player.movement <= 0 || player.movement == player.MaxMovemment))
            GSC.ChangeState<PlayerEndState>();
        else
            findNextState();
    }

    public void dontHeal()
    {
        audioManager.Play("MenuInteract");
        GSC.gameUI.transform.GetChild(3).gameObject.SetActive(false);

        if (player.actions <= 0 && (player.movement <= 0 || player.movement == player.MaxMovemment))
            GSC.ChangeState<PlayerEndState>();
        else
            findNextState();
    }

    public void dontMove()
    {
        audioManager.Play("MenuInteract");
        player.movement = 0;
        GSC.gameUI.transform.GetChild(4).gameObject.SetActive(false);
        GSC.ChangeState<PlayerEndState>();
    }

    public void endTurn()
    {
        audioManager.Play("MenuInteract");
        if (player.movement < player.MaxMovemment)
        {
            endText.text = String.Format("\nYou have " + player.movement + " Remaining Movement.\nEnd Turn Without Using? ");
            GSC.gameUI.transform.GetChild(4).gameObject.SetActive(true);
        }
        else
            dontMove();
    }


    public void actionButton()
    {
        GSC.ChangeState<ActionState>();
    }

    public void findNextState()
    {
        if (GSC.gameType == 1)
        {
            if (player.unit == 0)
            {
                GSC.ChangeState<DialaState>();
            }
            if (player.unit == 1)
            {
                GSC.ChangeState<FennState>();
            }
            if (player.unit == 2)
            {
                GSC.ChangeState<GaarkhanState>();
            }
            if (player.unit == 3)
            {
                GSC.ChangeState<GideonState>();
            }
            if (player.unit == 4)
            {
                GSC.ChangeState<JynState>();
            }
            if (player.unit == 5)
            {
                GSC.ChangeState<MakState>();
            }
        }
        else
        {
            if (GSC.map.SelectedUnit.name.Equals("Diala"))
            {
                GSC.ChangeState<DialaState>();
            }
            if (GSC.map.SelectedUnit.name.Equals("Fenn"))
            {
                GSC.ChangeState<FennState>();
            }
            if (GSC.map.SelectedUnit.name.Equals("Gaarkhan"))
            {
                GSC.ChangeState<GaarkhanState>();
            }
            if (GSC.map.SelectedUnit.name.Equals("Gideon"))
            {
                GSC.ChangeState<GideonState>();
            }
            if (GSC.map.SelectedUnit.name.Equals("Jyn"))
            {
                GSC.ChangeState<JynState>();
            }
            if (GSC.map.SelectedUnit.name.Equals("Mak"))
            {
                GSC.ChangeState<MakState>();
            }
        }

    }
}
