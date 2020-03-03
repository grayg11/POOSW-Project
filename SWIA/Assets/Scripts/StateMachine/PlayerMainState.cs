using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMainState : GameState
{
    GameObject uiGO;
    public Vector3 playerPos;

    public override void Enter()
    {
        Debug.Log("Entered player main state");
        // choose the player 
        uiGO = owner.gameUI.transform.GetChild(1).gameObject;
        uiGO.SetActive(true);

        owner.gameType = 1;
        Debug.Log("Game Type: " + owner.gameType);

        CheckGameOver();

        if (!allActivated())
        {
            for (int i = 0; i < 4; i++)
            {
                if (owner.activated[i] == 1 || owner.activated[i] == -1)
                {
                    // disable button
                    uiGO.transform.GetChild(i).GetComponent<Image>().sprite = owner.heroes[i].GetComponent<Unit>().playerImage;
                    uiGO.transform.GetChild(i).GetComponent<Button>().interactable = false;
                }
                else
                {
                    uiGO.transform.GetChild(i).GetComponent<Image>().sprite = owner.heroes[i].GetComponent<Unit>().playerImage;
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (owner.activated[i] == 1)
                {
                    uiGO.transform.GetChild(i).GetComponent<Image>().sprite = owner.heroes[i].GetComponent<Unit>().playerImage;
                    uiGO.transform.GetChild(i).GetComponent<Button>().interactable = true;
                    owner.activated[i] = 0;
                }
                else
                {
                    uiGO.transform.GetChild(i).GetComponent<Image>().sprite = owner.heroes[i].GetComponent<Unit>().playerImage;
                    uiGO.transform.GetChild(i).GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        uiGO.SetActive(false);
    }

    bool allActivated()
    {
        foreach (int hero in owner.activated)
        {
            if (hero == 0)
                return false;
        }

        return true;
    }

    public void CheckGameOver()
    {
        int deathCount = 0;

        for (int i = 0; i < 4; i++)
        {
            if (owner.activated[i] == -1)
            {
                deathCount++;
            }
        }

        if (deathCount == 4)
            SceneManager.LoadScene("GameOver");
    }

}
