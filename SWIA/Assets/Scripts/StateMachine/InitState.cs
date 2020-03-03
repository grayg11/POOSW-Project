using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitState : GameState
{

    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());     
    }

    IEnumerator Init()
    {

        if (owner.gameType == 1)
        {
            Debug.Log("RAID mode");

            // raid 
            owner.generator.init();
            owner.generator.generateMap();
            owner.generator.generateMapData();
            owner.path.GeneratePathfindingGraph();
            owner.data.createHeroes();
            owner.data.createEnemies();
            owner.data.createObjectives();
        }
        else
        {
            Debug.Log("SKIRMISH mode");

            // skirmish
            owner.generator.init();
            owner.generator.generateMap();
            owner.generator.generateMapData();
            owner.path.GeneratePathfindingGraph();
            owner.data.createHeroes();
            owner.data.createEnemies();
            owner.data.createObjectives();

            //map.GenerateMapData();
            //Debug.Log("Map data");
            //map.GenerateMapVisual();
            //Debug.Log("Map visual");
            //map.GeneratePathfindingGraph();
            //Debug.Log("pathfinding");
            //map.GenerateUnits();
            //Debug.Log("gen units");
            //owner.heroes = map.heroes;
            //owner.enemies = map.enemies;
        }
        
        yield return null;
        Debug.Log("Entering Player Main State");
        owner.ChangeState<PlayerMainState>();
    }

}