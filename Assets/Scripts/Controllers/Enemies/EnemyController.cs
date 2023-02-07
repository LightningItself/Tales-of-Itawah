using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // Components
    private List<Vector2> waypoints;
    private SpriteRenderer spriteRender;
    private Rigidbody2D rb;
    private EnemyEngager engager;
    private Attacker attacker;
    private Animator anim;

    // Fields
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float damage;
    [SerializeField] private float attackRate;

    private int currentNode;
    private int totalNodes;

    private bool isBattling = false;

    public Vector2 SpawnOffset { get; set; }

    // Properties
    private Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.y); }
    }

    // Debug
    public TroopEngager target;

    private void Start()
    {
        // Finds wayponts
        Vector3[] wp = GameObject.Find("Waypoints").GetComponent<Waypoint>().Points;

        waypoints = new List<Vector2>();

        foreach(Vector3 w in wp)
        {
            waypoints.Add(new Vector2(w.x, w.y));
        }

        totalNodes = waypoints.Count;
        currentNode = 0;

        
        rb = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        engager = GetComponent<EnemyEngager>();
        attacker = GetComponent<Attacker>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        target = engager.Target;

        // Waypoints
        UpdateWayPoints();

        // Check Engager
        CheckEngager();

        // Attack
        if (isBattling)
        {
            Attack();
            anim.Play("Attack");
        } else
        {
            anim.Play("Run");
        }
    }

    private void CheckEngager()
    {
        if (engager.Target == null)
        {
            isBattling = false;
        }
    }

    private void FixedUpdate()
    {
        // Movement
        if (!isBattling)
        {
            Movement();
        }
    }

    private void Attack()
    {
        if (engager.Target == null) return;

        Damagable troop = engager.Target.GetComponent<Damagable>();

        attacker.Attack(troop);
    }

    private void Movement()
    {
        // Calculate direction
        Vector2 dir = waypoints[currentNode] - Position + SpawnOffset;
        dir.Normalize();

        if (dir.x < -0.01)
        {
            spriteRender.flipX = true;
        }
        else if (dir.x > 0.01)
        {
            spriteRender.flipX = false;
        }

        // Move the game object
        rb.MovePosition(rb.position + dir * Time.deltaTime * speed);
    }

    private void UpdateWayPoints()
    {
        // Check for reaching the game object
        if ((Position - waypoints[currentNode] - SpawnOffset).magnitude <= 0.5)
        {
            currentNode++;

            // Destroy if completes the path
            if (currentNode == totalNodes)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnEngage()
    {
        Debug.Log(engager.Target.name);
    }

    public void OnBeginBattle()
    {
        isBattling = true;
    }
}
