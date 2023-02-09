using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerAttackManager 
{
    public void Attack();
    public void AddEnemy(Damagable enemy);
    public void RemoveEnemy(Damagable enemy);
    public void SetDamage(float damage, float boost);
    public void SetFireRate(float fireRate);
}
