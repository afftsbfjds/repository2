using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TilesChangingTool : MonoBehaviour
{
    public AnimatorOverrideController overrideController;
    [SerializeField] private AnimationClip InteractUp;
    [SerializeField] private AnimationClip InteractDown;
    [SerializeField] private AnimationClip InteractLeft;
    [SerializeField] private AnimationClip InteractRight;


    private Tile OutputTile;
    private TileBase TargetTile;
    public bool CanUseTool()
    {
        return HotBarController.Instance.CurrentItemHeld.Name == this.GetComponent<Item>().Name;
        //check if currently holding a tool
    }

    public void StartUseTool()
    {
        overrideController["UseToolDown"] = InteractDown;
        overrideController["UseToolLeft"] = InteractLeft;
        overrideController["UseToolRight"] = InteractRight;
        overrideController["UseToolUp"] = InteractUp;
    }

    private TileBase CheckTile()
    {
        Vector3 Mousepos = Input.mousePosition;
        Vector3Int TargetTilePos = TileMap.Instance.Map.WorldToCell(Mousepos);
        return TileMap.Instance.Map.GetTile(TargetTilePos);
    }

    public void Use_Tile_Changing_Tool_On(InputAction.CallbackContext context)
    {
        if(!CanUseTool())
            return ;
        
        
    }
}
