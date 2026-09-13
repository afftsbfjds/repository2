using UnityEngine;

[CreateAssetMenu(fileName = "ToolConfig", menuName = "Scriptable Objects/ItemData/ToolConfig")]
public class ToolConfig : BehaviorConfig
{
    public AnimatorOverrideController overrideController;
    [HideInInspector]public Animator DefaultAnimator;
    public AnimationClip InteractUp;
    public AnimationClip InteractDown;
    public AnimationClip InteractLeft;
    public AnimationClip InteractRight;

    public enum ToolType
    {
        Harvest,
        Convert,
        TileChanging,
        Seed
    }
    [Space]
    [Space]
    [Space]
    [Header("Seed Exclusive")]
    public GameObject Plants;

    public void BindAnimator(Animator animator)
    {
        DefaultAnimator = animator;
    }

    public ToolType Tooltype;
}