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
    [SerializeField] private float towerAttackBoost = 2f;
    [SerializeField] private float towerRangeBoost = 0.2f;
    [SerializeField] private float towerheathBoost = 2f;
    [SerializeField] private float towerCountBoost = 0.34f;

    private GameObject selectedTowerMarker;
    private GameManager gm;

    private int selected = -1;

    public bool CanPlace { get; set; }
    public List<TowerInhibitor> Inhibitors { get; set; }

    public bool canPlace;

    private void Awake()
    {
        selectedTowerMarker = null;
        CanPlace = true;
        Inhibitors = new List<TowerInhibitor>();
        env.BroadcastMessage("TowerPlacementManagerInitialized");

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
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

            GameObject tower = Instantiate(Towers[selected], new Vector3(towerPos.x, towerPos.y, -1), Quaternion.identity);
            tower.transform.SetParent(env.transform);

            if (selected == 1 || selected == 2)
            {
                ArcherTower con = tower.GetComponent<ArcherTower>();

                con.DamageBoost = gm.TowerUpgradeStatus * towerAttackBoost;
                con.RadiusBoost = gm.TowerUpgradeStatus * towerRangeBoost;
                
                con.UpdateStats();
            }
            else
            {
                Barrack con = tower.GetComponent<Barrack>();

                con.TroopDamageBoost = gm.TowerUpgradeStatus * towerAttackBoost;
                con.TroopHealthBoost = gm.TowerUpgradeStatus * towerheathBoost;
                con.RangeBoost = gm.TowerUpgradeStatus * towerRangeBoost;
                con.TroopCountBoost = (int) (gm.TowerUpgradeStatus * towerCountBoost);

                con.UpdateStats();
            }

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
