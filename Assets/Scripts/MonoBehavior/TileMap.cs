using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMap : MonoBehaviour
{
    public static TileMap Instance { get; private set; }

    public Tilemap Map;
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

    public void ChangeTile(Tile tile,Vector3Int TileLocation)
    {
        Map.SetTile(Map.WorldToCell(TileLocation), tile); // tile is your chosen Tile/TileBase
        //Debug.Log(DirFacing.ToString());
        
    }
}
