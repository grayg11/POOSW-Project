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

        var enemies = new List<GameObject>();
        int amount = test.startingEnemies();
        List<int> prevSpawns = new List<int>();
        for (int i = 0; i < amount; i++)
        {
            // add weights for tiered enemy spawns
            int type = test.pickEnemy();
            UnitType ut = test.enemyTypes[type];
            bool newSpawn = false;
            int sp = Random.Range(0, test.GSC.generator.enemySpawns.Count - 1);

            while (!newSpawn)
            {
                if (!prevSpawns.Contains(sp))
                {
                    newSpawn = true;
                    prevSpawns.Add(sp);
                }
                else
                {
                    sp = Random.Range(0, test.GSC.generator.enemySpawns.Count - 1);
                }
            }

            Vector3 position = test.GSC.generator.enemySpawns[sp] + ut.tileVisualPrefab.transform.position;
            GameObject go = Instantiate(ut.tileVisualPrefab, position, ut.tileVisualPrefab.transform.rotation);
            go.GetComponent<Unit>().map = test.GSC.map;
            go.GetComponent<Unit>().unit = 6;
            go.GetComponent<Unit>().tileX = (int)position.x;
            go.GetComponent<Unit>().tileY = (int)position.y;
            go.GetComponent<Unit>().isMobile = ut.isMobile;
            go.GetComponent<Unit>().maxHealth = go.GetComponent<Unit>().health = ut.health;
            go.GetComponent<Unit>().maxEndurance = go.GetComponent<Unit>().endurance = ut.endurance;
            go.GetComponent<Unit>().movement = go.GetComponent<Unit>().MaxMovemment = ut.movement;
            go.GetComponent<Unit>().defDice = ut.defDice;
            go.GetComponent<Unit>().playerImage = ut.playerImage;
            go.GetComponent<Unit>().playerCard = ut.playerCard;
            go.GetComponent<Unit>().weapon = ut.weapon;
            go.name = ut.name;
            enemies.Add(go);
        }

        Assert.AreEqual(enemies, test.GSC.enemies);
    }

    [Test]
    public void objectivesCreated()
    {
        var test = new GameObject().AddComponent<DataManager>();

        List<Vector3> objectiveSpawns = test.GSC.generator.objectiveSpawns;
        GameObject[] objectives = test.objectives;

        int max = test.GSC.generator.enemySpawns.Count;
        int val;

        if (test.GSC.difficulty == 0)        // easy
        {
            val = Mathf.RoundToInt(Random.Range(.4f * max, .5f * max));
        }
        else if (test.GSC.difficulty == 1)   // normal
        {
            val = Mathf.RoundToInt(Random.Range(.25f * max, .4f * max));
        }
        else                            // hard
        {
            val = Mathf.RoundToInt(Random.Range(.1f * max, .3f * max));
        }

        if (val > max)
            val = max;
        for (int i = 0; i < val; i++)
        {
            GameObject randObj = objectives[Random.Range(0, objectives.Length)];
            int sp = Random.Range(0, objectiveSpawns.Count - 1);
            Vector3 position = objectiveSpawns[sp];
            objectiveSpawns.RemoveAt(sp);
            position += randObj.transform.position;
            GameObject crateGo = Instantiate(randObj, position, randObj.transform.rotation);
        }

        test.createObjectives();
        Assert.AreEqual(objectiveSpawns, test.GSC.generator.objectiveSpawns);
        Assert.AreEqual(objectives, test.objectives);
    }

    [Test]
    public void enemiesStarted()
    {

    }

    [Test]
    public void enemyPicked()
    {

    }
}