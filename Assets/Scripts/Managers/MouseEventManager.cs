using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventManager : MonoBehaviour
{
    private List<GameObject> towers;


    // Start is called before the first frame update
    void Start()
    {
        towers = new List<GameObject>();

        towers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tower"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}

