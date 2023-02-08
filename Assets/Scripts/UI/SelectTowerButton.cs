using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectTowerButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject TowerSelector;

    private GameManager manager;
    private Button btn;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        btn = GetComponent<Button>();
    }

    private void Update()
    {
        btn.interactable = manager._Time > manager.TowerBuildCost;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(manager._Time <= manager.TowerBuildCost)
        {
            return;
        }

        TowerSelector.SetActive(!TowerSelector.activeSelf);
        TowerSelector.GetComponent<TowerSelector>().SelectTower(-1);
    }
}
