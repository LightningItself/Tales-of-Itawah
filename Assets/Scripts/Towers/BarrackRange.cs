using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackRange : MonoBehaviour
{
    // Components
    private Barrack bk;

    // Fields
    public bool Hovered { get; private set; }

    private void Start()
    {
        bk = GetComponentInParent<Barrack>();
        transform.localScale = new Vector3(bk.InfluenceRangeRadius * 2, bk.InfluenceRangeRadius * 2, 1);
    }

    private void OnMouseEnter()
    {
        Hovered = true;
    }

    private void OnMouseExit()
    {
        Hovered = false;
    }
}
