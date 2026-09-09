using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptable Objects/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public ItemData[] database;

    public ItemData LookUp(int ID)
    {
        return database[ID];
    }
}
