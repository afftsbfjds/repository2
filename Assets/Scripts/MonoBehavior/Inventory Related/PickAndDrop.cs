using Unity.VisualScripting;
using UnityEngine;

public class PickAndDrop : MonoBehaviour
{
    public static PickAndDrop Instance {get ; private set; }

    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private GameObject PhysicalPrefab;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return ;
        }
        Instance = this;
    }

    public void PickUpItem(PhysicalItem physicalItem, int amount)
    {
        if (physicalItem == null || physicalItem.Data == null ||
            InventoryController.Instance == null)
        {
            return;
        }
        ItemPopUpMenu.Instance.AddNewPopUp(physicalItem.Data);
        ///                 IF NOT FOUND ITEM OR ITEM HAS NOT WRITTEN DATA THEN END

        Item item = Instantiate(ItemPrefab).GetComponent<Item>();
        item.Data = physicalItem.Data;
        item.NumbersOfItem = amount;
        item.visualUpdate();
        if (InventoryController.Instance.SetItem(item,item.NumbersOfItem))
        {
            Destroy(physicalItem.gameObject);
        }
        else
        {
            Destroy(item.gameObject);
        }
    }

    public void DropItem(Item item,int amount,Vector3 pos)
    {
        GameObject player = GameObject.Find("Player");
        if (player == null || item == null || PhysicalPrefab == null)
        {
            return;
        }

        if(item.Data==null || PhysicalPrefab == null || amount <= 0)
        {
            Debug.LogWarning("THIS ITEM'S DATA HAS NOT BEEN SET!");
            return;
        }
        GameObject RealItem = Instantiate(PhysicalPrefab);
        RealItem.transform.position = pos;
        PhysicalItem PhysItem = RealItem.GetComponent<PhysicalItem>();
        PhysItem.Data = item.Data;
        PhysItem.NumbersOfItem = amount;
        PhysItem.visualUpdate();
        if (item.gameObject != null && item.gameObject.scene.IsValid())
        {
            Destroy(item.gameObject);
        }
    }

}
