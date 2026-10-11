using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Output")]
    public Item Output1;
    public  Item Output2;
    public Item OutputRandom;


    [Space]
    [Space]
    [Space]


    [Header("Numbers Of Item Giving From Output")]
    public int amount1;
    public int amount2;
    public int min;
    public int max;
    public ToolConfig[] ToolRequired;
    /// <summary>
    /// 
    /// </summary>

    public enum ObjectType
    {
        Harvestable,
        Convertable
    };
    public ObjectType Type;

    public virtual void Interact()
    {
        if(PickAndDrop.Instance==null)
            return;
        if(Output1!= null)
            PickAndDrop.Instance.DropItem(Output1,amount1,transform.position);
        if(Output2!= null)
            PickAndDrop.Instance.DropItem(Output2,amount2,transform.position);
        if(OutputRandom != null)
            PickAndDrop.Instance.DropItem(OutputRandom,Random.Range(min,max),transform.position);
        Destroy(gameObject);
    }

    public bool CanInteractWith()
    {
        if (ToolRequired == null || ToolRequired.Length == 0)   /// DOES REQUIRE TOOL?
            return true;

        if (HotBarController.Instance == null)
            return false;

        Item heldItem = HotBarController.Instance.CurrentItemHeld;
        if (heldItem == null || heldItem.Data == null || heldItem.Data.config == null)
            return false;

        foreach (ToolConfig toolAllowed in ToolRequired)
        {
            if (heldItem.Data.config == toolAllowed)
            {
                if (heldItem.Data.behavior is ToolBehavior toolBehavior)
                    return toolBehavior.CanUseTool();
                return false;
            }
                
        }

        return false;
    }



}
