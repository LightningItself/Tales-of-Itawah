using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackMarker : MonoBehaviour
{
    private Barrack bk;

    // Start is called before the first frame update
    void Start()
    {
        bk = GetComponentInParent<Barrack>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = bk.MarkerPosition;
    }
}
