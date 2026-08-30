using UnityEngine;

[CreateAssetMenu(fileName = "ToolData1", menuName = "Scriptable Objects/ToolData1")]
public class ToolData1 : ScriptableObject
{
    public AnimatorOverrideController overrideController;
    [SerializeField] private AnimationClip InteractUp;
    [SerializeField] private AnimationClip InteractDown;
    [SerializeField] private AnimationClip InteractLeft;
    [SerializeField] private AnimationClip InteractRight;

    public enum ToolType
    {
        Harvest,
        Convert,
        TileChanging
    }

    public ToolType Tooltype;



    public void StartUseTool()
    {
        overrideController["UseToolDown"] = InteractDown;
        overrideController["UseToolLeft"] = InteractLeft;
        overrideController["UseToolRight"] = InteractRight;
        overrideController["UseToolUp"] = InteractUp;
    }


}



