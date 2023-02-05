using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackMarker : MonoBehaviour
{
    private Barrack bk;

    // Start is called before the first frame update
    void Start()
    {
        bk = GetComponentInParent<Barrack>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = bk.MarkerPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        EnemyEngager enemy = collision.GetComponent<EnemyEngager>();

        enemy.AddRange(bk.Code);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        EnemyEngager enemy = collision.GetComponent<EnemyEngager>();

        enemy.RemoveRange(bk.Code);
    }
}
