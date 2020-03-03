using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RestState : GameState
{
    Unit player;
    GameObject restUI;

    public override void Enter()
    {
        if (owner.gameType == 1)
        {
            player = owner.selectedUnit.GetComponent<Unit>();
            Debug.Log("Entered " + player.name + "Rest state");
        }
        else
        {
            player = map.SelectedUnit.GetComponent<Unit>();
            Debug.Log("Entered " + player.name + "Rest state");
        }

        restUI = owner.gameUI.transform.GetChild(3).gameObject;

        restUI.transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = player.health.ToString();
        restUI.transform.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = player.endurance.ToString();
        restUI.transform.GetChild(6).GetComponent<TMPro.TextMeshProUGUI>().text = player.movement.ToString();
        restUI.transform.GetChild(3).GetComponent<Image>().sprite = player.playerCard;
        restUI.SetActive(true);
        restUI.GetComponent<Image>().color = Color.clear;
        restUI.transform.GetChild(0).gameObject.SetActive(false);
        restUI.transform.GetChild(1).gameObject.SetActive(false);
        restUI.transform.GetChild(2).gameObject.SetActive(false);


        if (player.health == player.maxHealth && player.endurance == player.maxEndurance)
        {
            // Ask to Heal
            StartCoroutine(heal());
        }
        else
        {
            // Restore player endurance;
            StartCoroutine(addEndurance());
        }

    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        restUI.SetActive(false);
    }

    IEnumerator addEndurance()
    {

        for (int i = 0; i < player.maxEndurance; i++)
        {
            yield return new WaitForSeconds(1);
            if (player.endurance < player.maxEndurance)
            {
                player.endurance++;
                restUI.transform.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = player.endurance.ToString();
                // add increment audio
            }
            else if (player.health < player.maxHealth)
            {
                player.health++;
                restUI.transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = player.health.ToString();
                // add increment audio
            }
        }

        restUI.GetComponent<Image>().color = Color.white;
        restUI.transform.GetChild(0).gameObject.SetActive(true);
        restUI.transform.GetChild(1).gameObject.SetActive(true);
        restUI.transform.GetChild(2).gameObject.SetActive(true);

        // option to use medpack, which heals 5 health
        if (owner.items.ContainsKey("medpack") && owner.items["medpack"] > 0)
        {
            int amount = owner.items["medpack"];
            string text = String.Format("\nYou have " + amount + " Medpacks.\nUse one to recover 5 health ? ");
            owner.gameUI.GetComponent<GameUI>().changeRestText(text);
            restUI.SetActive(true);
        }
        else
        {
            changeTo();
        }
    }

    IEnumerator heal()
    {
        restUI.GetComponent<Image>().color = Color.white;
        restUI.transform.GetChild(0).gameObject.SetActive(true);
        restUI.transform.GetChild(1).gameObject.SetActive(true);
        restUI.transform.GetChild(2).gameObject.SetActive(true);

        // option to use medpack, which heals 5 health
        if (owner.items.ContainsKey("medpack") && owner.items["medpack"] > 0)
        {
            int amount = owner.items["medpack"];
            string text = String.Format("\nYou have " + amount + " Medpacks.\nUse one to recover 5 health ? ");
            owner.gameUI.GetComponent<GameUI>().changeRestText(text);
            restUI.SetActive(true);
        }
        else
        {
            changeTo();
        }

        yield return null;
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
