using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] 
    private Vector3[] points;

    public Vector3[] Points => points;
    public Vector3 CurrentPosition => _currentPosition;

    private bool _gameStarted;
    private Vector3 _currentPosition;


    // Start is called before the first frame update
    private void Start()
    {
        _gameStarted = true;
        _currentPosition = transform.position;
    }

    public Vector3 GetWayPointPosition(int index)
    {
        return CurrentPosition + Points[index];
    }

    private void OnDrawGizmos()
    {
        if(!_gameStarted && transform.hasChanged)
        {
            _currentPosition = transform.position;
        }
        for(int i=0;i<points.Length;i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(points[i] + _currentPosition, 0.5f);

            if(i < points.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[i] + _currentPosition, points[i + 1] + _currentPosition);
            }
        }
    }

}
