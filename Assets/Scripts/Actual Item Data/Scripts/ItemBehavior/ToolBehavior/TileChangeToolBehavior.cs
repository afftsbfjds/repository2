using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileChangeToolBehavior", menuName = "Scriptable Objects/itembehavior/ToolBehavior/TileChangeToolBehavior")]
public class TileChangeToolBehavior : ToolBehavior
{
    public TileBase OutputTile;
    public override void Use(GameObject target, Vector3Int targetTileloc)
    {
        if(!(CanUseTool()))
        {
            Debug.LogWarning("CANNOT USE TOOL");
        }
            
        if(ToolData.Tooltype != ToolConfig.ToolType.TileChanging)
        {
            Debug.LogWarning("TileChangeToolBehavior.Use: ToolData.Tooltype is not Harvest.");
            return ;
        }
        if(TileMap.Instance == null || TileMap.Instance.Map == null)
        {
            Debug.LogWarning("TileChangeToolBehavior.Use: Map Is Not Loaded Or Bugged");
            return ;
        }
        if(TileMap.Instance.Map.GetTile(targetTileloc) == null)
        {
            Debug.LogWarning("TileChangeToolBehavior.Use: No tile at target position.");
            return;
        }
        TileBase targetTile = TileMap.Instance.Map.GetTile(targetTileloc);

        ///                             FUNCTION START HERE
        
        if(ToolData.DefaultAnimator == null)
        {
            Debug.LogWarning("TileChangeToolBehavior.Use: DefaultAnimator is null.");
            return ;
        }

        //Debug.Log("TileChangeToolBehavior.Use: starting Change Tile animation.");
        //Debug.Log("Used");
        StartAnim(ToolData.DefaultAnimator);

        
    }

    public override void FinishUsingTool(Object target,Vector3 Position)
    {
        if(!(target is TileBase))
        {
            return;
        }
        
        ToolData.DefaultAnimator.SetBool("UseTool",false);
        TileMap.Instance.Map.SetTile(TileMap.Instance.Map.WorldToCell(Position),OutputTile);
    }
}
