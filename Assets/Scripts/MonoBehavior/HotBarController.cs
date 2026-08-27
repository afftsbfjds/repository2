using UnityEngine;
using UnityEngine.UI;
public class HotBarController : MonoBehaviour
{
        public GameObject Slot;
        public GameObject parentsMenu;
        public int Hotbarsize;
        [SerializeField] private ItemDataBase DataBase;
        private Transform currentSlot;  //reference to the item player holds
        public Item CurrentItemHeld;
        public static HotBarController Instance { get; set;}

        private int KeyboardOutput=1;
    private void KeyboardNumberOutput()
    {
        
        // Check number keys 1-9 

            for (int i = 1; i <= Hotbarsize; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
                {
                    KeyboardOutput = i;
                    break;
                }
            }
        
    }

    public bool ThisItemInHotbar(string ItemName)
    {
        foreach (Transform Slot in parentsMenu.transform)
        {
            Slot SLOT = Slot.GetComponent<Slot>();
            if (SLOT.currentitem != null && SLOT.currentitem.Name == ItemName)
            {
                return true;
            }
        }
        return false;
    }
    public bool HoldingThis(Item ItemHeld)
    {
        return ItemHeld != null && CurrentItemHeld != null &&
            CurrentItemHeld.Name == ItemHeld.Name;
    }
    void Awake()
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
        for (int i = 0; i < Hotbarsize; i++)
        {
            GameObject slot = Instantiate(Slot,parentsMenu.transform);//create x numbers of slots(include image,name,..)as children of Hotbar   
        }
        //create slots for hotbar


        //create starter Item
        if (InventoryController.Instance != null)
        {
            InventoryController.Instance.TempSetItem(DataBase.FindItem("Axe"), 1, parentsMenu.transform);
        }

    }

    void Update()
    {
        for(int i = 0; i < 9; i++)
        {
            this.gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.gray;
        }
        KeyboardNumberOutput();

        //                      visual changes to current slots
        currentSlot = this.gameObject.transform.GetChild(KeyboardOutput-1);
        currentSlot.GetComponent<Image>().color = Color.white;
        //                      visual changes to current slots



        //                      detect item holding
        CurrentItemHeld = currentSlot.GetComponent<Slot>().currentitem;



        //Is Holding Tool?
            
    }
    

}
