using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ToolInteractData", menuName = "Scriptable Objects/AxeInteract")]
public class ToolInteract : ScriptableObject
{
    public void Harvest(Interactable interactable)
    {
        
    }

    public void ChangeTile(Tile OutputTile)
    {
        Debug.Log("Change Tile");
    }

    
    
}
