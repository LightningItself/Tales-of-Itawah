using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    // Components
    private CircleCollider2D col;

    // Fields
    [SerializeField] private float radius;
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;

    private bool readyToFire = true;
    private List<EnemyController> enemyList;

    private void Start()
    {
        col = GetComponent<CircleCollider2D>();
        col.radius = radius;

        enemyList = new List<EnemyController>();
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (enemyList.Count == 0 || !readyToFire) return;

        StartCoroutine(DamageCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController controller = collision.gameObject.GetComponent<EnemyController>();

        enemyList.Add(controller);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        EnemyController controller = collision.gameObject.GetComponent<EnemyController>();

        enemyList.Remove(controller);
    }

    IEnumerator DamageCoroutine ()
    {
        if(enemyList.Count > 0)
        {
            while (enemyList.Count > 0 && enemyList[0] == null) enemyList.RemoveAt(0);

            enemyList[0].ApplyDamage(damage, health =>
            {
                if(health <= 0)
                {
                    enemyList.RemoveAt(0);
                }
            });
        }

        readyToFire = false;
        yield return new WaitForSeconds(1 / fireRate);
        readyToFire = true;
    }
}
