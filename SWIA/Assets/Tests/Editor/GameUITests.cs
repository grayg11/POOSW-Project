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
        var health = test.player.health;
        var numMedpackItems = test.GSC.items["medpack"];

        test.Heal();
        if (health < test.player.maxHealth - 5)
            health += 5;
        else
            health = test.player.maxHealth;
        Assert.AreEqual(health, test.player.health);

        Assert.AreEqual(test.GSC.gameUI.transform.GetChild(3).gameObject.activeSelf, false);

        if (test.player.actions <= 0 && (test.player.movement <= 0 || test.player.movement == test.player.MaxMovemment))
            Assert.AreEqual(test.GSC.currentState, PlayerEndState);
    }

    [Test]
    public void HasNotHealed()
    {
        var test = GameObject().AddComponent<GameUI>();

        test.dontHeal();

        Assert.AreEqual(test.GSC.gameUI.transform.GetChild(3).gameObject.activeSelf, false);

        if (test.player.actions <= 0 && (test.player.movement <= 0 || test.player.movement == test.player.MaxMovemment))
            Assert.AreEqual(test.GSC.currentState, PlayerEndState);
    }

    [Test]
    public void HasNotMoved()
    {
        var test = GameObject().AddComponent<GameUI>();

        test.dontMove();

        Assert.AreEqual(test.player.movement, 0);
        Assert.AreEqual(test.GSC.gameUI.transform.GetChild(4).gameObject.activeSelf, false);
        Assert.AreEqual(test.GSC.currentState, PlayerEndState);
    }

    [Test]
    public void HasEndedTurn()
    {
        var test = GameObject().AddComponent<GameUI>();

        test.endTurn();

        if (test.player.movement < test.player.MaxMovemment)
        {
            Assert.AreEqual(test.endText.text, String.Format("\nYou have " + test.player.movement + " Remaining Movement.\nEnd Turn Without Using? "));
            Assert.AreEqual(test.GSC.gameUI.transform.GetChild(4).gameObject.activeSelf, true);
        }
    }

    [Test]
    public void ActionButtonAction()
    {
        var test = GameObject().AddComponent<GameUI>();

        test.actionButton();

        Assert.AreEqual(test.GSC.currentState, ActionState);
    }

    [Test]
    public void HasFoundNextState()
    {
        var test = GameObject().AddComponent<GameUI>();
        var gameType = test.GSC.gameType;
        var unit = test.player.unit;
        var name = test.GSC.map.SelectedUnit.name;
        
        test.findNextState();

        if (gameType == 1)
        {
            if (unit == 0)
            {
                Assert.AreEqual(test.GSC.currentState, DialaState);
            }
            if (unit == 1)
            {
                Assert.AreEqual(test.GSC.currentState, FennState);
            }
            if (unit == 2)
            {
                Assert.AreEqual(test.GSC.currentState, GaarkhanState);
            }
            if (tunit == 3)
            {
                Assert.AreEqual(test.GSC.currentState, GideonState);
            }
            if (unit == 4)
            {
                Assert.AreEqual(test.GSC.currentState, JynState);
            }
            if (unit == 5)
            {
                Assert.AreEqual(test.GSC.currentState, MakState);
            }
        }
        else
        {
            if (name.Equals("Diala"))
            {
                Assert.AreEqual(test.GSC.currentState, DialaState);
            }
            if (name.Equals("Fenn"))
            {
                Assert.AreEqual(test.GSC.currentState, FennState);
            }
            if (name.Equals("Gaarkhan"))
            {
                Assert.AreEqual(test.GSC.currentState, GaarkanState);
            }
            if (name.Equals("Gideon"))
            {
                Assert.AreEqual(test.GSC.currentState, GideonState);
            }
            if (name.Equals("Jyn"))
            {
                Assert.AreEqual(test.GSC.currentState, JynState);
            }
            if (name.Equals("Mak"))
            {
                Assert.AreEqual(test.GSC.currentState, MakState);
            }
        }
    }
}
