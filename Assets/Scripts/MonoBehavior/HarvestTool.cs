using UnityEngine;
public class HarvestTool : MonoBehaviour
{

    public bool CanUseTool()
    {
        if (HotBarController.Instance.ThisItemInHotbar(this.GetComponent<Item>().Name) //does player have required tool in inventory?
        && HotBarController.Instance.HoldingThis(this.GetComponent<Item>())) //is player holding the required tool right now?
        {
            return true;
        }
        return false;
    }

}
