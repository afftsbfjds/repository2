using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    private Item owner;
    public Sprite icon;
    public string Name;
    public int maxStack = 99;
    public bool isStackable = true;
    public ToolInteract Interact;

    public enum ITEMTYPE
    {
        Tool,
        Material,
        Consumable,
        Seed
    };
    public ITEMTYPE type;

    private void Start()
    {
        
    }

}
