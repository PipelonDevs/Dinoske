using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int initialHealth=100;
    public int health;
    public bool hasHealthBar=false;
    public Slider healthBar;
    public GameObject fill;

    private void Awake()
    {
        health = initialHealth;
    }

    private void Update()
    {
        if(hasHealthBar)
        {
            if(health<=0)
            {
                fill.SetActive(false);
            }
            else
            {
                fill.SetActive(true);
            }
            healthBar.value = health;
        }
    }

    public bool TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    void Die()
    {
        if(!hasHealthBar)
        Destroy(gameObject);
        else
        {
            gameObject.SetActive(false);
        }
    }

    void Heal(int heal)
    {
        if (health + heal > initialHealth)
        {
            health = initialHealth;
        }
        else
        {
            health += heal;
        }
    }

    
}
