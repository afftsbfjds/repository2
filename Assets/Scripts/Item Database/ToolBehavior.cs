using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ToolBehavior", menuName = "Scriptable Objects/itembehavior/toolbehavior")]
public class ToolBehavior : ItemBehavior
{
    public ToolConfig ToolData;
    public override void Use(GameObject target, Vector3Int targetTile)
    {
        Debug.Log("Use Tool");
    }

    public void StartAnim(Animator animator1)
    {
        StartUseTool();
        animator1.runtimeAnimatorController = ToolData.overrideController;       //prepare animations
        animator1.SetBool("UseTool",true);
    }

    public void StartUseTool()
    {
        ToolData.overrideController["UseHoeDown"] = ToolData.InteractDown;
        ToolData.overrideController["UseHoeLeft"] = ToolData.InteractLeft;
        ToolData.overrideController["UseHoeRight"] = ToolData.InteractRight;
        ToolData.overrideController["UseHoeUp"] = ToolData.InteractUp;
    }

    public virtual void FinishUsingTool(Object target,Vector3 Position)
    {
        // Empty default - each tool can override
    }

    public virtual bool CanUseTool()
    {
        if(HotBarController.Instance == null)
            return false;

        Item itemheld = HotBarController.Instance.CurrentItemHeld;


        if(itemheld != BehaviorOwner ||itemheld.Data == null||itemheld.Data.config == null||
        itemheld.Data.behavior == null)
            return false;
        if (ToolData == null)
            return false;
        if (ToolData.DefaultAnimator == null)
            return false;

        return true;
    }
}
