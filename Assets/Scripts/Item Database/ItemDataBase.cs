using System.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "Scriptable Objects/ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    public Item[] DataBase;

    public Item FindItem(string itemName)
    {
        foreach (var item in DataBase)
        {
            if (item != null && item.Name == itemName)
                return item;
        }
        return null;
    }


}
