using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PhysicalItem : MonoBehaviour
{
     
    public Sprite PI_icon;
    public int Stack;
    public string ItemName;
    [SerializeField] private GameObject ItemPrefab;
    //I Locked the Fuck In While Writting These TS The Hardest One
    public void ConvertFromObjectToItem()
    {
        Item item = Instantiate(ItemPrefab.GetComponent<Item>());
        item.icon = this.PI_icon;
        item.Name = this.ItemName;
        item.NumbersOfItem = this.Stack;
        InventoryController.Instance.TempSetItem(ItemPrefab.GetComponent<Item>(),item.Name,item.NumbersOfItem);
        Destroy(this.gameObject);
        //CODE

    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = PI_icon;
        this.gameObject.name = ItemName;
    }
}
