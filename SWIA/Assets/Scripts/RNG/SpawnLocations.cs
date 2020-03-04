using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnLocations
{
    public Vector3[] heroSpawns = { new Vector3(9, 9), new Vector3(9, 11), new Vector3(11, 8), new Vector3(11, 11) };
    public Vector3[] enemySpawns = { new Vector3(11, 5) };  // hard code??? probably

}
