using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    // Components
    private CircleCollider2D col;

    // Fields
    [SerializeField] private float radius;
    List<EnemyController> enemyList;

    private void Start()
    {
        col = GetComponent<CircleCollider2D>();
        col.radius = radius;

        enemyList = new List<EnemyController>();
    }

    private void Update()
    {
        
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
}
