using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public MapGen generator;
    public GameStateController GSC;
    public TileType[] tileTypes;
    Node[,] graph;
    public List<Node> currentPath = null;
    public bool moving = false;
    public bool attacking = false;


    private void Start()
    {
        //GSC = FindObjectOfType<GameStateController>();
    }

    public void GeneratePathfindingGraph()
    {
        // Initialze the array

        graph = new Node[GSC.data.mapSizeX, GSC.data.mapSizeY];

        // Create a node for each spot
        for (int x = 0; x < GSC.data.mapSizeX; x++)
        {
            for (int y = 0; y < GSC.data.mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        // Create each nodes neighbors
        for (int x = 0; x < GSC.data.mapSizeX; x++)
        {
            for (int y = 0; y < GSC.data.mapSizeY; y++)
            {
                TileType tt = tileTypes[GSC.data.tiles[x, y]];
                //Debug.Log("neighbor len" + tt.neighbors.Length);

                // Left neighbors including diags
                if (x > 0)
                {
                    if (tt.neighbors[3])
                        graph[x, y].neighbors.Add(graph[x - 1, y]);
                    if (y > 0 && tt.neighbors[5] && isMoveValid(x, y, 2))
                        graph[x, y].neighbors.Add(graph[x - 1, y - 1]);
                    if (y < GSC.data.mapSizeY - 1 && tt.neighbors[0] && isMoveValid(x, y, 0))
                        graph[x, y].neighbors.Add(graph[x - 1, y + 1]);
                }
                // Right neighbors including diags
                if (x < GSC.data.mapSizeX - 1)
                {
                    if (tt.neighbors[4])
                        graph[x, y].neighbors.Add(graph[x + 1, y]);
                    if (y > 0 && tt.neighbors[7] && isMoveValid(x, y, 3))
                        graph[x, y].neighbors.Add(graph[x + 1, y - 1]);
                    if (y < GSC.data.mapSizeY - 1 && tt.neighbors[2] && isMoveValid(x, y, 1))
                        graph[x, y].neighbors.Add(graph[x + 1, y + 1]);
                }
                // Top and bottom neighbors
                if (y > 0 && tt.neighbors[6])
                    graph[x, y].neighbors.Add(graph[x, y - 1]);
                if (y < GSC.data.mapSizeY - 1 && tt.neighbors[1])
                    graph[x, y].neighbors.Add(graph[x, y + 1]);
            }
        }
    }

    bool isMoveValid(int x, int y, int dir) // dir(0 = q, 1= p, 2 = z, 3 = m)
    {
        int orig, check1, check2, check3;
        orig = generator.tiles[x, y];

        if (dir == 0)
        {
            // move NW
            check1 = generator.tiles[x - 1, y + 1];
            if (check1 == 12 || check1 == 13)
                return false;

            if (check1 != 4 && check1 != 5 && check1 != 8 && check1 != 9 && orig != 0 && orig != 1 && orig != 8 && orig!= 0)
            {
                check2 = generator.tiles[x, y + 1];
                if (check2 != 6 && check2 != 7 && check2 != 10 && check2 != 11)
                    return true;
            }

            if (check1 != 2 && check1 != 3 && check1 != 14 && check1 != 15 && orig != 6 && orig != 7 && orig != 14 && orig != 15)
            {
                check3 = generator.tiles[x - 1, y];
                if (check3 != 0 && check3 != 1 && check3 != 10 && check3 != 11)
                    return true;
            }

        }

        if (dir == 1)
        {
            // move NE
            check1 = generator.tiles[x + 1, y + 1];
            if (check1 == 14 || check1 == 15)
                return false;

            if (check1 != 6 && check1 != 7 && check1 != 10 && check1 != 11 && orig != 0 && orig != 1 && orig != 10 && orig != 11)
            {
                check2 = generator.tiles[x, y + 1];
                if (check1 != 4 && check1 != 5 && check1 != 8 && check1 != 9)
                    return true;
            }

            if (check1 != 2 && check1 != 3 && check1 != 12 && check1 != 13 && orig != 4 && orig != 5 && orig != 12 && orig != 13)
            {
                check3 = generator.tiles[x + 1, y];
                if (check3 != 0 && check3 != 1 && check3 != 8 && check3 != 9)
                    return true;
            }
        }

        if (dir == 2)
        {
            // move SW
            check1 = generator.tiles[x - 1, y - 1];
            if (check1 == 8 || check1 == 9)
                return false;

            if (check1 != 4 && check1 != 4 && check1 != 12 && check1 != 13 && orig != 2 && orig != 3 && orig != 12 && orig != 13)
            {
                check2 = generator.tiles[x, y - 1];
                if (check2 != 6 && check2 != 7 && check2 != 14 && check2 != 15)
                    return true;
            }

            if (check1 != 0 && check1 != 1 && check1 != 10 && check1 != 11 && orig != 6 && orig != 7 && orig != 10 && orig != 11)
            {
                check3 = generator.tiles[x - 1, y];
                if (check3 != 2 && check3 != 3 && check3 != 14 && check3 != 15)
                    return true;
            }
        }

        if (dir == 3)
        {
            // move SE
            check1 = generator.tiles[x + 1, y - 1];
            if (check1 == 10 || check1 == 11)
                return false;

            if (check1 != 6 && check1 != 7 && check1 != 14 && check1 != 15 && orig != 2 && orig != 3 && orig != 14 && orig != 15)
            {
                check2 = generator.tiles[x, y - 1];
                if (check2 != 4 && check2 != 5 && check2 != 12 && check2 != 13)
                    return true;
            }

            if (check1 != 0 && check1 != 0 && check1 != 8 && check1 != 9 && orig != 4 && orig != 5 && orig != 8 && orig != 9)
            {
                check3 = generator.tiles[x + 1, y];
                if (check3 != 2 && check3 != 3 && check3 != 12 && check3 != 13)
                    return true;
            }
        }
        return false;
    }

    public void GeneratePathTo(int x, int y, bool isMove, bool isAttack)
    {
        // Clear out old path
        //selectedUnit.GetComponent<Unit>().currentPath = null;

        // If Invalid terrain is selected return now.
        if (UnitCanEnterTile(x, y) == false)
        {
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();
        Node source = graph[
                            GSC.selectedUnit.GetComponent<Unit>().tileX - (int)GSC.data.minX,
                            GSC.selectedUnit.GetComponent<Unit>().tileY - (int)GSC.data.minY
                            ];

        Node target = graph[
                            x - (int)GSC.data.minX,
                            y - (int)GSC.data.minY
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
        if (prev[target] == null)
            return;

        if (isMove && dist[target] <= GSC.selectedUnit.GetComponent<Unit>().movement + .5)
        {
            foreach (GameObject heroGO in GSC.heroes)
            {
                if (heroGO.transform.position.x == x && heroGO.transform.position.y == y)
                {
                    return;
                }
            }

            foreach (GameObject enemyGO in GSC.enemies)
            {
                if (enemyGO.transform.position.x == x && enemyGO.transform.position.y == y)
                {
                    return;
                }
            }

            //Debug.Log(GSC.data.clickables.ContainsKey(new Vector2(x, y)));
            //GameObject retVal;
            //if (GSC.data.allTiles.TryGetValue(new Vector2(x, y), out retVal))
            if (GSC.data.clickables.ContainsKey(new Vector2(x, y)))
            {
                GameObject go = (GameObject)Instantiate(GSC.data.moveTileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
                go.transform.SetParent(this.transform, true);
                //go.GetComponent<ClickableTile>().tileX = x;
                //go.GetComponent<ClickableTile>().tileY = y;
                //go.GetComponent<ClickableTile>().type = retVal.GetComponent<Tile>().type;
                GSC.data.moveSpaces.Add(new Vector2(x, y), go);
                GSC.data.clickables[new Vector2(x, y)].GetComponent<BoxCollider2D>().enabled = true;
            }

            return;
        }

        else if (isAttack && dist[target] <= (1.01 * GSC.selectedUnit.GetComponent<Unit>().maxRange))
        {
            foreach (GameObject heroGO in GSC.heroes)
            {
                if (heroGO.transform.position.x == x && heroGO.transform.position.y == y)
                {
                    return;
                }
            }

            if (GSC.data.clickables.ContainsKey(new Vector2(x, y)))
            {
                // if < min range use 100% hit tile
                // else if < mid range use 50% tile
                // else use max range tile
                GameObject go = (GameObject)Instantiate(GSC.data.attackTileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
                go.transform.SetParent(this.transform, true);
                GSC.data.attackSpaces.Add(new Vector2(x, y), go);
                // we dont actually want the tiles clickable
                //GSC.data.clickables[new Vector2(x, y)].GetComponent<BoxCollider>().enabled = true;
            }
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

        GSC.selectedUnit.GetComponent<Unit>().currentPath = currentPath;
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
                            GSC.selectedUnit.GetComponent<Unit>().tileX,
                            GSC.selectedUnit.GetComponent<Unit>().tileY
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

        if (isMove && dist[target] <= GSC.selectedUnit.GetComponent<Unit>().movement + .5)
        {
            foreach (GameObject heroGO in GSC.heroes)
            {
                if (heroGO.transform.position.x == x && heroGO.transform.position.y == y)
                {
                    return;
                }
            }
            GameObject go = (GameObject)Instantiate(GSC.data.moveTileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
            go.transform.SetParent(this.transform, true);
            GSC.data.moveSpaces.Add(new Vector2(x, y), go);
            GSC.data.clickables[new Vector2(x, y)].GetComponent<BoxCollider>().enabled = true;
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

        GSC.selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }

    // CostToEnterTile and GeneratPathTo
    public bool UnitCanEnterTile(int x, int y)
    {
        // Test units movement method against terrain flags to check if they can

        GameObject go;
        bool walkable;
        if (GSC.data.allTiles.TryGetValue(new Vector2(x, y), out go))
            walkable = go.GetComponent<Tile>().isWalkable;
        else
            walkable = false;
            
        return walkable;
    }

    // Called from GeneratePathTo and Unit pathing
    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {

        TileType tt = tileTypes[GSC.data.tiles[targetX, targetY]];

        if (UnitCanEnterTile(targetX + (int)GSC.data.minX, targetY + (int)GSC.data.minY) == false)
            return Mathf.Infinity;

        float cost = tt.movementCost;

        // Makes diagonal movement slightly higer cost to encourage straighter movement
        if (sourceX != targetX && sourceY != targetY)
        {
            cost += 0.001f;

            // eliminate diag movement through impassable terrain
            if (!GSC.selectedUnit.GetComponent<Unit>().isMobile)
            {
                if (targetX > sourceX)
                {
                    if (targetY > sourceY)
                    {
                        if ((GSC.data.tiles[sourceX, sourceY + 1] == 19 || GSC.data.tiles[sourceX, sourceY + 1] == 18)
                            && (GSC.data.tiles[sourceX + 1, sourceY] == 19 || GSC.data.tiles[sourceX + 1, sourceY] == 18))
                        {
                            cost += 100;
                        }
                    }
                    else
                    {
                        if ((GSC.data.tiles[sourceX, sourceY - 1] == 19 || GSC.data.tiles[sourceX, sourceY - 1] == 18)
                            && (GSC.data.tiles[sourceX + 1, sourceY] == 19 || GSC.data.tiles[sourceX + 1, sourceY] == 18))
                        {
                            cost += 100;
                        }
                    }
                }
                else
                {
                    if (targetY > sourceY)
                    {
                        if ((GSC.data.tiles[sourceX, sourceY + 1] == 19 || GSC.data.tiles[sourceX, sourceY + 1] == 18)
                            && (GSC.data.tiles[sourceX - 1, sourceY] == 19 || GSC.data.tiles[sourceX - 1, sourceY] == 18))
                        {
                            cost += 100;
                        }
                    }
                    else
                    {
                        if ((GSC.data.tiles[sourceX, sourceY - 1] == 19 || GSC.data.tiles[sourceX, sourceY - 1] == 18)
                            && (GSC.data.tiles[sourceX - 1, sourceY] == 19 || GSC.data.tiles[sourceX - 1, sourceY] == 18))
                        {
                            cost += 100;
                        }
                    }
                }
            }
        }

        return cost;
    }

    public void GenerateUnitSpaces(int x, int y, int distance, bool isMove, bool isAttack)
    {
        // From our location to any valid space within movement
        GSC.data.moveSpaces = new Dictionary<Vector2, GameObject>();
        GSC.data.attackSpaces = new Dictionary<Vector2, GameObject>();

        for (int i = x - distance; i <= x + distance; i++)
        {
            for (int j = y - distance; j <= y + distance; j++)
            {
                if (i >= (int)GSC.data.minX && i < (int)GSC.data.maxX && j >= (int)GSC.data.minY && j < (int)GSC.data.maxY)
                    GeneratePathTo(i, j, isMove, isAttack);
            }
        }
    }
}
