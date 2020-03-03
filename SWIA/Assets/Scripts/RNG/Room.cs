using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        tiles = new Dictionary<Vector2, GameObject>();
        Tile[] tArray = this.GetComponentsInChildren<Tile>();
        for (int i = 0; i < tArray.Length; i++)
        {
            if (i == 0)
            {
                minX = maxX = tArray[i].transform.position.x;
                minY = maxY = tArray[i].transform.position.y;
            }
            
            tiles.Add(new Vector2(tArray[i].transform.position.x, tArray[i].transform.position.y), tArray[i].gameObject);
            if (tArray[i].transform.position.x < minX)
                minX = tArray[i].transform.position.x;
            if (tArray[i].transform.position.x > maxX)
                maxX = tArray[i].transform.position.x;
            if (tArray[i].transform.position.y < minY)
                minY = tArray[i].transform.position.y;
            if (tArray[i].transform.position.y > maxY)
                maxY = tArray[i].transform.position.y;
        }
        tArray = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int room;
    public bool endcap;
    public string biome;
    public int xDist;
    public int yDist;
    public GameObject northExit;
    public GameObject southExit;
    public GameObject eastExit;
    public GameObject westExit;
    public Vector3 northOffset;
    public Vector3 southOffset;
    public Vector3 eastOffset;
    public Vector3 westOffset;
    public bool playerSpawns;
    public bool enemySpawns;
    public bool objectiveSpawns;
    public Dictionary<Vector2, GameObject> tiles;
    public float minX, maxX;
    public float minY, maxY;

}
