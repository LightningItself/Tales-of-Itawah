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

    private int currentTroopCount = 0;
    private bool hovered;
    private bool spawning = true;
    private Vector3 spawnerPosition;

    public Vector3 MarkerPosition { get; private set; }
    public bool Selected { get; private set; }
    public float InfluenceRangeRadius { get { return influenceRangeRadius; } }

    private Vector2 SqawnOffsetDir
    {
        get
        {
            float angle = currentTroopCount * 2f * Mathf.PI / barrackTroopCount;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        Selected = false;

        br = GetComponentInChildren<BarrackRange>();

        bm = GetComponentInChildren<BarrackMarker>();

        spawnerPosition = Spawner.transform.position;
    }

    private void Update()
    {
        ActivateChildren();


        // Mouse input
        if(Input.GetMouseButtonDown(0))
        {
            SelectedToggleOnMouseClick();
        }

        // Spawn
        SpawnTroop();
    }

    private void ActivateChildren()
    {
        // Activate Children
        br.gameObject.GetComponent<SpriteRenderer>().enabled = Selected;
        bm.gameObject.GetComponent<SpriteRenderer>().enabled = Selected;
    }

    private void SelectedToggleOnMouseClick()
    {
        bool prevSelected = Selected;
        Selected = hovered || br.Hovered;

        if (Selected && prevSelected)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 worldPos2D = new(worldPos.x, worldPos.y);

            MarkerPosition = worldPos2D;
        }
    }

    private void SpawnTroop()
    {
        if (!spawning || currentTroopCount >= barrackTroopCount) return;

        StartCoroutine(SpawnTroopCoroutine());
    }

    IEnumerator SpawnTroopCoroutine()
    {
        // Instantiate
        GameObject troop = Instantiate(BarrakTroopPrefab, spawnerPosition, Quaternion.identity);

        // Initialize
        Vector2 offset = SqawnOffsetDir * hoardRadius;

        BarrackTroopController controller = troop.GetComponent<BarrackTroopController>();
        controller.Target = bm.transform;
        controller.Offset = offset;
        controller.Barrack = this;


        currentTroopCount++;

        spawning = false;
        yield return new WaitForSeconds(spawnDelay);
        spawning = true;
    }

    private void OnMouseEnter()
    {
        hovered = true;
    }

    private void OnMouseExit()
    {
        hovered = false; 
    }

    public void DecreaseCurrentTroopCount()
    {
        currentTroopCount--;
    }
}
