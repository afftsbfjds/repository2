using UnityEngine;
using UnityEngine.UI;
public class PhysicalItem : MonoBehaviour
{
     
    public Sprite PI_icon;
    public int Stack;
    public string ItemName;
    [SerializeField] private GameObject ItemPrefab;
    //I Locked the Fuck In While Writting These TS The Hardest One
    public void ConvertFromObjectToItem(GameObject inventoryController)
    {

        GameObject TempItem = Instantiate(ItemPrefab,transform.position,transform.rotation);
        TempItem.GetComponent<Item>().icon = PI_icon;
        TempItem.GetComponent<Item>().Name = ItemName;
        TempItem.GetComponent<Item>().NumbersOfItem = Stack;
        //these three lines set the variables of Item with the value of PhysicalItem
        inventoryController.GetComponent<InventoryController>().SetItem(TempItem,TempItem.GetComponent<Item>().NumbersOfItem);
        Destroy(TempItem.gameObject);
        Debug.Log("WORKING!");
        Destroy(this.gameObject);

    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = PI_icon;
        this.gameObject.name = ItemName;
    }
}
