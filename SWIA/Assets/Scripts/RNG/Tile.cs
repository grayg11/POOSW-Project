using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private void Awake()
    {
        position = this.transform.position;
    }

    public int type;
    public bool[] neighbors;
    public bool isWalkable;
    public int moveCost;
    public bool isPlayerSpawn;
    public bool isEnemySpawn;
    public bool isObjectiveSpawn;
    public Vector3 position;

}
