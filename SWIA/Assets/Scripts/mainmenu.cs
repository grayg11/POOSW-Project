﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class mainmenu : MonoBehaviour
{
    void Start()
    {
     
    }

    public Button gamemodeBut;
    public Button loadGameBut;
    public Button rulesBut;
    public Button exitGameBut;
    public Button skirmishBut;
    public Button gameModeBackBut;
    public Button sEasy;
    public Button sNorm;
    public Button sHard;
    public TMPro.TextMeshProUGUI sDiff;
    public Button campaignBut;
    public Button cEasy;
    public Button cNorm;
    public Button cHard;
    public TMPro.TextMeshProUGUI cDiff;
    public Button RNGBut;

    public GameObject skirmishPanel;
    public static int gameDifficulty;
    public GameObject campaignPanel;
    public TMPro.TextMeshProUGUI campaignText;
    public static string levelName = "Skirmish1";
    public string lvlName;
    public GameObject raidPanel;
    public static int gameType;


    public void gameMode()
    {
        gamemodeBut.transform.gameObject.SetActive(false);
        loadGameBut.gameObject.SetActive(false);
        rulesBut.gameObject.SetActive(false);
        exitGameBut.gameObject.SetActive(false);
        skirmishBut.gameObject.SetActive(true);
        campaignBut.gameObject.SetActive(true);
        RNGBut.gameObject.SetActive(true);
        gameModeBackBut.gameObject.SetActive(true);
    }

    public void gameModeBack()
    {
        gamemodeBut.transform.gameObject.SetActive(true);
        loadGameBut.gameObject.SetActive(true);
        rulesBut.gameObject.SetActive(true);
        exitGameBut.gameObject.SetActive(true);
        skirmishBut.gameObject.SetActive(false);
        campaignBut.gameObject.SetActive(false);
        RNGBut.gameObject.SetActive(false);
        gameModeBackBut.gameObject.SetActive(false);
    }

    public void loadGame()
    {

    }

    public void rules()
    {

    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void skirmish()
    {
        skirmishBut.gameObject.SetActive(false);
        campaignBut.gameObject.SetActive(false);
        gameModeBackBut.gameObject.SetActive(false);
        RNGBut.gameObject.SetActive(false);
        skirmishPanel.SetActive(true);
    }

    public void skirmishSelect(int difficulty)
    {
        gameDifficulty = difficulty;

        if (difficulty == 0)
        {
            sDiff.text = "Easy";
            sDiff.color = new Color(0, 217, 73, 255);
        }
        else if (difficulty == 1)
        {
            sDiff.text = "Normal";
            sDiff.color = new Color(255, 255, 0, 255);
        }
        else if (difficulty == 2)
        {
            sDiff.text = "Hard";
            sDiff.color = new Color(255, 0, 0, 255);
        }
        else
            sDiff.text = "";
    }

    public void skirmishChoser(int number)
    {
        if (number == 0)
        {
            campaignText.text = string.Format("This is the description for s1");
            levelName = "Skirmish1";
        }
        else if (number == 1)
        {
            campaignText.text = string.Format("This is the description for s2");
            levelName = "Skirmish2";
        }
        else
        {
            campaignText.text = string.Format("This is the description for s3");
            levelName = "Skirmish3";
        }
    }

    public void skirmishStart()
    {
        gameType = 0;
        Debug.Log("Skirmish Diff " + gameDifficulty);
        SceneManager.LoadScene("Character Select");
    }

    public void skirmishBack()
    {
        skirmishSelect(-1);
        skirmishBut.gameObject.SetActive(true);
        campaignBut.gameObject.SetActive(true);
        RNGBut.gameObject.SetActive(true);
        gameModeBackBut.gameObject.SetActive(true);
        skirmishPanel.SetActive(false);
    }
    public void campaign()
    {
        skirmishBut.gameObject.SetActive(false);
        campaignBut.gameObject.SetActive(false);
        gameModeBackBut.gameObject.SetActive(false);
        RNGBut.gameObject.SetActive(false);
        campaignPanel.SetActive(true);
    }

    public void campaignSelect(int difficulty)
    {
        gameDifficulty = difficulty;

        if (difficulty == 0)
        {
            cDiff.text = "Easy";
            cDiff.color = new Color(0, 217, 73, 255);
        }
        else if (difficulty == 1)
        {
            cDiff.text = "Normal";
            cDiff.color = new Color(255, 255, 0, 255);
        }
        else if (difficulty == 2)
        {
            cDiff.text = "Hard";
            cDiff.color = new Color(255, 0, 0, 255);
        }
        else
            cDiff.text = "";

    }

    public void campaignChoser(int number)
    {
        if (number == 0)
        {
            campaignText.text = string.Format("This is the description for c1");
            lvlName = levelName = "Campaign1Level1";
        }
        else if (number == 1)
        {
            campaignText.text = string.Format("This is the description for c2");
            lvlName = levelName = "Campaign2Level1";
        }
        else
        {
            campaignText.text = string.Format("This is the description for c3");
            lvlName = levelName = "Campaign3Level1";
        }
    }

    public void campaignStart()
    {
        gameType = 2;
        Debug.Log("Campaign Diff " + gameDifficulty);
        SceneManager.GetSceneByName("Character Select");
    }

    public void campaignBack()
    {
        campaignSelect(0);
        skirmishBut.gameObject.SetActive(true);
        campaignBut.gameObject.SetActive(true);
        RNGBut.gameObject.SetActive(true);
        gameModeBackBut.gameObject.SetActive(true);
        campaignPanel.SetActive(false);
    }

    public void raid()
    {

    }
}
