using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    //public GameObject mapPiece;
    public GameStateController GSC;
    UnityEngine.Object[] resources;
    GameObject[] forestTiles;
    GameObject[] desertTiles;
    GameObject[] baseTiles;
    public List<GameObject> spawnPoints;
    Dictionary<Vector2, GameObject> allTiles;
    public Dictionary<Vector2, GameObject> AllTiles { get => allTiles; private set => allTiles = value; }
    float minX, minY, maxX, maxY;
    int maxAttempts = 4;
    public GameObject[] forestEndCaps;
    public GameObject[] desertEndCaps;
    public GameObject[] baseEndCaps;
    public GameObject[] stationEndCaps;
    public List<Vector3> playerSpawns;
    public List<Vector3> enemySpawns;
    public List<Vector3> objectiveSpawns;
    public GameObject[] skirmishTiles;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

    }

    public void init()
    {
        allTiles = new Dictionary<Vector2, GameObject>();
        playerSpawns = new List<Vector3>();
        enemySpawns = new List<Vector3>();
        objectiveSpawns = new List<Vector3>();

        if (GSC.gameType == 1)
        {
            resources = Resources.LoadAll("Map Tiles/forest");
            forestTiles = new GameObject[resources.Length];
            for (int i = 0; i < resources.Length; i++)
            {
                forestTiles[i] = (GameObject)resources[i];
            }
            resources = null;

            resources = Resources.LoadAll("Map Tiles/desert");
            desertTiles = new GameObject[resources.Length];
            for (int i = 0; i < resources.Length; i++)
            {
                desertTiles[i] = (GameObject)resources[i];
            }
            resources = null;
        }

    }

    public void generateMap()
    {
        if (GSC.gameType == 0)
        {
            minX = int.MaxValue;
            minY = int.MaxValue;
            maxX = int.MinValue;
            maxY = int.MinValue;
            foreach (GameObject go in skirmishTiles)
            {
                if (go.GetComponent<Room>().minX < minX)
                    minX = go.GetComponent<Room>().minX;
                if (go.GetComponent<Room>().minY < minY)
                    minY = go.GetComponent<Room>().minY;
                if (go.GetComponent<Room>().maxX > maxX)
                    maxX = go.GetComponent<Room>().maxX;
                if (go.GetComponent<Room>().maxY > maxY)
                    maxY = go.GetComponent<Room>().maxY;

                foreach (KeyValuePair<Vector2, GameObject> tile in go.GetComponent<Room>().tiles)
                {
                    allTiles.Add(tile.Key, tile.Value);
                }

                GSC.data.allTiles = allTiles;
                GSC.data.allTilesLen = AllTiles.Count;
                collectSpawns(go);

            }
            return;
        }

        bool spawns = false;
        GameObject startTile = null;
        while (!spawns)
        {
            startTile = forestTiles[UnityEngine.Random.Range(0, forestTiles.Length)];
            spawns = startTile.GetComponent<Room>().playerSpawns;
        }
        startTile = Instantiate(startTile, new Vector3(0, 0, 0), startTile.transform.rotation);
        startTile.transform.SetParent(this.transform);
        minX = startTile.GetComponent<Room>().minX;
        minY = startTile.GetComponent<Room>().minY;
        maxX = startTile.GetComponent<Room>().maxX;
        maxY = startTile.GetComponent<Room>().maxY;

        foreach (KeyValuePair<Vector2, GameObject> tile in startTile.GetComponent<Room>().tiles)
        {
            allTiles.Add(tile.Key, tile.Value);
        }

        collectSpawns(startTile);

        spawnPoints = new List<GameObject>();
        addExits(startTile, -1);

        for (int i = 0; i < 30; i++)
        {
            GameObject sp = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count - 1)];
            spawnPoints.Remove(sp);

            addNextRoom(sp);
        }

        foreach (GameObject sp in spawnPoints)
        {
            endCap(sp);
        }

        GSC.data.allTiles = allTiles;
        GSC.data.allTilesLen = allTiles.Count;
    }


    void addExits(GameObject nextTile, int cameFrom)
    {

        GameObject sp = nextTile.GetComponent<Room>().northExit;
        if (sp != null && cameFrom != 1)
        {
            sp.name = nextTile.GetComponent<Room>().northExit.name;
            spawnPoints.Add(sp);
        }
        sp = nextTile.GetComponent<Room>().southExit;
        if (sp != null && cameFrom != 0)
        {
            sp.name = nextTile.GetComponent<Room>().southExit.name;
            spawnPoints.Add(sp);
        }
        sp = nextTile.GetComponent<Room>().eastExit;
        if (sp != null && cameFrom != 3)
        {
            sp.name = nextTile.GetComponent<Room>().eastExit.name;
            spawnPoints.Add(sp);
        }
        sp = nextTile.GetComponent<Room>().westExit;
        if (sp != null && cameFrom != 2)
        {
            sp.name = nextTile.GetComponent<Room>().westExit.name;
            spawnPoints.Add(sp);
        }
    }

    void addNextRoom(GameObject sp)
    {
        GameObject nextTile;
        int dir = -1;
        Vector3 position;

        nextTile = forestTiles[UnityEngine.Random.Range(0, forestTiles.Length)];
        if (sp.GetComponent<exit>().biome.Equals("desert"))
            nextTile = desertTiles[UnityEngine.Random.Range(0, desertTiles.Length)];
        if (sp.GetComponent<exit>().biome.Equals("base") && baseTiles != null)
            nextTile = baseTiles[UnityEngine.Random.Range(0, baseTiles.Length)];


        if (sp.name.Equals("N exit"))
        {
            if (nextTile.GetComponent<Room>().southExit == null || sp.GetComponent<exit>().biome != nextTile.GetComponent<Room>().southExit.GetComponent<exit>().biome)
            {
                addNextRoom(sp);
                return;
            }
            position = sp.transform.position + nextTile.GetComponent<Room>().southOffset;
            dir = 0;
        }
        else if (sp.name.Equals("S exit"))
        {
            if (nextTile.GetComponent<Room>().northExit == null || sp.GetComponent<exit>().biome != nextTile.GetComponent<Room>().northExit.GetComponent<exit>().biome)
            {
                addNextRoom(sp);
                return;
            }
            position = sp.transform.position + nextTile.GetComponent<Room>().northOffset;
            dir = 1;
        }
        else if (sp.name.Equals("E exit"))
        {
            if (nextTile.GetComponent<Room>().westExit == null || sp.GetComponent<exit>().biome != nextTile.GetComponent<Room>().westExit.GetComponent<exit>().biome)
            {
                addNextRoom(sp);
                return;
            }
            position = sp.transform.position + nextTile.GetComponent<Room>().westOffset;
            dir = 2;
        }
        else
        {
            if (nextTile.GetComponent<Room>().eastExit == null || sp.GetComponent<exit>().biome != nextTile.GetComponent<Room>().eastExit.GetComponent<exit>().biome)
            {
                addNextRoom(sp);
                return;
            }
            position = sp.transform.position + nextTile.GetComponent<Room>().eastOffset;
            dir = 3;
        }

        nextTile = Instantiate(nextTile, position, nextTile.transform.rotation);

        if (checkForOverlapingRoom(nextTile))
        {
            Destroy(nextTile);
            if (--maxAttempts == 0)
            {
                maxAttempts = 4;
                endCap(sp);
                return;
            }
            else
            {
                addNextRoom(sp);
                return;
            }
        }


        nextTile.transform.SetParent(this.transform);

        collectSpawns(nextTile);
        addExits(nextTile, dir);
    }

    private bool checkForOverlapingRoom(GameObject nextTile)
    {
        Dictionary<Vector2, GameObject> tempList = new Dictionary<Vector2, GameObject>();
        foreach (KeyValuePair<Vector2, GameObject> tile in nextTile.GetComponent<Room>().tiles)
        {
            if (allTiles.ContainsKey(tile.Key))
            {
                tempList = null;
                return true;
            }
            else
            {
                tempList.Add(tile.Key, tile.Value);
            }
        }

        foreach (KeyValuePair<Vector2, GameObject> tile in tempList)
        {
            allTiles.Add(tile.Key, tile.Value);
        }

        if (nextTile.GetComponent<Room>().minX < minX)
            minX = nextTile.GetComponent<Room>().minX;
        if (nextTile.GetComponent<Room>().minY < minY)
            minY = nextTile.GetComponent<Room>().minY;
        if (nextTile.GetComponent<Room>().maxX > maxX)
            maxX = nextTile.GetComponent<Room>().maxX;
        if (nextTile.GetComponent<Room>().maxY > maxY)
            maxY = nextTile.GetComponent<Room>().maxY;


        return false;
    }

    private void endCap(GameObject sp)
    {
        GameObject nextTile;
        Vector3 position;

        if (sp.name.Equals("N exit"))
        {
            nextTile = forestEndCaps[0];
            if (sp.GetComponent<exit>().biome.Equals("desert"))
                nextTile = desertEndCaps[0];
            if (sp.GetComponent<exit>().biome.Equals("base"))
                nextTile = baseEndCaps[0];

            position = sp.transform.position + nextTile.GetComponent<Room>().southOffset;

            nextTile = Instantiate(nextTile, position, nextTile.transform.rotation);
            nextTile.transform.SetParent(this.transform);

            if (!checkForOverlapingRoom(nextTile))
            {
                collectSpawns(nextTile);
                return;
            }

            Destroy(nextTile);

            nextTile = forestEndCaps[1];
            if (sp.GetComponent<exit>().biome.Equals("desert"))
                nextTile = desertEndCaps[1];
            if (sp.GetComponent<exit>().biome.Equals("base"))
                nextTile = baseEndCaps[1];

            position = sp.transform.position + nextTile.GetComponent<Room>().southOffset;

            nextTile = Instantiate(nextTile, position, nextTile.transform.rotation);
            nextTile.transform.SetParent(this.transform);

            if (!checkForOverlapingRoom(nextTile))
            {
                collectSpawns(nextTile);
                return;
            }

            Destroy(nextTile);

            nextTile = forestEndCaps[2];
            if (sp.GetComponent<exit>().biome.Equals("desert"))
                nextTile = desertEndCaps[2];
            if (sp.GetComponent<exit>().biome.Equals("base"))
                nextTile = baseEndCaps[2];

            position = sp.transform.position + nextTile.GetComponent<Room>().southOffset;

            nextTile = Instantiate(nextTile, position, nextTile.transform.rotation);
            nextTile.transform.SetParent(this.transform);

            if (!checkForOverlapingRoom(nextTile))
            {
                collectSpawns(nextTile);
                return;
            }

            Destroy(nextTile);
        }
        else if (sp.name.Equals("S exit"))
        {
            nextTile = forestEndCaps[3];
            if (sp.GetComponent<exit>().biome.Equals("desert"))
                nextTile = desertEndCaps[3];
            if (sp.GetComponent<exit>().biome.Equals("base"))
                nextTile = baseEndCaps[3];

            position = sp.transform.position + nextTile.GetComponent<Room>().northOffset;

            nextTile = Instantiate(nextTile, position, nextTile.transform.rotation);
            nextTile.transform.SetParent(this.transform);

            if (checkForOverlapingRoom(nextTile) == false)
            {
                collectSpawns(nextTile);
                return;
            }

            Destroy(nextTile);

            nextTile = forestEndCaps[4];
            if (sp.GetComponent<exit>().biome.Equals("desert"))
                nextTile = desertEndCaps[4];
            if (sp.GetComponent<exit>().biome.Equals("base"))
                nextTile = baseEndCaps[4];

            position = sp.transform.position + nextTile.GetComponent<Room>().northOffset;

            nextTile = Instantiate(nextTile, position, nextTile.transform.rotation);
            nextTile.transform.SetParent(this.transform);

            if (!checkForOverlapingRoom(nextTile))
            {
                collectSpawns(nextTile);
                return;
            }

            Destroy(nextTile);

            nextTile = forestEndCaps[5];
            if (sp.GetComponent<exit>().biome.Equals("desert"))
                nextTile = desertEndCaps[5];
            if (sp.GetComponent<exit>().biome.Equals("base"))
                nextTile = baseEndCaps[5];

            position = sp.transform.position + nextTile.GetComponent<Room>().northOffset;

            nextTile = Instantiate(nextTile, position, nextTile.transform.rotation);
            nextTile.transform.SetParent(this.transform);

            if (!checkForOverlapingRoom(nextTile))
            {
                collectSpawns(nextTile);
                return;
            }

            Destroy(nextTile);
        }
        else if (sp.name.Equals("E exit"))
        {
            nextTile = forestEndCaps[6];
            if (sp.GetComponent<exit>().biome.Equals("desert"))
                nextTile = desertEndCaps[6];
            if (sp.GetComponent<exit>().biome.Equals("base"))
                nextTile = baseEndCaps[6];

            position = sp.transform.position + nextTile.GetComponent<Room>().westOffset;

            nextTile = Instantiate(nextTile, position, nextTile.transform.rotation);
            nextTile.transform.SetParent(this.transform);

            if (!checkForOverlapingRoom(nextTile))
            {
                collectSpawns(nextTile);
                return;
            }

            Destroy(nextTile);

            nextTile = forestEndCaps[7];
            if (sp.GetComponent<exit>().biome.Equals("desert"))
                nextTile = desertEndCaps[7];
            if (sp.GetComponent<exit>().biome.Equals("base"))
                nextTile = baseEndCaps[7];

            position = sp.transform.position + nextTile.GetComponent<Room>().westOffset;

            nextTile = Instantiate(nextTile, position, nextTile.transform.rotation);
            nextTile.transform.SetParent(this.transform);

            if (!checkForOverlapingRoom(nextTile))
            {
                collectSpawns(nextTile);
                return;
            }

            Destroy(nextTile);

            nextTile = forestEndCaps[8];
            if (sp.GetComponent<exit>().biome.Equals("desert"))
                nextTile = desertEndCaps[8];
            if (sp.GetComponent<exit>().biome.Equals("base"))
                nextTile = baseEndCaps[8];

            position = sp.transform.position + nextTile.GetComponent<Room>().westOffset;

            nextTile = Instantiate(nextTile, position, nextTile.transform.rotation);
            nextTile.transform.SetParent(this.transform);

            if (!checkForOverlapingRoom(nextTile))
            {
                collectSpawns(nextTile);
                return;
            }

            Destroy(nextTile);

        }
        else
        {
            nextTile = forestEndCaps[9];
            if (sp.GetComponent<exit>().biome.Equals("desert"))
                nextTile = desertEndCaps[9];
            if (sp.GetComponent<exit>().biome.Equals("base"))
                nextTile = baseEndCaps[9];

            position = sp.transform.position + nextTile.GetComponent<Room>().eastOffset;

            nextTile = Instantiate(nextTile, position, nextTile.transform.rotation);
            nextTile.transform.SetParent(this.transform);

            if (!checkForOverlapingRoom(nextTile))
            {
                collectSpawns(nextTile);
                return;
            }

            Destroy(nextTile);

            nextTile = forestEndCaps[10];
            if (sp.GetComponent<exit>().biome.Equals("desert"))
                nextTile = desertEndCaps[10];
            if (sp.GetComponent<exit>().biome.Equals("base"))
                nextTile = baseEndCaps[10];

            position = sp.transform.position + nextTile.GetComponent<Room>().eastOffset;

            nextTile = Instantiate(nextTile, position, nextTile.transform.rotation);
            nextTile.transform.SetParent(this.transform);

            if (!checkForOverlapingRoom(nextTile))
            {
                collectSpawns(nextTile);
                return;
            }

            Destroy(nextTile);
            nextTile = forestEndCaps[11];
            if (sp.GetComponent<exit>().biome.Equals("desert"))
                nextTile = desertEndCaps[11];
            if (sp.GetComponent<exit>().biome.Equals("base"))
                nextTile = baseEndCaps[11];

            position = sp.transform.position + nextTile.GetComponent<Room>().eastOffset;

            nextTile = Instantiate(nextTile, position, nextTile.transform.rotation);
            nextTile.transform.SetParent(this.transform);

            if (!checkForOverlapingRoom(nextTile))
            {
                collectSpawns(nextTile);
                return;
            }

            Destroy(nextTile);

        }

    }


    public int mapSizeX, mapSizeY;
    public int[,] tiles;

    public void generateMapData()
    {
        mapSizeX = (int)(maxX - minX) + 1;
        mapSizeY = (int)(maxY - minY) + 1;

        GSC.data.mapSizeX = mapSizeX;
        GSC.data.mapSizeY = mapSizeY;
        GSC.data.minX = minX;
        GSC.data.minY = minY;
        GSC.data.maxX = maxX;
        GSC.data.maxY = maxY;

        // Allocate our map tiles, init to oob
        tiles = new int[mapSizeX, mapSizeY];
        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
                tiles[i, j] = 18;
        }

        GSC.data.clickables = new Dictionary<Vector2, ClickableTile>();

        int x, y;

        for (x = 0; x < mapSizeX; x++)
        {
            for (y = 0; y < mapSizeY; y++)
            {
                GameObject retVal;
                int adjustedX = x + (int)minX;
                int adjustedY = y + (int)minY;
                if (allTiles.TryGetValue(new Vector2(adjustedX, adjustedY), out retVal))
                {
                    //Debug.Log("X, Y (" + adjustedX + ", " + adjustedY + ") tiles[" + x + ", " + y + "]");
                    tiles[x, y] = retVal.GetComponent<Tile>().type;


                    //GameObject go = (GameObject)Instantiate(GSC.data.clickablePrefab, new Vector3(adjustedX, adjustedY, .3f), Quaternion.identity);
                    ClickableTile ct = retVal.GetComponent<ClickableTile>();
                    ct.tileX = adjustedX;
                    ct.tileY = adjustedY;
                    ct.type = retVal.GetComponent<Tile>().type;
                    //ct.transform.SetParent(this.transform, true);
                    ct.GetComponent<BoxCollider2D>().enabled = false;
                    GSC.data.clickables.Add(new Vector2(adjustedX, adjustedY), ct);
                }
            }
        }

        GSC.data.tiles = tiles;
        GSC.data.tilesLen = tiles.Length;
        GSC.data.cliackablesLen = GSC.data.clickables.Count;
    }

    void collectSpawns(GameObject tile)
    {
        foreach (KeyValuePair<Vector2, GameObject> space in tile.GetComponent<Room>().tiles)
        {
            if (space.Value.GetComponent<Tile>().isPlayerSpawn)
            {
                playerSpawns.Add(space.Key);
            }

            if (space.Value.GetComponent<Tile>().isEnemySpawn)
            {
                enemySpawns.Add(space.Key);
            }

            if (space.Value.GetComponent<Tile>().isObjectiveSpawn)
            {
                objectiveSpawns.Add(space.Key);
            }
        }
    }

}
