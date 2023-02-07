using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private List<GameObject> Towers;
    [SerializeField] private List<GameObject> TowerMarkers;
    [SerializeField] private GameObject TowerSelector;
    [SerializeField] private GameObject env;

    private GameObject selectedTowerMarker;

    private int selected = -1;

    public bool CanPlace { get; set; }
    public List<TowerInhibitor> Inhibitors { get; set; }

    public bool canPlace;

    private void Start()
    {
        selectedTowerMarker = null;
        CanPlace = true;
        Inhibitors = new List<TowerInhibitor>();
        env.BroadcastMessage("TowerPlacementManagerInitialized");
    }

    // Update is called once per frame
    void Update()
    {
        canPlace = CanPlace;
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

        if (Input.GetMouseButtonDown(0) && selected > -1 && CanPlace) 
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
            foreach(TowerInhibitor i in Inhibitors)
            {
                i.gameObject.SetActive(false);
            }
            Destroy(selectedTowerMarker);
            return;
        }

        if (selectedTowerMarker != null)
        {
            Destroy(selectedTowerMarker);
        }
    
        selectedTowerMarker = Instantiate(TowerMarkers[selected], cam.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        foreach (TowerInhibitor i in Inhibitors)
        {
            i.gameObject.SetActive(true);
        }
    }
}
