using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // Components
    private SpriteRenderer spriteRender;
    private Rigidbody2D rb;
    private EnemyEngager engager;
    private Attacker attacker;
    private Animator anim;
    private WaypointReceiver wpr;

    // Fields
    [SerializeField] private float speed = 2.0f;

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
        rb = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        engager = GetComponent<EnemyEngager>();
        attacker = GetComponent<Attacker>();
        anim = GetComponent<Animator>();
        wpr = GetComponent<WaypointReceiver>();
    }

    private void Update()
    {
        target = engager.Target;

        // Waypoints
        //UpdateWayPoints();
        wpr.UpdateWayPoints(Position - SpawnOffset);

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
        Vector2 dir = wpr.GetDir(Position - SpawnOffset);

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

    public void OnEngage()
    {
        Debug.Log(engager.Target.name);
    }

    public void OnBeginBattle()
    {
        isBattling = true;
    }

    private void OnDestroy()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (GetComponent<Damagable>().Health <= 0)
        {
            gm.Score++;
        }
        else
        {
            gm.EnemyEscaped();
        }
    }
}
