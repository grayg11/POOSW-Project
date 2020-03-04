using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Experimental.U2D;
using System.IO;

public class TileMap : MonoBehaviour
{
    public UnitType[] unitTypes;
    private GameObject selectedUnit;
    public TileType[] tileTypes;
    public DieTypes[] dice; // red, yellow, green, blue, black, white
    //public List<Vector3> spawns;
    public List<GameObject> heroes;
    public List<GameObject> enemies;

    public Vector3[] heroSpawns = { new Vector3(8, 9), new Vector3(8, 11), new Vector3(11, 8), new Vector3(11, 11) };
    public Vector3[] enemySpawns = { new Vector3(5, 4) };  // hard code??? probably

    char[,] tiles;
    Node[,] graph;
    public List<Node> currentPath = null;
    public Dictionary<Vector2, ClickableTile> clickables;
    public Dictionary<Vector2, GameObject> moveSpaces;
    public Dictionary<Vector2, GameObject> attackSpaces;
    public bool moving;
    public bool attacking;

    public int enemyTurnCount = 0;

    int mapSizeX;
    int mapSizeY;

    public GameObject SelectedUnit { get => selectedUnit; set => selectedUnit = value; }
    public GameObject moveTileVisualPrefab;
    public Material moveTileSelected;
    public Material moveTileUnselected;
    public Material attackTile;
    public GameObject attackTileVisualPrefab;
    public GameObject clickedTile;

    public GameObject greenCrate;

