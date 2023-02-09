using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerAttackManager : MonoBehaviour, ITowerAttackManager
{
    private Attacker attacker;
    private List<Damagable> enemyList;
    // Start is called before the first frame update
    void Awake()
    {
        attacker = GetComponent<Attacker>();
        enemyList = new List<Damagable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        while (enemyList.Count > 0 && enemyList[0] == null)
        {
            enemyList.RemoveAt(0);
        }

        if (enemyList.Count == 0) return;

        attacker.Attack(enemyList[0]);
    }

    public void AddEnemy(Damagable enemy)
    {
        enemyList.Add(enemy);
    }

    public void RemoveEnemy(Damagable enemy)
    {
        enemyList.Remove(enemy);
    }

    public void SetDamage(float damage, float boost)
    {
        attacker.Damage = damage;
        attacker.AttackBoost = boost;
    }

    public void SetFireRate(float fireRate)
    {
        attacker.AttackRate = fireRate;
    }
}
