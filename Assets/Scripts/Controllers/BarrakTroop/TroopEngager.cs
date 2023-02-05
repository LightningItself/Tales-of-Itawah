using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopEngager : MonoBehaviour
{
    private BarrackTroopController par;

    public EnemyEngager Target { get; private set; }

    private void Start()
    {
        par = GetComponentInParent<BarrackTroopController>();
    }

    public void Engage(EnemyEngager target)
    {
        Target = target;
        par.OnEngage();
    }

    public void Disengage()
    {
        Target = null;
    }

}
