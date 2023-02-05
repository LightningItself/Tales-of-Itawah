using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damagable : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    [SerializeField] private float maxHealth = 50.0f;
    [SerializeField] private float health = 50.0f;

    public void ApplyDamage(float d)
    {
        // Update health
        health -= d;

        // Check if health is 0
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        // Update Health Bar
        healthBar.fillAmount = health / maxHealth;
    }
}
