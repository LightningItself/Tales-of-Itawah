using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    // Components
    private BoxCollider2D col;
    private ITowerAttackManager attackManager;
    private Stats stats;

    // Fields
    [SerializeField] private float radius;
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;
    [SerializeField] GameObject sprite;

    private bool hovered = false;


    public float RadiusBoost { get; set; }
    public float DamageBoost { get; set; }
    public float AttackRateBoost { get; set; }

    public float Radius { get { return radius + RadiusBoost; } }
    public float Damage { get { return damage + DamageBoost; } }

    public bool _hovered;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        attackManager = GetComponent<ITowerAttackManager>();
        sprite.transform.localScale = new Vector3(2 * Radius, 2 * Radius, 1);
        stats = GameObject.Find("Stats").GetComponent<Stats>();
    }

    private void Update()
    {
        _hovered = hovered;

        MouseClick();
        

        attackManager.Attack();
    }

    private void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sprite.SetActive(hovered);

            if (hovered)
            {
                List<string> statsList = new List<string>
                {
                    "Range: " + Radius.ToString(),
                    "Damage: " + Damage.ToString(),
                    "Fire Rate: " + fireRate.ToString(),
                };

                stats.UpdateText(statsList);
            }
            else
            {
                stats.ResetText();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            sprite.SetActive(false);
            stats.ResetText();
        }
    }

    public void OnColliderEnter(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        Damagable enemy = collision.gameObject.GetComponent<Damagable>();

        attackManager.AddEnemy(enemy);
    }

    public void OnColliderExit(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        Damagable enemy = collision.gameObject.GetComponent<Damagable>();

        attackManager.RemoveEnemy(enemy);
    }

    //private void OnMouseAbove()
    //{
    //    hovered = true;
    //}

    private void OnMouseOver()
    {
        hovered = true;
    }

    private void OnMouseExit()
    {
        hovered = false;
    }

    public void UpdateStats()
    {
        attackManager.SetDamage(damage, DamageBoost);
        attackManager.SetFireRate(fireRate);
    }
}
