using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class ItemBehavior : ScriptableObject
{
    public Item BehaviorOwner;
    public virtual void Use(GameObject target,Vector3Int TargetTile)
    {
        
    }
}



