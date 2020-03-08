using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;


public class DataManagerTests : MonoBehaviour
{
    [Test]
    public void sceneChangesWhenPartyFull()
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
    public void HeroesCreated()
    {
        var test = new GameObject().AddComponent<DataManager>();

        var heroes = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            UnitType ut = test.heroTypes[i];
            Vector3 position = test.GSC.generator.playerSpawns[i] + ut.tileVisualPrefab.transform.position;
            GameObject go = Instantiate(ut.tileVisualPrefab, position, ut.tileVisualPrefab.transform.rotation);
            go.GetComponent<Unit>().map = test.GSC.map;
            go.GetComponent<Unit>().unit = i;
            go.GetComponent<Unit>().tileX = (int)position.x;
            go.GetComponent<Unit>().tileY = (int)position.y;
            go.GetComponent<Unit>().maxHealth = go.GetComponent<Unit>().health = ut.health;
            go.GetComponent<Unit>().maxEndurance = go.GetComponent<Unit>().endurance = ut.endurance;
            go.GetComponent<Unit>().MaxMovemment = ut.movement;
            go.GetComponent<Unit>().defDice = ut.defDice;
            go.GetComponent<Unit>().strength = ut.strength;
            go.GetComponent<Unit>().insight = ut.insight;
            go.GetComponent<Unit>().tech = ut.tech;
            go.GetComponent<Unit>().playerImage = ut.playerImage;
            go.GetComponent<Unit>().playerCard = ut.playerCard;
            go.GetComponent<Unit>().weapon = ut.weapon;
            go.name = ut.name;
            heroes.Add(go);
        }

        test.createHeroes();
        Assert.AreEqual(heroes, test.GSC.heroes);
    }

    [Test]
    public void enemiesCreated()
    {
        var test = new GameObject().AddComponent<DataManager>();

        test.createEnemies();
        Assert.AreNotEqual(test.GSC.enemies.Count, 0);
    }

    [Test]
    public void objectivesCreated()
    {
        var test = new GameObject().AddComponent<DataManager>();

        test.createObjectives();
        Assert.AreEqual(0, test.GSC.generator.objectiveSpawns.Count);
        Assert.AreEqual(0, test.objectives.Length);
    }

    [Test]
    public void enemiesStarted()
    {
        var test = new GameObject().AddComponent<DataManager>();

        int max = test.GSC.generator.enemySpawns.Count;
        int val = test.startingEnemies();
        
        if (test.GSC.difficulty == 0) // easy
        {
            Assert.AreEqual((val == max || (val >= 2 && val <= 4)), true);
        }
        else if (test.GSC.difficulty == 1) // normal
        {
            Assert.AreEqual((val == max || (val >= 3 && val <= 5)), true);
        }
        else // hard
        {
            Assert.AreEqual((val == max || (val >= 4 && val <= 7)), true);
        }
    }

    [Test]
    public void enemyPicked()
    {
        var test = new GameObject().AddComponent<DataManager>();
        Assert.AreEqual((test.pickEnemy() > 0), true);
    }
}