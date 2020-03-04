using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DieTypes
{
    public string name;
    public List<DieFace> faces;
}

[System.Serializable]
public class DieFace
{
    public Sprite face;
    public int hit;
    public int surge;
    public int accuracy;
    public int block;
    public int evade;
    public int dodge;
    // michael edit
}