using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;


public class GameStateControllerTests : MonoBehaviour
{
    [Test]
    public void GameStateControllerStarted()
    {
        var test = new GameObject().AddComponent<GameStateController>();

        test.Start();
        Assert.AreEqual(test.activated, new int[4]);
        Assert.AreEqual(test.heroes, new List<GameObject>());
        Assert.AreEqual(test.enemies, new List<GameObject>());
        Assert.AreEqual(test.map, FindObjectOfType<TileMap>());
        Assert.AreEqual(test.cControl, FindObjectOfType<CombatController>());
        Assert.AreEqual(test.items, new Dictionary<string, int>());
        Assert.AreEqual(test.gameType, mainmenu.gameType);
        Assert.AreEqual(test.difficulty, mainmenu.gameDifficulty);
    }
}