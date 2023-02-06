using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSelectorButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int index;

    private TowerSelector ts;

    public void OnPointerClick(PointerEventData eventData)
    {
        ts.SelectTower(index);
    }

    // Start is called before the first frame update
    void Start()
    {
        ts = GetComponentInParent<TowerSelector>();
    }
}
