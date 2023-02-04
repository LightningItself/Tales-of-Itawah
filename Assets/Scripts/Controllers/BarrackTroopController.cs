using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackTroopController : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private SpriteRenderer sprite_renderer;

    public Transform Target { get; set; }
    public Vector2 Position { get { return new Vector2(rb.position.x, rb.position.y); } }
    public Vector2 Offset { get; set; }

    [SerializeField] private float speed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (Target == null) return;
        /// Calculate direction
        Vector2 dir = new Vector2(Target.position.x, Target.position.y) - Position + Offset;

        // Check if target is away
        if (dir.magnitude <= 0.1) return;

        dir.Normalize();

        if (dir.x < -0.01)
        {
            sprite_renderer.flipX = true;
        }
        else if (dir.x > 0.01)
        {
            sprite_renderer.flipX = false;
        }

        // Move the game object
        rb.MovePosition(rb.position + dir * Time.deltaTime * speed);
    }
}
