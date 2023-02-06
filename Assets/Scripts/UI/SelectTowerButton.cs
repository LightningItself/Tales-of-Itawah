using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectTowerButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject TowerSelector;

    public void OnPointerClick(PointerEventData eventData)
    {
        TowerSelector.SetActive(!TowerSelector.activeSelf);
        TowerSelector.GetComponent<TowerSelector>().SelectTower(-1);
    }
}
