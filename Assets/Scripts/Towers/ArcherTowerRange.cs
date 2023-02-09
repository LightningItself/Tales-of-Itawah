using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerRange : MonoBehaviour
{
    private ArcherTower par;

    

    private void Start()
    {
        par = GetComponentInParent<ArcherTower>();
        GetComponent<CircleCollider2D>().radius = par.Radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        par.OnColliderEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        par.OnColliderExit(collision);
    }
}
