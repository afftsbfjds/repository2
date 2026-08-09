using UnityEngine;

public class Tree : MonoBehaviour
{
    public Item woodLog;
    public Item Sapling;

    public void DestroyObject()
    {
        woodLog.DropItem();
        Sapling.DropItem();
        Destroy(this.gameObject);
    }

}