    public void GenerateUnits()
    {
        // spawn heros

        //spawns = SpawnLocations.heroSpawns;
        heroes = new List<GameObject>();
        for (int i = 0; i < CharacterSelection.party.Count; i++)
        {
            UnitType ut = unitTypes[CharacterSelection.party[i]];
            Vector3 position = heroSpawns[i] + ut.tileVisualPrefab.transform.position;
            GameObject go = Instantiate(ut.tileVisualPrefab, position, ut.tileVisualPrefab.transform.rotation);
            go.GetComponent<Unit>().map = this;
            go.GetComponent<Unit>().unit = CharacterSelection.party[i];
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

        // spawn enemies
        //enemyspawns = SpawnLocations.enemySpawns;
        enemies = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            UnitType ut = unitTypes[6];
            Vector3 position = enemySpawns[i] + ut.tileVisualPrefab.transform.position;
            GameObject go = Instantiate(ut.tileVisualPrefab, position, ut.tileVisualPrefab.transform.rotation);
            go.GetComponent<Unit>().map = this;
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

        //object generation
        GameObject crateGo = Instantiate(greenCrate, new Vector3(10,10) + greenCrate.transform.position, greenCrate.transform.rotation);
        GameObject crateGo2 = Instantiate(greenCrate, new Vector3(12, 10) + greenCrate.transform.position, greenCrate.transform.rotation);

    }

    public void GenerateMapData()
    {
        StreamReader sr = new StreamReader("Assets/Misc/Endor.txt");

        mapSizeX = int.Parse(sr.ReadLine());
        mapSizeY = int.Parse(sr.ReadLine());

        // Allocate our map tiles
        tiles = new char[mapSizeX, mapSizeY];

        int x, y;

        for (y = 0; y < mapSizeY; y++)
        {
            String rowtiles = sr.ReadLine();
            for (x = 0; x < mapSizeX; x++)
            {
                tiles[x, y] = rowtiles[x];
            }
        }

        sr.Close();
    }

    bool isMoveValid(int x, int y, int dir) // dir(0 = q, 1= p, 2 = z, 3 = m)
    {
        char orig, check1, check2, check3;
        orig = tiles[x, y];

        if (dir == 0)
        {
            // move NW
            check1 = tiles[x - 1, y + 1];
            if (check1.Equals('m') || check1.Equals('M'))
                return false;

            if (!check1.Equals('e') && !check1.Equals('E') && !check1.Equals('p') && !check1.Equals('P') && !orig.Equals('n') && !orig.Equals('N') && !orig.Equals('p') && !orig.Equals('P'))
            {
                check2 = tiles[x, y + 1];
                if (!check2.Equals('w') && !check2.Equals('W') && !check2.Equals('q') && !check2.Equals('Q'))
                    return true;
            }

            if (!check1.Equals('s') && !check1.Equals('S') && !check1.Equals('z') && !check1.Equals('Z') && !orig.Equals('w') && !orig.Equals('W') && !orig.Equals('z') && !orig.Equals('Z'))
            {
                check3 = tiles[x - 1, y];
                if (!check3.Equals('n') && !check3.Equals('N') && !check3.Equals('q') && !check3.Equals('Q'))
                    return true;
            }

        }

        if (dir == 1)
        {
            // move NE
            check1 = tiles[x + 1, y + 1];
            if (check1.Equals('z') || check1.Equals('Z'))
                return false;

            if (!check1.Equals('w') && !check1.Equals('W') && !check1.Equals('q') && !check1.Equals('Q') && !orig.Equals('n') && !orig.Equals('N') && !orig.Equals('q') && !orig.Equals('Q'))
            {
                check2 = tiles[x, y + 1];
                if (!check1.Equals('e') && !check1.Equals('E') && !check1.Equals('p') && !check1.Equals('P'))
                    return true;
            }

            if (!check1.Equals('s') && !check1.Equals('S') && !check1.Equals('m') && !check1.Equals('M') && !orig.Equals('e') && !orig.Equals('E') && !orig.Equals('m') && !orig.Equals('M'))
            {
                check3 = tiles[x + 1, y];
                if (!check3.Equals('n') && !check3.Equals('N') && !check3.Equals('p') && !check3.Equals('P'))
                    return true;
            }
        }

        if (dir == 2)
        {
            // move SW
            check1 = tiles[x - 1, y - 1];
            if (check1.Equals('p') || check1.Equals('P'))
                return false;

            if (!check1.Equals('e') && !check1.Equals('E') && !check1.Equals('m') && !check1.Equals('M') && !orig.Equals('s') && !orig.Equals('S') && !orig.Equals('m') && !orig.Equals('M'))
            {
                check2 = tiles[x, y - 1];
                if (!check2.Equals('w') && !check2.Equals('W') && !check2.Equals('z') && !check2.Equals('Z'))
                    return true;
            }

            if (!check1.Equals('n') && !check1.Equals('N') && !check1.Equals('q') && !check1.Equals('Q') && !orig.Equals('w') && !orig.Equals('W') && !orig.Equals('q') && !orig.Equals('Q'))
            {
                check3 = tiles[x - 1, y];
                if (!check3.Equals('s') && !check3.Equals('S') && !check3.Equals('z') && !check3.Equals('Z'))
                    return true;
            }
        }

        if (dir == 3)
        {
            // move SE
            check1 = tiles[x + 1, y - 1];
            if (check1.Equals('q') || check1.Equals('Q'))
                return false;

            if (!check1.Equals('w') && !check1.Equals('W') && !check1.Equals('z') && !check1.Equals('Z') && !orig.Equals('s') && !orig.Equals('S') && !orig.Equals('z') && !orig.Equals('Z'))
            {
                check2 = tiles[x, y - 1];
                if (!check2.Equals('e') && !check2.Equals('E') && !check2.Equals('m') && !check2.Equals('M'))
                    return true;
            }

            if (!check1.Equals('n') && !check1.Equals('N') && !check1.Equals('p') && !check1.Equals('P') && !orig.Equals('e') && !orig.Equals('E') && !orig.Equals('p') && !orig.Equals('P'))
            {
                check3 = tiles[x + 1, y];
                if (!check3.Equals('s') && !check3.Equals('S') && !check3.Equals('m') && !check3.Equals('M'))
                    return true;
            }
        }
        return false;
    }

    public void GeneratePathfindingGraph()
    {
        // Initialze the array
        graph = new Node[mapSizeX, mapSizeY];

        // Create a node for each spot
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        // Create each nodes neighbors
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {

                TileType tt = tileTypes[tileToType(tiles[x, y])];
                //Debug.Log("neighbor len" + tt.neighbors.Length);

                // Left neighbors including diags
                if (x > 0)
                {
                    if (tt.neighbors[3])
                        graph[x, y].neighbors.Add(graph[x - 1, y]);
                    if (y > 0 && tt.neighbors[5] && isMoveValid(x, y, 2))
                        graph[x, y].neighbors.Add(graph[x - 1, y - 1]);
                    if (y < mapSizeY - 1 && tt.neighbors[0] && isMoveValid(x, y, 0))
                        graph[x, y].neighbors.Add(graph[x - 1, y + 1]);
                }
                // Right neighbors including diags
                if (x < mapSizeX - 1)
                {
                    if (tt.neighbors[4])
                        graph[x, y].neighbors.Add(graph[x + 1, y]);
                    if (y > 0 && tt.neighbors[7] && isMoveValid(x, y, 3))
                        graph[x, y].neighbors.Add(graph[x + 1, y - 1]);
                    if (y < mapSizeY - 1 && tt.neighbors[2] && isMoveValid(x, y, 1))
                        graph[x, y].neighbors.Add(graph[x + 1, y + 1]);
                }
                // Top and bottom neighbors
                if (y > 0 && tt.neighbors[6])
                    graph[x, y].neighbors.Add(graph[x, y-1]);
                if (y < mapSizeY - 1 && tt.neighbors[1])
                    graph[x, y].neighbors.Add(graph[x, y+1]);
                // Diagonals
            }
        }
    }

