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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move
        Move();

        // Destroy Check
        CheckDestroy();

    }

    private void Move()
    {
        Vector2 dir = Target - Position;
        dir.Normalize();

        transform.Translate(dir * Speed * Time.deltaTime);
    }

    private void CheckDestroy()
    {
        if((Position - Target).magnitude <= 0.1)
        {
            Destroy(gameObject);
        }
    }
}
