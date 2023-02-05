using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopEngager : MonoBehaviour
{
    private BarrackTroopController par;

    public EnemyEngager Target { get; set; }

    private void Start()
    {
        par = GetComponentInParent<BarrackTroopController>();
    }

    public void OnEngage(EnemyEngager target)
    {
        Target = target;
        par.OnEngage();
    }
}
