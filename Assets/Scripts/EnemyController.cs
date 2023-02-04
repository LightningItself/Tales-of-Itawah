using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /*[SerializeField] private float moveSpeed;

    private Vector3 CurrentPointPosition;

    

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }*/

    // Components
    private List<Vector2> waypoints;
    private SpriteRenderer sprite_renderer;

    // Fields
    [SerializeField] private float speed = 2.0f;

    private int currentNode;
    private int totalNodes;

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
        // Calculate direction
        Vector2 dir = waypoints[currentNode] - Position;
        dir.Normalize();

        if(dir.x < -0.01)
        {
            sprite_renderer.flipX = true;
        }else if(dir.x > 0.01)
        {
            sprite_renderer.flipX = false;
        }

        // Move the game object
        transform.Translate(dir * Time.deltaTime * speed);

        // Check for reaching the game object
        if((Position - waypoints[currentNode]).magnitude <= 0.5) {
            currentNode++;

            // Destroy if completes the path
            if(currentNode == totalNodes)
            {
                Destroy(gameObject);
            }
        }
    }

}
