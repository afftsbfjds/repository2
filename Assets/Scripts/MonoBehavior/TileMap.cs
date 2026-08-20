using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMap : MonoBehaviour
{
    public static TileMap Instance { get; private set; }

    public Tilemap Map;
    [SerializeField] private Tile tile;
    [SerializeField] private Player player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    public void ChangeTile()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (player.GetDistance(player.transform.position, mousepos) < player.InteractRange)
        {
            Map.SetTile(Map.WorldToCell(Vector3Int.FloorToInt(mousepos)), tile); // tile is your chosen Tile/TileBase
        }
        //Debug.Log(DirFacing.ToString());
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeTile();
        }
        
    }
}
