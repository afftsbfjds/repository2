using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMap : MonoBehaviour
{
    [SerializeField] private Tilemap Map;
    [SerializeField] private Tile tile;
    [SerializeField] private Player player;

    private Vector3Int Tilelocation;
    public void ChangeTile()
    {
        Tilelocation = Vector3Int.FloorToInt
        (player.transform.position + new Vector3(player.LastDirection.x,player.LastDirection.y,0));//convert playerpos to tile postion
        
    }

    void Update()
    {
        
    }
}
