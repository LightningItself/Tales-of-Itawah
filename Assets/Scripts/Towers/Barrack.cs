using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour
{
    // Components
    private BarrackRange br;
    private BarrackMarker bm;

    // Fields
    [SerializeField] private float influenceRangeRadius;
    [SerializeField] private Transform Spawner;
    [SerializeField] private GameObject BarrakTroopPrefab;
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
    public float InfluenceRangeRadius { get { return influenceRangeRadius; } }
    public int Code { get { return code; } }

    public bool _hovered;
    public bool selected;

    // Start is called before the first frame update
    private void Start()
    {
        Selected = false;

        br = GetComponentInChildren<BarrackRange>();
        bm = GetComponentInChildren<BarrackMarker>();
        bm.GetComponent<CircleCollider2D>().radius = hoardInfluenceRadius;

        br.gameObject.SetActive(false);
        bm.gameObject.SetActive(false);

        spawnerPosition = Spawner.transform.position;
        MarkerPosition = spawnerPosition;

        troopsToBeSpawnwed = new Stack<int>();
        for(int i = 0; i < barrackTroopCount; i++)
        {
            troopsToBeSpawnwed.Push(i);
        }
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
            br.gameObject.SetActive(true);
            bm.gameObject.SetActive(true);
            return;
        }

        if(!hovered && br.Hovered)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 worldPos2D = new(worldPos.x, worldPos.y);

            MarkerPosition = worldPos2D;
            return;
        }

        if(!hovered && !br.Hovered)
        {
            br.gameObject.SetActive(false);
            bm.gameObject.SetActive(false);
        }
    }

    private void SpawnTroop()
    {
        if (!spawning || troopsToBeSpawnwed.Count == 0) return;

        StartCoroutine(SpawnTroopCoroutine());
    }

    private Vector2 SpawnOffsetDir(int troopNumber)
    {    
        float angle = troopNumber * 2f * Mathf.PI / barrackTroopCount;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    IEnumerator SpawnTroopCoroutine()
    {
        spawning = false;
        yield return new WaitForSeconds(spawnDelay);
        spawning = true;

        // Instantiate
        GameObject troop = Instantiate(BarrakTroopPrefab, spawnerPosition, Quaternion.identity);
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
}
