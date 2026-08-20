using UnityEngine;
using UnityEngine.Tilemaps;

public class Seed : MonoBehaviour
{   
    [SerializeField] private Plant Plant;
    [SerializeField] private Item SeedBag;
    [SerializeField] private Tile soil;
    private void Planting()
    {
        Vector3 Mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Mousepos.z = 0;
        Vector3Int TargetTilePos = Vector3Int.FloorToInt(Mousepos);
        
        TileBase TilePointedAt = TileMap.Instance.Map.GetTile(TargetTilePos);

        if (TilePointedAt == null)  //check if the plant could be planted here
        {
            return;
        }
        
        Debug.Log($"Found tile: {TilePointedAt.name}");

        if (!HotBarController.Instance.ThisItemInHotbar(SeedBag.Name))//check if seed in hotbar
        {
            Debug.LogWarning("No SeedBag found in Hotbar");
            return ;
        }

        if (!HotBarController.Instance.HoldingThis(this.GetComponent<Item>()))
        {
            Debug.LogWarning("Not Holding Seed Right Now!");
        }

        if (!IsSoil(TilePointedAt))
        {
            Debug.LogWarning("Cannot Plant Here!");
            return ;
        }


        Debug.Log("Planting Seed!");
        Plant NewPlant = Instantiate(Plant);
        NewPlant.transform.position = TargetTilePos;
        NewPlant.transform.position += new Vector3(0.5f,0.5f,0);
        SeedBag.NumbersOfItem-=1;
    }

    private bool IsSoil(TileBase TileCurrentlyChecking)
    {
        return TileCurrentlyChecking == soil;
    }
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
            Planting();
    }
}
