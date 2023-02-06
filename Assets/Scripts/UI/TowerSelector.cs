using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    [SerializeField] private TowerPlacementManager manager;

    public void SelectTower(int index)
    {
        manager.Select(index);
    }
}
