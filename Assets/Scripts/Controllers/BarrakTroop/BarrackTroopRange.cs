using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackTroopRange : MonoBehaviour
{
    private BarrackTroopController con;

    private void Start()
    {
        con = GetComponentInParent<BarrackTroopController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        con.OnRangeEnter(collision);
    }
}
