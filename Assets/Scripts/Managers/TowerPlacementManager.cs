using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private List<GameObject> Towers;
    [SerializeField] private List<GameObject> TowerMarkers;
    [SerializeField] private GameObject TowerSelector;

    private GameObject selectedTower;
    private GameObject selectedTowerMarker;

    private int selected = -1;

    private void Start()
    {
        selectedTowerMarker = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(selectedTowerMarker != null)
        {
            Vector3 towerPos = cam.ScreenToWorldPoint(Input.mousePosition);
            towerPos.z = -3;
            selectedTowerMarker.transform.position = towerPos;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Select(-1);
            TowerSelector.SetActive(!TowerSelector.activeSelf);
        }

        if (Input.GetMouseButtonDown(0) && selected > -1)
        {
            Vector3 towerPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Pushed");
            Instantiate(Towers[selected], new Vector3(towerPos.x, towerPos.y, -1), Quaternion.identity);
            GameObject.Find("GameManager").GetComponent<GameManager>().TowerBuilt();
            TowerSelector.SetActive(!TowerSelector.activeSelf);
            Select(-1);
        }
    }

    public void Select(int newSelected)
    {
        selected = newSelected;

        if (selected == -1)
        {
            Destroy(selectedTowerMarker);
            return;
        }

        selectedTower = Towers[selected];

        if (selectedTowerMarker != null)
        {
            Destroy(selectedTowerMarker);
        }
    
        selectedTowerMarker = Instantiate(TowerMarkers[selected], cam.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        
    }
}
