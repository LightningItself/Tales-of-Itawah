using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngager : MonoBehaviour
{
    private EnemyController par;

    public int AttackerCount { get; private set; }
    public TroopEngager Target { get; set; }

    //Debug
    public float attackerCount;

    private void Start()
    {
        par = GetComponentInParent<EnemyController>();
    }

    private void Update()
    {
        attackerCount = AttackerCount;
    }

    public void OnEngage(TroopEngager troop)
    {   
        AttackerCount++;
        Target = troop;
        troop.OnEngage(this);
        par.OnEngage();
    }

    public void OnDisengage()
    {
        AttackerCount--;
    }

    public void OnBeginBattle()
    {
        par.OnBeginBattle();
    }

}
