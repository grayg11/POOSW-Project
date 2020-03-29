using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUITests
{
    [Test]
    public void HasStarted()
    {
        var test = GameObject().AddComponent<GameUI>();

        test.Start();

        Assert.AreEqual((test.GSC != null), true);
        Assert.AreEqual((test.audioManager != null), true);
    }

    [Test]
    public void HasUpdated()
    {
        // Update function is empty
    }

    [Test]
    public void HasSelectedPlayer()
    {
        var test = GameObject().AddComponent<GameUI>();

        if (test.GSC.gameType == 1)
        {
            for (int i = 0; i < GameStateController.heroes.capacity)
            {
                test.selectPlayer(i);
                Assert.AreEqual(test.GSC.selectedUnit, test.GSC.heroes(i));
            }
        }
    }

    [Test]
    public void HasMovedButton()
    {
        var test = GameObject().AddComponent<GameUI>();
    }

    [Test]
    public void HasConfirmedMove()
    {
        var test = GameObject().AddComponent<GameUI>();
    }

    [Test]
    public void HasRested()
    {
        var test = GameObject().AddComponent<GameUI>();
    }

    [Test]
    public void HasChangedRestText()
    {
        var test = GameObject().AddComponent<GameUI>();
    }

    [Test]
    public void AttackButtonAction()
    {
        var test = GameObject().AddComponent<GameUI>();
    }

    [Test]
    public void HasHealed()
    {
        var test = GameObject().AddComponent<GameUI>();
    }

    [Test]
    public void HasUpdated()
    {
        var test = GameObject().AddComponent<GameUI>();
    }

    [Test]
    public void HasNotHealed()
    {
        var test = GameObject().AddComponent<GameUI>();
    }

    [Test]
    public void HasRestHealed()
    {
        var test = GameObject().AddComponent<GameUI>();
    }

    [Test]
    public void HasNotMoved()
    {
        var test = GameObject().AddComponent<GameUI>();
    }

    [Test]
    public void HasEndedTurn()
    {
        var test = GameObject().AddComponent<GameUI>();
    }

    [Test]
    public void ActionButtonAction()
    {
        var test = GameObject().AddComponent<GameUI>();
    }

    [Test]
    public void HasFoundNextState()
    {
        var test = GameObject().AddComponent<GameUI>();
    }
}
