using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/itemdata")]
public class ItemData : ScriptableObject
{
    public int ID;
    [HideInInspector]public Item owner; 
    public Sprite icon;
    public string Name;
    public int maxStack = 99;
    public bool isStackable = true;
    public BehaviorConfig config;
    public ItemBehavior behavior;
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
