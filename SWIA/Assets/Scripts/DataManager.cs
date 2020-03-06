using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public GameStateController GSC;

    // Start is called before the first frame update
    void Start()
    {
        //GSC = FindObjectOfType<GameStateController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int mapSizeX;
    public int mapSizeY;
    public float minX, minY, maxX, maxY;
    [SerializeField]public int[,] tiles;
    public int tilesLen;
    public Dictionary<Vector2, GameObject> allTiles;
    public int allTilesLen;

    public List<WeaponType> allWeapons;
    public UnitType[] heroTypes;    // Diala, Fenn, Garkhaan, Gideon, Jyn, Mak
    public UnitType[] enemyTypes;   // StormTrooper
    public DieTypes[] dice;         // red - 0, yellow - 1, green - 2, blue - 3, black - 4, white - 5
    public Sprite[] conditions;
    public GameObject moveTileVisualPrefab;
    public Material moveTileSelected;
    public Material moveTileUnselected;
    public Material attackTile;

    public GameObject attackTileVisualPrefab;
    public GameObject clickedTile;

    public GameObject clickablePrefab;
    public Dictionary<Vector2, ClickableTile> clickables;
    public int cliackablesLen;
    public Dictionary<Vector2, GameObject> moveSpaces;
    public Dictionary<Vector2, GameObject> attackSpaces;

    public int enemyTurnCount = 0;
    public GameObject enemyTarget = null;

    // create an ObjectiveType class
    public GameObject[] objectives;

    public void createHeroes()
    {
        for (int i = 0; i < 4; i++)
        {
            UnitType ut;
            //if (GSC.gameType == 1)
                ut = heroTypes[i]; // unitTypes[CharacterSelection.party[i]];
            //else
            //    ut = heroTypes[CharacterSelection.party[i]];
            Vector3 position = GSC.generator.playerSpawns[i] + ut.tileVisualPrefab.transform.position;
            GameObject go = Instantiate(ut.tileVisualPrefab, position, ut.tileVisualPrefab.transform.rotation);
            go.GetComponent<Unit>().map = GSC.map;
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
            GSC.heroes.Add(go);
        }
    }

    public void createEnemies()
    {
        int amount = startingEnemies();
        List<int> prevSpawns = new List<int>();
        for (int i = 0; i < amount; i++)
        {
            // add weights for tiered enemy spawns
            int type = pickEnemy();
            UnitType ut = enemyTypes[type];
            bool newSpawn = false;
            int sp = Random.Range(0, GSC.generator.enemySpawns.Count - 1);

            while (!newSpawn)
            {
                if (!prevSpawns.Contains(sp))
                {
                    newSpawn = true;
                    prevSpawns.Add(sp);
                }
                else
                {
                    sp = Random.Range(0, GSC.generator.enemySpawns.Count - 1);
                }
            }

            Vector3 position = GSC.generator.enemySpawns[sp] + ut.tileVisualPrefab.transform.position;
            GameObject go = Instantiate(ut.tileVisualPrefab, position, ut.tileVisualPrefab.transform.rotation);
            go.GetComponent<Unit>().map = GSC.map;
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
            GSC.enemies.Add(go);
        }

    }

    public void createObjectives()
    {
        int max = GSC.generator.enemySpawns.Count;
        int val;

        if (GSC.difficulty == 0)        // easy
        {
            val = Mathf.RoundToInt(Random.Range(.4f * max, .5f * max));
        }
        else if (GSC.difficulty == 1)   // normal
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
            int sp = Random.Range(0, GSC.generator.objectiveSpawns.Count - 1);
            Vector3 position = GSC.generator.objectiveSpawns[sp];
            GSC.generator.objectiveSpawns.RemoveAt(sp);
            position += randObj.transform.position;
            GameObject crateGo = Instantiate(randObj, position, randObj.transform.rotation);
        }
    }

    int startingEnemies()
    {
        int max = GSC.generator.enemySpawns.Count;
        int val;

        if (GSC.difficulty == 0) // easy
        {
            val = Random.Range(2,4);
        }
        else if (GSC.difficulty == 1) // normal
        {
            val = Random.Range(3, 5);
        }
        else // hard
        {
            val = Random.Range(4, 7);
        }

        if (val > max)
            val = max;
        return val;
    }

    int pickEnemy()
    {
        int val = 0;

        float f = Random.Range(0f, 1f);
        bool elite = false;

        if (GSC.difficulty == 0)
        {
            if (f > .9)
                elite = true;
        }
        else if (GSC.difficulty == 1)
        {
            if (f > .8)
                elite = true;
        }
        else
        {
            if (f > .7)
                elite = true;
        }

        if (elite)
        {
            val = Random.Range(0, GSC.data.enemyTypes.Length);
            if (val == 0)
                val = 1;
            else if (val % 2 == 0)
                val -= 1;
        }
        else
        {
            val = Random.Range(0, GSC.data.enemyTypes.Length);
            if (val % 2 != 0)
                val -= 1;
        }

        return val;
    }
}
