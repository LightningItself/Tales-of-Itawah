using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInhibitor : MonoBehaviour
{
    private TowerPlacementManager placementManager;

    private void Start()
    {
        placementManager = GameObject.Find("TowerPlacementManager").GetComponent<TowerPlacementManager>();
        if (placementManager.Inhibitors != null) placementManager.Inhibitors.Add(this);
    }

    private void OnMouseEnter()
    {
        placementManager.CanPlace = false;
        Debug.Log("MENTER");
    }

    private void OnMouseExit()
    {
        placementManager.CanPlace = true;
    }

    private void TowerPlacementManagerInitialized()
    {
        placementManager.Inhibitors.Add(this);
    }
}
