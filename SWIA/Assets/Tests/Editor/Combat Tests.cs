using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;


public class CombatTests : MonoBehaviour
{

    Unit attacker, defender;
    GameStateController GSC;
    DataManager DM;
    CombatController test;

    [Test]
    public void testSetup()
    {
        test = gameObject.AddComponent<CombatController>();
        attacker.health = 10;
        attacker.weapon = DM.allWeapons[0];

        defender.health = 10;
        int[] def = new int[1];
        def[0] = 4;
        defender.defDice = def;
        GSC.selectedUnit = attacker.gameObject;
    }

    [Test]
    public void doesCombatStart()
    {
        int damage = -1, surge = -1, block = -1, evade = -1;

        test.Attack();
        damage = test.damage;
        surge = test.surge;
        block = test.block;
        evade = test.evade;


        Assert.AreNotEqual(-1, damage);
        Assert.AreNotEqual(-1, surge);
        Assert.AreNotEqual(-1, block);
        Assert.AreNotEqual(-1, evade);

    }

    [Test]
    public void doesCombatEnd()
    {
        test.damage = 5;
        test.block = 0;
        test.confrimAttack();

        int newHealth = defender.health - 5;

        Assert.AreEqual(newHealth, defender.health);
    }
}
