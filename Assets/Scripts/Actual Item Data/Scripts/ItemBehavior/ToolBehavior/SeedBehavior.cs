using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "SeedBehavior", menuName = "Scriptable Objects/itembehavior/ToolBehavior/SeedBehavior")]
public class SeedBehavior : ToolBehavior
{
    [SerializeField] private TileBase PlantingTile;
    public override void Use( GameObject target,Vector3Int TargetTileloc)
    {
        if(HotBarController.Instance == null ||
            HotBarController.Instance.CurrentItemHeld == null ||
            HotBarController.Instance.CurrentItemHeld.Data == null)
        {
            Debug.LogWarning("SeedBehavior.Use: Planting Requires Holding Seed!");
            return;
        }
        if(ToolData == null)
        {
            Debug.LogWarning("SeedBehavior.Use: ToolData is null.");
            return ;
        }
        if(ToolData.Tooltype != ToolConfig.ToolType.Seed)
        {
            Debug.LogWarning("SeedBehavior.Use: ToolData.Tooltype is not Seed.");
            return ;
        }
        if(TileMap.Instance == null || TileMap.Instance.Map == null)
        {
            Debug.LogWarning("SeedBehavior.Use: Map Is Not Loaded Or Bugged.");
            return ;
        }
        if(TileMap.Instance.Map.GetTile(TargetTileloc) == null)
        {
            Debug.LogWarning("SeedBehavior.Use: No tile at target position.");
            return;
        }
        TileBase targetTile = TileMap.Instance.Map.GetTile(TargetTileloc);      //PLANTING PLACE
        if (targetTile != PlantingTile)
        {
            Debug.Log("SeedBehavior.Use: Cannot Plant Here!");
            return;
        }
        Vector3 PlantPos = TileMap.Instance.Map.GetCellCenterWorld(TargetTileloc); //plant position
        if(ToolData.Plants == null)
            return;
        StartAnim(ToolData.DefaultAnimator);
    }
    public override void FinishUsingTool(Object target,Vector3 Position)
    {
        ToolData.DefaultAnimator.SetBool("UseTool",false);
        GameObject Plant = Instantiate(ToolData.Plants);
        Plant.transform.position = Position+new Vector3(0.5f,0.5f,0);
        
        if(BehaviorOwner.NumbersOfItem>=1)
            BehaviorOwner.NumbersOfItem-=1;
        if(BehaviorOwner.NumbersOfItem<=0)
            Destroy(BehaviorOwner.gameObject);
        
    }
}
