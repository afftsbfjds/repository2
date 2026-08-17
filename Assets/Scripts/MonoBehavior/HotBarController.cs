using UnityEngine;
using UnityEngine.UI;
public class HotBarController : MonoBehaviour
{
        public GameObject Slot;
        public GameObject parentsMenu;
        public int Hotbarsize;
        [SerializeField] private ItemDataBase DataBase;
        private Transform currentItemHolding;  //reference to the item player holds
        public static HotBarController Instance { get; set;}

        private int KeyboardOutput=1;
    private int KeyboardNumberOutput(int numberPressed)
    {
        int a = numberPressed;
        // Check number keys 1-9 and 0 (as 10)

            for (int i = 1; i <= Hotbarsize; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
                {
                    a = i;
                    break;
                }
            }
        
        return a;
    }

    public bool ThisItemExist(string ItemName)
    {
        foreach (Transform Slot in parentsMenu.transform)
        {
            Slot SLOT = Slot.GetComponent<Slot>();
            if (SLOT.currentitem.Name == ItemName)
            {
                return true;
            }
        }
        return false;
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
        KeyboardOutput = KeyboardNumberOutput(KeyboardOutput);
        currentItemHolding = this.gameObject.transform.GetChild(KeyboardOutput-1);
        currentItemHolding.GetComponent<Image>().color = Color.white;
    }
    

}
