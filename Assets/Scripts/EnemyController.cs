using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // Components
    private List<Vector2> waypoints;
    private SpriteRenderer sprite_renderer;

    [SerializeField] private Image healthBar;

    // Fields
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float maxHealth = 50.0f;
    [SerializeField] private float health = 50.0f;

    private int currentNode;
    private int totalNodes;

    public Vector2 SpawnOffset { get; set; }

    // Properties
    private Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.y); }
    }

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

        // Sprite Renderer
        sprite_renderer = GetComponent<SpriteRenderer>();

        
    }

    private void Update()
    {
        // Movement
        Movement();

        // Waypoints
        UpdateWayPoints();

        // Update Health bar
        UpdateHealthBar();
    }

    private void Movement()
    {
        // Calculate direction
        Vector2 dir = waypoints[currentNode] - Position + SpawnOffset;
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
        transform.Translate(dir * Time.deltaTime * speed);
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

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyDamage(float d, System.Action<float> callback)
    {
        health -= d;
        callback(health);
    }

}
