using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject Slot;
    [SerializeField] private GameObject parentsMenu;
    [SerializeField] private GameObject Hotbar;
    [SerializeField] private int Inventorysize;
    public Item PrefabItem;
    [SerializeField] private GameObject PauseMenu;

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
    private void Start()
    {
        for (int i = 0; i < Inventorysize; i++)
        {
            GameObject slot = Instantiate(Slot,parentsMenu.transform);

        }

        
        PauseMenu.gameObject.SetActive(false);

    }
///                                     SET ITEM                /////////////////////////////////////////
    public bool SetItem(Item item,int amount)
    {
        if (item == null || item.Data == null)
        {
            return false;
        }

        ///                                 CHECK FOR DUPLICATE                              ///
        Item DupeItem = null;
        if (ItemInInven(item, parentsMenu) != null)
        {
            DupeItem = ItemInInven(item,parentsMenu);
        }
        if (ItemInInven(item, Hotbar) != null)
        {
            DupeItem = ItemInInven(item,Hotbar);
        }
        if(DupeItem != null && DupeItem.Data.isStackable && DupeItem.NumbersOfItem<=DupeItem.Data.maxStack)/// IF NO DUPE OR REACH MAXIMUM STACK, SKIP
        {
            Debug.Log("ITEM DUPE");
            DupeItem.NumbersOfItem+=amount;
            Destroy(item.gameObject);
            return true;
        }
        

        ///                                 CHECK FOR DUPLICATE                              ///


        ///                                     Set Item                                     ///
        
        
        foreach(Transform Slots1 in parentsMenu.transform)
        {
            Slot slot1 = Slots1.GetComponent<Slot>();
            if(slot1 == null || slot1.currentitem != null){
                continue;
            }
            item.gameObject.transform.SetParent(Slots1);
            item.transform.localPosition = new Vector2(0,0);
            slot1.currentitem = item;
            item.visualUpdate();
            return true;
        }









        return false;
        
    }





private Item ItemInInven(Item item,GameObject menu)
    {
        if(menu==null || item==false)
            return null;
        foreach(Transform invenslot in menu.transform)
        {
            if(invenslot == null)
                return null;
            Slot Slot1 = invenslot.GetComponent<Slot>();
            if(Slot1==null || Slot1.currentitem==null || Slot1.currentitem.Data ==null
             || Slot1.currentitem.Data.ID !=item.Data.ID || !Slot1.currentitem.Data.isStackable)
            {
                ///CANNOT STACK OR NO DUPLICATE
                continue;
            }
            ///CAN STACK OR DUPLICATE
            return Slot1.currentitem;
        }
        return null;
    }


    

}//end of class
