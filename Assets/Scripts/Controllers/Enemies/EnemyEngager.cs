using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngager : MonoBehaviour
{
    private EnemyController par;
    private List<int> ranges;
    private List<TroopEngager> targets;

    public int AttackerCount { get { return targets.Count; } }
    public TroopEngager Target { get { return targets.Count == 0 ? null : targets[0]; } }


    //Debug
    public float attackerCount;
    public List<int> _ranges;
    public List<TroopEngager> _targets;
    private Animator anim;

    private void Start()
    {
        par = GetComponentInParent<EnemyController>();
        ranges = new List<int>();
        targets = new List<TroopEngager>();
        anim = gameObject.GetComponent<Animator>();
        
    }

    private void Update()
    {
        attackerCount = AttackerCount;
        _ranges = ranges;
        _targets = targets;
    }

    public void Engage(TroopEngager troop)
    {   
        targets.Add(troop);
        troop.Engage(this);
        par.OnEngage();
    }

    public void Disengage(TroopEngager troop)
    {
        targets.Remove(troop);
    }

    public void OnBeginBattle()
    {
        par.OnBeginBattle();
        anim.Play("Attack");
    }

    public void AddRange(int code)
    {
        ranges.Add(code);
    }

    public void RemoveRange(int code)
    {
        ranges.Remove(code);
    }

    public bool FindRange(int code)
    {
        foreach (int range in ranges)
        {
            if (range == code) return true;
        }
        return false;
    }
}
