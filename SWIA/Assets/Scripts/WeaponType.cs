using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WeaponType
{
    public string name;
    public int[] weaponDice;
    public List<WeaponAbility> weaponAbilities;
    public int[] surgeAbilities;
    public Sprite weaponImage;
}

[System.Serializable]
public class SurgeType
{
    /// <summary>
    /// Eventually update to an array where each index = a surgetype
    /// 
    /// 0 - +1 dmg
    /// 1 - +2 dmg
    /// 2 - +3 dmg
    /// 3 - cleave 1
    /// 4 - cleave 2
    /// 5 - blast 1
    /// 6 - blast 2
    /// 7 - pierce 1
    /// 8 - pierce 2
    /// 9 - +1 acc
    /// 10 - +2 acc
    /// 11 - +3 acc
    /// 12 - bleed
    /// 13 - stun
    /// 14 - weaken
    /// 15 - +1 dmg, +2 acc
    /// 16 - +1 dmg, peirce 1
    /// 17 - +1 dmg, bleed
    /// 18 - pierce 1, bleed
    /// 19 - pierce 2, bleed
    /// 20 - +1 acc, pierce 1
    /// 21 - +2 acc, pierce 1
    /// 22 - +1 acc, weaken
    /// 
    /// // other
    /// -1 dodge
    /// blast 1 range 2 for 2 surge
    /// focus for 2 surge
    /// +2 dmg for 2 surge
    /// +1 power token
    /// +1 surge token
    /// /// </summary>
    // Damage
    public bool plus1dmg;
    public bool peirce1;
    public bool pierce2;
    public bool cleave1;

    // Status effects / conditions
    public bool stun;

    // Accuracy
    public bool plus1acc;
    public bool plus2acc;


}

[System.Serializable]
public class WeaponAbility
{
    /// <summary>
    /// reach free
    /// pierce 1 free
    /// +1 acc free
    /// +2 acc free
    /// +6 acc free
    /// +1 dmg free
    /// reroll 1 attack die free
    /// reroll all attack dice free
    /// 
    /// +2 acc for -1 dmg
    /// pierce 1 for 1 endurance
    /// 2 actions to attack 3 times
    /// weaken adjacent figure for 1 endurance
    /// interrupt attack targeting this unit to attack that enemy for 2 endurance
    /// if target defeated, gain 1 power token
    /// after attack, all adjacent enemies suffer 1 dmg for 1 endurance
    /// become focused and attack, figures dont block LOS for 2 actions
    /// </summary>
    public bool reach;
}