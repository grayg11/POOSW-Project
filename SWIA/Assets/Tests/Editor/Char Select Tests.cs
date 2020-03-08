using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;


public class CharSelectTests : MonoBehaviour
{
    [Test]
    public void SceneChangesWhenPartyFull()
    {
        var test = new GameObject().AddComponent<CharacterSelection>();

        string sceneName = SceneManager.GetActiveScene().name;
        test.AddToParty();
        test.AddToParty();
        test.AddToParty();
        test.AddToParty();

        Assert.AreNotEqual(sceneName, SceneManager.GetActiveScene().name);

    }

    [Test]
    public void SceneDoesntChangeWhenPartyNotFull()
    {
        var test = new GameObject().AddComponent<CharacterSelection>();

        string sceneName = SceneManager.GetActiveScene().name;
        test.AddToParty();

        Assert.AreEqual(sceneName, SceneManager.GetActiveScene().name);

        test.AddToParty();

        Assert.AreEqual(sceneName, SceneManager.GetActiveScene().name);

        test.AddToParty();

        Assert.AreEqual(sceneName, SceneManager.GetActiveScene().name);

    }

    [Test]
    public void nextButtonChangesSelection()
    {
        var test = new GameObject().AddComponent<CharacterSelection>();

        var image1 = test.leftButton.image;
        var image2 = test.rightButton.image;

        test.Next();

        Assert.AreNotEqual(image1, test.leftButton.image);
        Assert.AreNotEqual(image2, test.rightButton.image);
    }

    [Test]
    public void previousButtonChangesSelection()
    {
        var test = new GameObject().AddComponent<CharacterSelection>();

        var image1 = test.leftButton.image;
        var image2 = test.rightButton.image;

        test.Prev();

        Assert.AreNotEqual(image1, test.leftButton.image);
        Assert.AreNotEqual(image2, test.rightButton.image);
    }
}