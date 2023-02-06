using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 Target { get; set; }
    public float Damage { get; set; }
    public float SplashRadius { get; set; }
    public float Speed { get; set; }

    private Vector2 Position { get { return new Vector2(transform.position.x, transform.position.y); } }

    private CircleCollider2D col;
    private List<Damagable> enemies;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        enemies = new List<Damagable>();
    }

    // Update is called once per frame
    void Update()
    {
        col.radius = SplashRadius;

        // Move
        Move();

        // Destroy Check
        CheckHit();

    }

    private void Move()
    {
        Vector2 dir = Target - Position;
        dir.Normalize();

        transform.Translate(dir * Speed * Time.deltaTime);
    }

    private void CheckHit()
    {
        if((Position - Target).magnitude <= 0.1)
        {
            
            foreach(Damagable e in enemies)
            {
                if(e != null)
                {
                    e.ApplyDamage(Damage);
                }
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        enemies.Add(collision.GetComponent<Damagable>());
    }
}
