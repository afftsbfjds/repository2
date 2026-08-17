using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject Slot;
    [SerializeField] private GameObject parentsMenu;
    [SerializeField] private int Inventorysize;
    public Item PrefabItem;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private ItemDataBase DataBase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static InventoryController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return ;
        }
        Instance = this;
    }
    void Start()
    {
        for (int i = 0; i < Inventorysize; i++)
        {
            GameObject slot = Instantiate(Slot,parentsMenu.transform);//create x numbers of slots(include image,name,..)as children of inventory

        }
        //SetItem(Slot,Inventorysize);

        
        PauseMenu.gameObject.SetActive(false);
        TempSetItem(DataBase.FindItem("Oak Log"),5,parentsMenu.transform);

    }//end of func

    //this is an WIP version of SetItem function so Keep working till it's done

    public void TempSetItem(Item PrefabItem,int amount, Transform TargetParent)
    {
        if(parentsMenu == null)
            return;

        if (PrefabItem == null)   //If The Item I Want To Set Doesn't Exist, Do nothing
        {
            Debug.LogWarning($"Could not find item '{PrefabItem}' in the database.");
            return;
        }
        Item TempItem = Instantiate(PrefabItem);
        foreach (Transform slotTransform in TargetParent)  //Loop Through All Slots
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot == null)   //If The Slot Get Bugged And Doesn't Generate, Skip
                continue;

            if (slot.currentitem != null)       //IF The Slot Contains Item, Skip / + Amount
            {
                if (slot.currentitem.Name == TempItem.Name)       //if Item Is The Same, + Amount
                {
                    slot.currentitem.NumbersOfItem += amount;
                    Destroy(TempItem);
                    return;
                }
                continue;
            }

            TempItem.transform.SetParent(slotTransform);
            slot.currentitem = TempItem;
            TempItem.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            TempItem.gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            return;
        }
    }    //end of func

}//end of class