    int tileToType(Char c)
    {
        if (c == 'n')
            return 0;
        if (c == 'N')
            return 1;
        if (c == 's')
            return 2;
        if (c == 'S')
            return 3;
        if (c == 'e')
            return 4;
        if (c == 'E')
            return 5;
        if (c == 'w')
            return 6;
        if (c == 'W')
            return 7;
        if (c == 'p')
            return 8;
        if (c == 'P')
            return 9;
        if (c == 'q')
            return 10;
        if (c == 'Q')
            return 11;
        if (c == 'm')
            return 12;
        if (c == 'M')
            return 13;
        if (c == 'z')
            return 14;
        if (c == 'Z')
            return 15;
        if (c == 'c')
            return 16;
        if (c == 'C')
            return 17;
        if (c == 'o')
            return 18;
        if (c == 'I')
            return 19;
        return 1;
    }

    public void GenerateMapVisual()
    {
        clickables = new Dictionary<Vector2, ClickableTile>();
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[tileToType(tiles[x, y])];
                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);

                ClickableTile ct = go.GetComponent<ClickableTile>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
                ct.transform.SetParent(this.transform, true);
                ct.GetComponent<BoxCollider>().enabled = false;
                clickables.Add(new Vector2(x, y), ct);
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }


    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {
        TileType tt = tileTypes[tileToType(tiles[targetX, targetY])];

        if (UnitCanEnterTile(targetX, targetY) == false)
            return Mathf.Infinity;

        float cost = tt.movementCost;

        // Makes diagonal movement slightly higer cost to encourage straighter movement
        if(sourceX != targetX && sourceY != targetY)
        {
            cost += 0.001f;

            // eliminate diag movement through impassable terrain
            if (!selectedUnit.GetComponent<Unit>().isMobile)
            {
                if (targetX > sourceX)
                {
                    if (targetY > sourceY)
                    {
                        if ((tiles[sourceX, sourceY + 1].Equals('I') || tiles[sourceX, sourceY + 1].Equals('o'))
                            && (tiles[sourceX + 1, sourceY].Equals('I') || tiles[sourceX + 1, sourceY].Equals('o')))
                        {
                            cost += 100;
                        }
                    }
                    else
                    {
                        if ((tiles[sourceX, sourceY - 1].Equals('I') || tiles[sourceX, sourceY - 1].Equals('o'))
                            && (tiles[sourceX + 1, sourceY].Equals('I') || tiles[sourceX + 1, sourceY].Equals('o')))
                        {
                            cost += 100;
                        }
                    }
                }
                else
                {
                    if (targetY > sourceY)
                    {
                        if ((tiles[sourceX, sourceY + 1].Equals('I') || tiles[sourceX, sourceY + 1].Equals('o'))
                            && (tiles[sourceX - 1, sourceY].Equals('I') || tiles[sourceX - 1, sourceY].Equals('o')))
                        {
                            cost += 100;
                        }
                    }
                    else
                    {
                        if ((tiles[sourceX, sourceY - 1].Equals('I') || tiles[sourceX, sourceY - 1].Equals('o'))
                            && (tiles[sourceX - 1, sourceY].Equals('I') || tiles[sourceX - 1, sourceY].Equals('o')))
                        {
                            cost += 100;
                        }
                    }
                }
            }
        }

        return cost;
    }

    public bool UnitCanEnterTile(int x, int y)
    {

        // Test units movement method against terrain flags to check if they can enter



        return tileTypes[tileToType(tiles[x,y])].isWalkable;
    }

    public void GeneratePathTo(int x, int y, bool isMove, bool isAttack)
    {
        // Clear out old path
        //selectedUnit.GetComponent<Unit>().currentPath = null;

        // If Invalid terrain is selected return now.
        if (UnitCanEnterTile(x, y) == false)
            return;

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[
                            selectedUnit.GetComponent<Unit>().tileX,
                            selectedUnit.GetComponent<Unit>().tileY
                            ];

        Node target = graph[
                            x,
                            y
                            ];

        dist[source] = 0;
        prev[source] = null;

        // Initialize all values to infinity
        foreach(Node n in graph)
        {
            if (n != source)
            {
                dist[n] = Mathf.Infinity;
                prev[n] = null;
            }

            unvisited.Add(n);
        }

        while (unvisited.Count > 0)
        {
            Node u = null;

            foreach(Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                    u = possibleU;
            }

            if (u == target)
                break;

            unvisited.Remove(u);

            foreach(Node n in u.neighbors)
            {
                //float alt = dist[u] + u.DistanceTo(n);
                float alt = dist[u] + CostToEnterTile(u.x, u.y, n.x, n.y);
                if (alt < dist[n])
                {
                    dist[n] = alt;
                    prev[n] = u;
                }
            }
        }

        // No route exists
        if (prev[target] == null)
            return;

        if (isMove && dist[target] <= selectedUnit.GetComponent<Unit>().movement + .5)
        {
            //moving = true;
            foreach (GameObject heroGO in heroes)
            {
                if (heroGO.transform.position.x == x && heroGO.transform.position.y == y)
                {
                    return;
                }
            }
            GameObject go = (GameObject)Instantiate(moveTileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
            go.transform.SetParent(this.transform, true);
            moveSpaces.Add(new Vector2(x, y), go);
            clickables[new Vector2(x, y)].GetComponent<BoxCollider>().enabled = true;
            return;
        }

        else if (isAttack && dist[target] <= 15)
        {
            //attacking = true;
            GameObject go = (GameObject)Instantiate(attackTileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
            go.transform.SetParent(this.transform, true);
            attackSpaces.Add(new Vector2(x, y), go);
            clickables[new Vector2(x, y)].GetComponent<BoxCollider>().enabled = true;
            return;
        }



        // Found route
        currentPath = new List<Node>();

        Node curr = target;

        // Create path from prev[]
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        // Reverse list
        currentPath.Reverse();

        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }

    public void GeneratePathToAlt(int x, int y, bool isMove)
    {
        // Clear out old path
        //selectedUnit.GetComponent<Unit>().currentPath = null;

        // If Invalid terrain is selected return now.
        //if (UnitCanEnterTile(x, y) == false)
            //return;

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[
                            selectedUnit.GetComponent<Unit>().tileX,
                            selectedUnit.GetComponent<Unit>().tileY
                            ];

        Node target = graph[
                            x,
                            y
                            ];

        dist[source] = 0;
        prev[source] = null;

        // Initialize all values to infinity
        foreach (Node n in graph)
        {
            if (n != source)
            {
                dist[n] = Mathf.Infinity;
                prev[n] = null;
            }

            unvisited.Add(n);
        }

        while (unvisited.Count > 0)
        {
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                    u = possibleU;
            }

            if (u == target)
                break;

            unvisited.Remove(u);

            foreach (Node n in u.neighbors)
            {
                //float alt = dist[u] + u.DistanceTo(n);
                float alt = dist[u] + CostToEnterTile(u.x, u.y, n.x, n.y);
                if (alt < dist[n])
                {
                    dist[n] = alt;
                    prev[n] = u;
                }
            }
        }

        // No route exists
        //if (prev[target] == null)
            //return;

        if (isMove && dist[target] <= selectedUnit.GetComponent<Unit>().movement + .5)
        {
            foreach (GameObject heroGO in heroes)
            {
                if (heroGO.transform.position.x == x && heroGO.transform.position.y == y)
                {
                    return;
                }
            }
            GameObject go = (GameObject)Instantiate(moveTileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
            go.transform.SetParent(this.transform, true);
            moveSpaces.Add(new Vector2(x, y), go);
            clickables[new Vector2(x, y)].GetComponent<BoxCollider>().enabled = true;
            return;
        }



        // Found route
        currentPath = new List<Node>();

        Node curr = target;

        // Create path from prev[]
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        // Reverse list
        currentPath.Reverse();

        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }

    public void GenerateUnitSpaces(int x, int y, int distance, bool isMove, bool isAttack)
    {
        // From our location to any valid space within movement
        moveSpaces = new Dictionary<Vector2, GameObject>();
        attackSpaces = new Dictionary<Vector2, GameObject>();

        for (int i = x - distance; i <= x + distance; i++)
        {
            for (int j = y - distance; j <= y + distance; j++)
            {
                if( i > 0 && i < mapSizeX - 1 && j > 0 && j < mapSizeY - 1)
                    GeneratePathTo(i, j, isMove, isAttack);
            }
        }
    }

    void determineLOS(int x, int y, int targetX, int targetY)
    {
        Vector2[] attacker = { new Vector3(x - .5f, y - .5f), 
                            new Vector3(x - .5f, y + .5f), 
                            new Vector3(x + .5f, y - .5f), 
                            new Vector3 (x +.5f, y +.5f) };

        Vector2[] target = { new Vector3(targetX - .5f, targetY - .5f), 
                            new Vector3(targetX - .5f, targetY + .5f), 
                            new Vector3(targetX + .5f, targetY - .5f), 
                            new Vector3(targetX + .5f, targetY + .5f) };

        RaycastHit[] hits;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Vector3 direction = (target[j] - attacker[i]).normalized;
                float distance = Mathf.Sqrt(Mathf.Pow(target[j].x - attacker[i].x, 2) + Mathf.Pow(target[j].y - attacker[i].y, 2));
                
                Debug.DrawRay(attacker[i], direction * distance, Color.red);
                
                hits = (Physics.RaycastAll(attacker[i], direction, distance));

                //for (int k = 0; i < hits.Length; i++)
                //{
                //    GameObject hitGO = hits[k].transform.gameObject;

                //    TileType tt = tileTypes[tileToType(tiles[(int)hitGO.transform.position.x, (int)hitGO.transform.position.y])];

                //    if (x < targetX)
                //    {
                //        if (y < targetY)
                //        {

                //        }
                //        else
                //        {

                //        }
                //    }
                //    else
                //    {

                //    }
                //}


            }
        }

    }
}
