using UnityEngine;
public class HarvestTool : MonoBehaviour
{
    public AnimatorOverrideController overrideController;
    [SerializeField] private AnimationClip InteractUp;
    [SerializeField] private AnimationClip InteractDown;
    [SerializeField] private AnimationClip InteractLeft;
    [SerializeField] private AnimationClip InteractRight;
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


    public void Use_Harvest_Tool_On(Interactable Object)
    {
        if(Object.OutputRandom!=null)
            Object.OutputRandom.DropItem(Random.Range(Object.min,Object.max));
        if(Object.Output1!=null)
            Object.Output1.DropItem(Object.amount1);
        if(Object.Output2!=null)
            Object.Output2.DropItem(Object.amount2);
        Destroy(Object.gameObject);
    }
}
