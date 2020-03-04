using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UnitType
{
    public string name;
    public GameObject tileVisualPrefab;
    public int health;
    public int endurance;
    public bool isMobile;
    public int movement;
    public int[] defDice;
    public int[] strength;
    public int[] insight;
    public int[] tech;
    public WeaponType weapon;
    public Sprite playerImage;
    public Sprite playerCard;
}
