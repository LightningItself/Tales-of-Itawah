using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackTroopController : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private SpriteRenderer sprite_renderer;
    private CircleCollider2D range;
    private TroopEngager engager;
    private Attacker attacker;

    public Transform Target { get; set; }
    public Vector2 Position { get { return new Vector2(rb.position.x, rb.position.y); } }
    public Vector2 Offset { get; set; }
    public Barrack Barrack { get; set; }
    public int TroopNumber { get; set; }
    public int BarrackCode { get; set; }

    [SerializeField] private float speed;
    [SerializeField] private float rangeRadius;

    public bool isBattling = false;
    public bool hasReachedMarkerOnce = false;

    // Debug
    public EnemyEngager target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        range = GetComponentInChildren<CircleCollider2D>();
        engager = GetComponent<TroopEngager>();
        attacker = GetComponent<Attacker>();

        range.radius = rangeRadius;
        hasReachedMarkerOnce = false;
    }

    private void Update()
    {
        target = engager.Target;

        // Check for engager
        CheckEngager();

        // Attack
        if (isBattling)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {    
        // Movement
        if(!isBattling)
        {
            Movement();
        }
    }

    private void CheckEngager()
    {
        if(engager.Target == null)
        {
            isBattling = false;
        }
    }

    private void Attack()
    {
        if (engager.Target == null) return;

        Damagable enemy = engager.Target.GetComponent<Damagable>();

        attacker.Attack(enemy);
    }

    private void Movement()
    {
        if (Target == null) return;

        if(engager.Target == null)
        {
            hasReachedMarkerOnce = MoveTowards(Target) || hasReachedMarkerOnce;
        }
        else
        {
            if (MoveTowards(engager.Target.transform))
            {
                isBattling = true;
                engager.Target.OnBeginBattle();
            }
        }
    }

    private bool MoveTowards(Transform target)
    {
        /// Calculate direction
        Vector2 dir = new Vector2(target.position.x, target.position.y) - Position + Offset;

        // Check if target is away
        if (dir.magnitude <= 0.1) return true;

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

        return false;
    }

    public void OnRangeEnter(Collider2D collision)
    {
        EnemyEngager newEnemy = collision.GetComponent<EnemyEngager>();

        if (newEnemy == null || !newEnemy.CompareTag("Enemy") || !newEnemy.FindRange(BarrackCode) || !hasReachedMarkerOnce) return;
        Debug.Log("Enter");

        EnemyEngager targetEnemy = engager.Target;

        if (targetEnemy == null || targetEnemy.AttackerCount - 1 > newEnemy.AttackerCount) 
        {
            Debug.Log("LOLOL");
            SetTargetEnemy(newEnemy);
        }
    }

    private void SetTargetEnemy(EnemyEngager target)
    {

        if (engager.Target != null)
        {
            engager.Target.Disengage(engager);
        }

        target.Engage(engager);
        engager.Engage(target);
        isBattling = false;
    }

    public void OnEngage()
    {
        Debug.Log(engager.Target.name);
    }

    private void OnDestroy()
    {
        Barrack.DecreaseCurrentTroopCount(TroopNumber);
        if(engager.Target != null) engager.Target.Disengage(engager);
    }

    private void MarkerChanged()
    {
        hasReachedMarkerOnce = false;
        isBattling = false;
        Debug.Log("Marker Change");
        if(engager.Target != null) engager.Target.Disengage(engager);
        engager.Disengage();
    }
}
