using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class EnemyTests
{
    DataManager test;
    GameStateController GSC;

    public void testSetup()
    {
        GSC = new GameObject().AddComponent<GameStateController>();
        test = new GameObject().AddComponent<DataManager>();

        test.createEnemies();
    }


    [Test]
    public void isHealthStatCorrect()
    {
        for (int i = 0; i < 4; i++)
        {
            Assert.AreEqual(GSC.enemies[i].GetComponent<Unit>().maxHealth, GSC.data.heroTypes[i].health);
        }
    }


    [Test]
    public void isSpeedStatCorrect()
    {
        for (int i = 0; i < 4; i++)
        {
            Assert.AreEqual(GSC.heroes[i].GetComponent<Unit>().MaxMovemment, GSC.data.heroTypes[i].movement);
        }
    }

    [Test]
    public void isDefenseDiceCorrect()
    {
        for (int i = 0; i < 4; i++)
        {
            Assert.AreEqual(GSC.heroes[i].GetComponent<Unit>().defDice, GSC.data.heroTypes[i].defDice);
        }
    }

    [Test]
    public void isWeaponCorrect()
    {
        for (int i = 0; i < 4; i++)
        {
            Assert.AreEqual(GSC.heroes[i].GetComponent<Unit>().weapon, GSC.data.heroTypes[i].weapon);
        }
    }


    [Test]
    public void isImageCorrect()
    {
        for (int i = 0; i < 4; i++)
        {
            Assert.AreEqual(GSC.heroes[i].GetComponent<Unit>().playerImage, GSC.data.heroTypes[i].playerImage);
        }
    }

    [Test]
    public void isCardCorrect()
    {
        for (int i = 0; i < 4; i++)
        {
            Assert.AreEqual(GSC.heroes[i].GetComponent<Unit>().playerCard, GSC.data.heroTypes[i].playerCard);
        }
    }

    [Test]
    public void isUnitCorrect()
    {
        for (int i = 0; i < 4; i++)
        {
            Assert.AreEqual(GSC.heroes[i].GetComponent<Unit>().unit, i);
        }
    }

}
