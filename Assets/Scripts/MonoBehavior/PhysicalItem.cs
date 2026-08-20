using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PhysicalItem : MonoBehaviour
{
     
    public Sprite PI_icon;
    public int Stack;
    public string ItemName;
    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private ItemDataBase DataBase;
    //I Locked the Fuck In While Writting These TS The Hardest One
    public void ConvertFromObjectToItem()
    {
        Item item = Instantiate(DataBase.FindItem(ItemName));
        item.icon = this.PI_icon;
        item.Name = this.ItemName;
        item.NumbersOfItem = this.Stack;
        InventoryController.Instance.TempSetItem(item,item.NumbersOfItem,InventoryController.Instance.transform);
        Destroy(item.gameObject);
        Destroy(this.gameObject);
        //CODE

    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = PI_icon;
        this.gameObject.name = ItemName;
    }
}
