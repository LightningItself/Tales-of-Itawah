using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    // Components
    private CircleCollider2D col;
    private ITowerAttackManager attackManager;

    // Fields
    [SerializeField] private float radius;

    private void Start()
    {
        col = GetComponent<CircleCollider2D>();
        attackManager = GetComponent<ITowerAttackManager>();
        col.radius = radius;
    }

    private void Update()
    {
        attackManager.Attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        Damagable enemy = collision.gameObject.GetComponent<Damagable>();

        attackManager.AddEnemy(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        Damagable enemy = collision.gameObject.GetComponent<Damagable>();

        attackManager.RemoveEnemy(enemy);
    }
}
