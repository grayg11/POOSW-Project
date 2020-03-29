﻿using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTests
{
    [Test]
    public void HasStarted()
    {
        var test = new GameObject().AddComponent<PlayerHealth>();
        var player = GetComponent<Unit>();

        test.Start();

        Assert.AreEqual(test.maxHealth, player.maxHealth);
        Assert.AreEqual(player.maxHealth, test.health);
        Assert.AreEqual(test.slider.value, test.CalculateHealth());
    }

    [Test]
    public void CanUpdate()
    {
        var test = new GameObject().AddComponent<PlayerHealth>();

        var calculatedHealth = test.CalculateHealth();
        test.Update();

        Assert.AreEqual(test.slider.value, calculatedHealth);
        if (test.health < test.maxHealth)
        {
            Assert.AreEqual(true, test.healthBarUI.active);
        }
        if (test.health <= 0)
        {
            Assert.AreEqual(test.health, 0);
        }
    }
    
    [Test]
    public void CanCalculateHealth()
    {
        var test = new GameObject().AddComponent<PlayerHealth>();
        var player = GetComponent<Unit>();

        var health = player.health;
        if (health > maxHealth)
        {
            health = maxHealth;
        } else {
            health = health / maxHealth;
        }

        Assert.AreEqual(health, test.CalculateHealth());
    }
}
