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
    private Damagable damagable;
    private Animator anim;
    private WaypointReceiver wpr;
    private GameManager gm;

    // Fields
    [SerializeField] private float speed = 2.0f;

    private bool isBattling = false;

    public Vector2 SpawnOffset { get; set; }
    public Attacker Attacker { get { return attacker; } }
    public Damagable Damagable { get { return damagable; } }

    // Properties
    private Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.y); }
    }

    // Debug
    public TroopEngager target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        engager = GetComponent<EnemyEngager>();
        attacker = GetComponent<Attacker>();
        damagable = GetComponent<Damagable>();
        anim = GetComponent<Animator>();
        wpr = GetComponent<WaypointReceiver>();
        GameObject gameManager = GameObject.Find("GameManager");
        gm = gameManager.GetComponent<GameManager>();
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
        
        if (damagable.Health + damagable.HealthBoost  <= 0)
        {
            gm.Score++;
        }
        else
        {
            gm.EnemyEscaped();
        }
    }

    public void SetHealthBoost(float val)
    {
        //damagable.HealthBoost = val;
    }

    public void SetDamageBoost(float val)
    {
        //attacker.AttackBoost = val;
    }
}
