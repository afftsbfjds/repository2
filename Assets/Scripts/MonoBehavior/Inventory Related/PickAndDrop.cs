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
        ///                 IF NOT FOUND ITEM OR ITEM HAS NOT WRITTEN DATA THEN END

        Item item = Instantiate(ItemPrefab).GetComponent<Item>();
        item.Data = Instantiate(physicalItem.Data);
        item.NumbersOfItem = amount;
        item.visualUpdate();
        if (InventoryController.Instance.SetItem(item))
        {
            Destroy(physicalItem.gameObject);
        }
        else
        {
            Destroy(item.gameObject);
        }
    }

    public void DropItem(ItemData Data)
    {
        PhysicalItem PhysItem = Instantiate(PhysicalPrefab).GetComponent<PhysicalItem>();
        PhysItem.Data = Instantiate(Data);
        PhysItem.visualUpdate();
    }

}
