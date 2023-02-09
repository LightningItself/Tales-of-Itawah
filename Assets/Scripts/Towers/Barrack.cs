using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour
{
    // Components
    private BarrackRange br;
    private BarrackMarker bm;
    private Stats stats;

    // Fields
    [SerializeField] private float influenceRangeRadius;
    [SerializeField] private Transform Spawner;
    [SerializeField] private GameObject BarrakTroopPrefab;
    [SerializeField] private float troopDamage;
    [SerializeField] private float troopHealth;
    [SerializeField] private float troopAttackRate;
    [SerializeField] private int barrackTroopCount;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float hoardRadius;
    [SerializeField] private float hoardInfluenceRadius;
    [SerializeField] private int code;

    private bool hovered;
    private bool spawning = true;
    private Vector3 spawnerPosition;
    private Stack<int> troopsToBeSpawnwed;

    public Vector3 MarkerPosition { get; private set; }
    public bool Selected { get; private set; }
    public float InfluenceRangeRadius { get { return influenceRangeRadius + RangeBoost; } }
    public int Code { get { return code; } }

    public float TroopDamageBoost { get; set; }
    public float TroopHealthBoost { get; set; }
    public int TroopCountBoost { get; set; }
    public float RangeBoost { get; set; }

    public int BarrackTroopCount { get { return barrackTroopCount + TroopCountBoost; } }
    
    public bool _hovered;
    public bool selected;

    // Start is called before the first frame update
    private void Start()
    {
        Selected = false;

        br = GetComponentInChildren<BarrackRange>();
        bm = GetComponentInChildren<BarrackMarker>();
        bm.GetComponent<CircleCollider2D>().radius = hoardInfluenceRadius;
        stats = GameObject.Find("Stats").GetComponent<Stats>();

        br.gameObject.SetActive(false);

        spawnerPosition = Spawner.transform.position;
        MarkerPosition = spawnerPosition;
    }

    private void Update()
    {
        _hovered = hovered;
        selected = Selected;

        // Mouse input
        if(Input.GetMouseButtonDown(0))
        {
            
            SelectedToggleOnMouseClick();
        }

        // Spawn
        SpawnTroop();
    }

    private void SelectedToggleOnMouseClick()
    {
        if (!hovered && !br.gameObject.activeSelf) return;

        if (hovered && !br.gameObject.activeSelf)
        {
            Debug.Log("Show Stat");
            br.gameObject.SetActive(true);

            List<string> statsList = new List<string>
            {
                "Range: " + InfluenceRangeRadius.ToString(),
                "Troop Count: " + barrackTroopCount.ToString(),
                "Troop Damage: " + troopDamage.ToString(),
                "Troop Health: " + troopHealth.ToString(),
            };

            stats.UpdateText(statsList, gameObject);

            return;
        }

        if(!hovered && br.Hovered)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 worldPos2D = new(worldPos.x, worldPos.y);

            MarkerPosition = worldPos2D;
            BroadcastMessage("MarkerChanged");
            return;
        }

        if(!hovered && !br.Hovered)
        {
            br.gameObject.SetActive(false);
            stats.ResetText(gameObject);
        }
    }

    private void SpawnTroop()
    {
        if (!spawning || troopsToBeSpawnwed.Count == 0) return;

        StartCoroutine(SpawnTroopCoroutine());
    }

    private Vector2 SpawnOffsetDir(int troopNumber)
    {    
        float angle = troopNumber * 2f * Mathf.PI / BarrackTroopCount;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    IEnumerator SpawnTroopCoroutine()
    {
        spawning = false;
        yield return new WaitForSeconds(spawnDelay);
        spawning = true;

        // Instantiate
        GameObject troop = Instantiate(BarrakTroopPrefab, spawnerPosition, Quaternion.identity);

        BarrackTroopController con = troop.GetComponent<BarrackTroopController>();
        con.SetDamage(troopDamage, TroopDamageBoost);
        con.SetHealth(troopHealth, TroopHealthBoost);
        con.SetAttackRate(troopAttackRate);

        troop.transform.SetParent(transform);

        // Initialize
        Vector2 offset = SpawnOffsetDir(troopsToBeSpawnwed.Peek()) * hoardRadius;

        BarrackTroopController controller = troop.GetComponent<BarrackTroopController>();
        controller.Target = bm.transform;
        controller.Offset = offset;
        controller.Barrack = this;
        controller.BarrackCode = code;
        controller.TroopNumber = troopsToBeSpawnwed.Peek();

        troopsToBeSpawnwed.Pop();
    }

    private void OnMouseEnter()
    {
        hovered = true;
    }

    private void OnMouseExit()
    {
        hovered = false; 
    }

    public void DecreaseCurrentTroopCount(int troopNumber)
    {
        troopsToBeSpawnwed.Push(troopNumber);
    }

    public void UpdateStats()
    {
        troopsToBeSpawnwed = new Stack<int>();
        for (int i = 0; i < BarrackTroopCount; i++)
        {
            troopsToBeSpawnwed.Push(i);
        }
    }
}
