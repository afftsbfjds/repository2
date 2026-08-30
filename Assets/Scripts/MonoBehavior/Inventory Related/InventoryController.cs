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

        }   ///                         SET ITEM                        ///

        
        PauseMenu.gameObject.SetActive(false);

    }

    public bool SetItem(Item item)
    {
        if (item == null || item.Data == null)
        {
            return false;
        }

        ///                                 CHECK FOR DUPLICATE                              ///
        foreach(Transform Slots in parentsMenu.transform)
        {
            Slot slot = Slots.GetComponent<Slot>();
            if (slot == null || slot.currentitem == null || slot.currentitem.Data == null ||
                slot.currentitem.Data.Name != item.Data.Name)
            {
                continue;
            }
            if(slot.currentitem.NumbersOfItem+item.NumbersOfItem > slot.currentitem.Data.maxStack)
                break;
            slot.currentitem.NumbersOfItem += item.NumbersOfItem;
            Destroy(item.gameObject);
            slot.currentitem.visualUpdate();
            return true;
        }

        ///                                 CHECK FOR DUPLICATE                              ///
        /// 

        ///                                     Set Item                                     ///
        
        
        foreach(Transform Slots1 in parentsMenu.transform)
        {
            Slot slot1 = Slots1.GetComponent<Slot>();
            if(slot1 == null || slot1.currentitem != null){
                continue;
            }
            item.gameObject.transform.SetParent(Slots1);
            slot1.currentitem = item;
            item.visualUpdate();
            return true;
        }









        return false;
        
    }








    

}//end of class
