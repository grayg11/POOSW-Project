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
    public void MoveButtonAction()
    {
        var test = GameObject().AddComponent<GameUI>();
        var actions = test.player.actions;

        test.moveButton();

        if (test.player.actions == 2 || (test.player.actions == 1 && test.player.movement <= 0))
            Assert.AreEqual(test.player.movement, test.player.MaxMovemment);

        if (test.player.movement == test.player.MaxMovemment)
            Assert.AreEqual(test.player.actions, actions - 1);

        Assert.AreEqual(test.GSC.CurrentState, MoveState);
    }

    [Test]
    public void HasConfirmedMove()
    {
        var test = GameObject().AddComponent<GameUI>();

        test.confirmMove();

        if (test.player.actions <= 0 && (test.player.movement <= 0 || test.player.movement == test.player.MaxMovemment))
            Assert.AreEqual(test.GSC.CurrentState, PlayerEndState);
    }

    [Test]
    public void HasRested()
    {
        var test = GameObject().AddComponent<GameUI>();
        var actions = test.player.actions;

        test.Rest();

        Assert.AreEqual(test.player.actions, actions - 1);
        Assert.AreEqual(test.GSC.currentState, RestState);
    }

    [Test]
    public void HasChangedRestText()
    {
        var test = GameObject().AddComponent<GameUI>();
        var testText = "test";
        test.changeRestText(testText);

        test.changeRestTest(testText);
        Assert.AreEqual(test.restText.text, testText);
    }

    [Test]
    public void AttackButtonAction()
    {
        var test = GameObject().AddComponent<GameUI>();
        var actions = test.player.actions;

        test.attackButton();

        Assert.AreEqual(test.player.actions, actions - 1);
        Assert.AreEqual(test.GSC.currentState, AttackState);
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
