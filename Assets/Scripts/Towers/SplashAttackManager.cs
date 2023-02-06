using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAttackManager : MonoBehaviour, ITowerAttackManager
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireDuration;
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private float splashRadius;
    [SerializeField] private float zSpeed;

    private List<Damagable> enemyList;

    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = true;
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

        if (enemyList.Count == 0 || !isAttacking) return;
        //

        Vector2 target = new(enemyList[0].transform.position.x, enemyList[0].transform.position.y);

        StartCoroutine(AttackCoroutine(target));
    }

    IEnumerator AttackCoroutine(Vector2 target)
    {
        Debug.Log(target);
        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();

        proj.Target = target;
        proj.Damage = damage;
        proj.SplashRadius = splashRadius;
        proj.Speed = (target - new Vector2(transform.position.x, transform.position.y)).magnitude / fireDuration;
        proj.ZSpeed = zSpeed;
        proj.ZAccel = 2 * zSpeed / fireDuration;
        //proj.SetCollider();

        isAttacking = false;
        yield return new WaitForSeconds(1 / fireRate);
        isAttacking = true;
    }

    public void AddEnemy(Damagable enemy)
    {
        enemyList.Add(enemy);
    }

    public void RemoveEnemy(Damagable enemy)
    {
        enemyList.Remove(enemy);
    }
}
