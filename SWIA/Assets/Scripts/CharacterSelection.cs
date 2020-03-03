using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public Button prevButton;
    public Button nextButton;
    public Image[] partypics;
    int partyint;
    public GameObject charZoom;
    public int selectedPlayer;
    public List<Sprite> playerSprites;
    public List<Sprite> playerSelSprites;
    public static List<int> party;
    
    private AudioManager audioManager;
    
    int len;
    int nextSprite;
    int prevSprite;
    

    // Start is called before the first frame update
    void Start()
    {
        leftButton.image.sprite = playerSprites[0];
        rightButton.image.sprite = playerSprites[1];
        len = playerSprites.Count;
        nextSprite = 2;
        prevSprite = len - 1;
        selectedPlayer = 0;
        party = new List<int>();

        //caching
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("NO AUDIO MANAGER FOUND IN SCENE");
        }
    }

    void Update()
    {
        if (party.Count >= 4)
            SceneManager.LoadScene(mainmenu.levelName);
    }

    public void Next()
    {
        audioManager.Play("NextPrev");
        if (nextSprite >= len)
            nextSprite = 0;

        if (disableChar(nextSprite))
            leftButton.interactable = false;
        else
            leftButton.interactable = true;

        leftButton.image.sprite = playerSprites[nextSprite++];

        if (disableChar(nextSprite))
            rightButton.interactable = false;
        else
            rightButton.interactable = true;

        rightButton.image.sprite = playerSprites[nextSprite++];
    }

    public void Prev()
    {
        audioManager.Play("NextPrev");
        if (prevSprite < 0)
            prevSprite = (len - 1);

        if (disableChar(prevSprite))
            rightButton.interactable = false;
        else
            rightButton.interactable = true;

        rightButton.image.sprite = playerSprites[prevSprite--];

        if (disableChar(prevSprite))
            leftButton.interactable = false;
        else
            leftButton.interactable = true;

        leftButton.image.sprite = playerSprites[prevSprite--];
    }

    bool disableChar(int sprite)
    {
        if (party.Count > 0)
        {
            foreach (int i in party)
            {
                if (i == sprite)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void RightBut()
    {
        audioManager.Play("Select");
        for (int i = 0; i < len; i++)
        {
            if (rightButton.image.sprite.Equals(playerSprites[i]))
            {
                SelectedPlayer(i);
                return;
            }
        }
    }

    public void LeftBut()
    {
        audioManager.Play("Select");
        for (int i = 0; i < len; i++)
        {
            if (leftButton.image.sprite.Equals(playerSprites[i]))
            {
                SelectedPlayer(i);
                return;
            }
        }
    }

    private void SelectedPlayer(int i)
    {
        rightButton.enabled = false;
        leftButton.enabled = false;
        prevButton.enabled = false;
        nextButton.enabled = false;
        charZoom.GetComponent<Image>().sprite = playerSprites[i];
        selectedPlayer = i;
        charZoom.SetActive(true);
    }

    public void AddToParty()
    {
        audioManager.Play("Affirm");
        rightButton.enabled = true;
        leftButton.enabled = true;
        prevButton.enabled = true;
        nextButton.enabled = true;
        Color c = Color.white;
        partypics[partyint].color = c;
        partypics[partyint++].sprite = playerSelSprites[selectedPlayer];
        party.Add(selectedPlayer);
        charZoom.SetActive(false);
        if (rightButton.image.sprite.Equals(playerSprites[selectedPlayer]))
            rightButton.interactable = false;
        if (leftButton.image.sprite.Equals(playerSprites[selectedPlayer]))
            leftButton.interactable = false;
    }

    public void GoBack()
    {
        audioManager.Play("Negative");
        rightButton.enabled = true;
        leftButton.enabled = true;
        prevButton.enabled = true;
        nextButton.enabled = true;
        charZoom.SetActive(false);
    }

}
