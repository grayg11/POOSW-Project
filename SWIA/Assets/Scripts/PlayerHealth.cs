using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healthBarUI;
    public Slider slider;
    Unit player;

    void Start()
    {
        player = GetComponent<Unit>();
        health = maxHealth = player.maxHealth;
        slider.value = CalculateHealth();
    }

    void Update()
    {
        slider.value = CalculateHealth();
        if(health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if(health<=0)
        {
            health = 0;
            //Destroy(gameObject);
        }

    }

    float CalculateHealth()
    {
        health = player.health;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        return health / maxHealth;
    }
}
