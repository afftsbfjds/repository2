using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "HarvestToolBehavior", menuName = "Scriptable Objects/itembehavior/ToolBehavior/HarvestToolBehavior")]
public class HarvestToolBehavior : ToolBehavior
{
    public override void Use(GameObject target, Vector3Int targetTile)
    {
        Debug.Log("StartUsing");
        if(ToolData == null)
        {
            Debug.LogWarning("HarvestToolBehavior.Use: ToolData is null.");
            return ;
        }

        if(ToolData.Tooltype != ToolConfig.ToolType.Harvest)
        {
            Debug.LogWarning("HarvestToolBehavior.Use: ToolData.Tooltype is not Harvest.");
            return ;
        }

        if(target == null)
        {
            Debug.LogWarning("HarvestToolBehavior.Use: target is null.");
            return;
        }
        
        Interactable Object = target.GetComponent<Interactable>();

        if(Object == null)
        {
            Debug.LogWarning("HarvestToolBehavior.Use: target has no Interactable component.");
            return ;
        }

        if(!Object.CanInteractWith())       //CAN INTERACT?
        {
            Debug.LogWarning("HarvestToolBehavior.Use: Object.CanInteractWith() returned false.");
            return ;
        }

        if(Object.Type != Interactable.ObjectType.Harvestable)
        {
            Debug.LogWarning("HarvestToolBehavior.Use: target is not Harvestable.");
            return ;
        }

        if(ToolData.DefaultAnimator == null)
        {
            Debug.LogWarning("HarvestToolBehavior.Use: DefaultAnimator is null.");
            return ;
        }

        Debug.Log("HarvestToolBehavior.Use: starting harvest animation.");
        Debug.Log("Used");
        StartAnim(ToolData.DefaultAnimator);
    }

    public override void FinishUsingTool(Object target,Vector3 Position)
    {
        if(target == null||!(target is GameObject))
            return;
        ToolData.DefaultAnimator.SetBool("UseTool",false);
        GameObject obj = (GameObject) target;
        Interactable Object = obj.GetComponent<Interactable>();
        if(Object.Output1 != null)
            PickAndDrop.Instance.DropItem(Object.Output1,Object.amount1,Position);
        if(Object.Output2 != null)
            PickAndDrop.Instance.DropItem(Object.Output2,Object.amount2,Position);
        if(Object.OutputRandom != null)
            PickAndDrop.Instance.DropItem(Object.OutputRandom,Random.Range(Object.min,Object.max),Position);
        Destroy(Object.gameObject);
    }
}
