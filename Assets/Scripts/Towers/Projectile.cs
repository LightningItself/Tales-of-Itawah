using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{ 
    public Vector2 Target { get; set; }
    public float Damage { get; set; }
    public float SplashRadius { get; set; }
    public float Speed { get; set; }
    public float ZSpeed { get; set; }
    public float ZAccel { get; set; }

    private Vector2 Position { get { return new Vector2(transform.position.x, transform.position.y); } }

    private CircleCollider2D col;
    private List<Damagable> enemies;

    [SerializeField] private Transform sprite;

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
        Vector3 dir = Target - Position;
        dir.Normalize();
        transform.Translate(dir * Speed * Time.deltaTime);
        if (sprite == null) return;
        sprite.Translate(new Vector3(0, ZSpeed, 0) * Time.deltaTime);

        ZSpeed -= ZAccel * Time.deltaTime;
    }

    private void CheckHit()
    {
        Debug.Log((Position - Target).magnitude);
        if((Position - Target).magnitude <= 0.01)
        {
            
            foreach(Damagable e in enemies)
            {
                if(e != null)
                {
                    e.ApplyDamage(Damage);
                }
            }
            ZAccel = 0;
            ZSpeed = 0;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        enemies.Add(collision.GetComponent<Damagable>());
    }
}
