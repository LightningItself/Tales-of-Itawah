using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damagable : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    [SerializeField] private float maxHealth = 50.0f;
    [SerializeField] private float health = 50.0f;

    public float Health { get { return health; } set { health = value; } }
    public float HealthBoost { get; set; }

    public void ApplyDamage(float d)
    {
        // Update health
        health -= d;
        health = Mathf.Min(maxHealth, health);

        // Check if health is 0
        if (health + HealthBoost <= 0)
        {
            Destroy(gameObject);
        }

        // Update Health Bar
        healthBar.fillAmount = (health + HealthBoost)/ (maxHealth + HealthBoost);
    }
}
