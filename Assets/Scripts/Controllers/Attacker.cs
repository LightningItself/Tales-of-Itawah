using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float attackRate;

    private bool isAttacking = true;
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Attack(Damagable enemy)
    {
        if (!isAttacking)
        {
            anim.Play("Run");
            return;
        }
        anim.Play("Attack");
        StartCoroutine(AttackCoroutine(enemy));
    }

    IEnumerator AttackCoroutine(Damagable enemy)
    {
        enemy.ApplyDamage(damage);
        isAttacking = false;
        yield return new WaitForSeconds(1 / attackRate);
        isAttacking = true;
    }
}
