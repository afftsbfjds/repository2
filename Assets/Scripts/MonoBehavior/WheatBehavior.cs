using UnityEngine;
using UnityEngine.Tilemaps;

public class WheatBehavior : MonoBehaviour
{
    [SerializeField] private Tile soil;
    [SerializeField] private Tilemap Map;
    [SerializeField] private Item SeedBag;
    [SerializeField] private Sprite[] GrowthState;
    private bool IsSoil()
    {
        Vector3 Mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int TargetTilePos =  Map.WorldToCell(Mousepos);
        TileBase TilePointedAt = Map.GetTile(TargetTilePos);
        return TilePointedAt == soil;
    }

    private void Planting()
    {
        if (!HotBarController.Instance.ThisItemExist(SeedBag.Name))
            return ;
        
    }
}
