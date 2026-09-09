using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ConvertToolBehavior", menuName = "Scriptable Objects/itembehavior/ToolBehavior/ConvertToolBehavior")]
public class ConvertToolBehavior : ToolBehavior
{
    public override void Use( GameObject target,Vector3Int targetTile)
    {
        Debug.Log("Convert item");
        // conversion logic here
    }

    
}
