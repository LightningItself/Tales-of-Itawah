using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointReceiver : MonoBehaviour
{
    public List<Vector2> Waypoints { get; set; }

    private int currentNode;
    private int totalNodes;

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] wp = GameObject.Find("Waypoints").GetComponent<Waypoint>().Points;

        Waypoints = new List<Vector2>();

        foreach (Vector3 w in wp)
        {
            Waypoints.Add(new Vector2(w.x, w.y));
        }

        totalNodes = Waypoints.Count;
        currentNode = 0;
    }

    public void UpdateWayPoints(Vector2 pos)
    {
        // Check for reaching the game object
        if ((pos - Waypoints[currentNode]).magnitude <= 0.5)
        {
            currentNode++;

            // Destroy if completes the path
            if (currentNode == totalNodes)
            {
                Destroy(gameObject);
            }
        }
    }

    public Vector2 GetDir(Vector2 pos)
    {
        Vector2 dir = Waypoints[currentNode] - pos;
        dir.Normalize();
        return dir;
    }
}
