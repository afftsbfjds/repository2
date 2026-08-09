using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject Slot;
    [SerializeField] private GameObject parentsMenu;
    [SerializeField] private int Inventorysize;
    public ItemDex m_ItemDex;//starteritem
    [SerializeField] private GameObject PauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < Inventorysize; i++)
        {
            GameObject slot = Instantiate(Slot,parentsMenu.transform);//create x numbers of slots(include image,name,..)as children of inventory

        }
        //SetItem(Slot,Inventorysize);

        SetItem(m_ItemDex.itemdex[0],10);
        SetItem(m_ItemDex.itemdex[1],5);
        PauseMenu.gameObject.SetActive(false);
    }//end of func


    public void SetItem(GameObject Items,int amount)
    {
        foreach (Transform slots1 in parentsMenu.transform)
        {
            Slot slot = slots1.GetComponent<Slot>();

            if (slot.currentitem != null &&
            slot.currentitem.icon == Items.GetComponent<Item>().icon)
            {
                slot.currentitem.NumbersOfItem += amount;
                Destroy(Items);
                return;
            }
        }
        foreach(Transform slots in parentsMenu.transform)
        {
            //Debug.Log("GameObject: "+slots.ToString());//loop through all slots in inventory
            if(slots.GetComponent<Slot>().currentitem == null && Items!= null)
            {
                //Debug.Log("This slot can be set to an item");
                Item newItem = Instantiate(Items,slots.transform).GetComponent<Item>();
                newItem.transform.SetParent(slots);
                newItem.name = Items.name;
                newItem.NumbersOfItem = amount;
                slots.GetComponent<Slot>().currentitem = newItem;
                newItem.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                newItem.gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f,0.5f);
                break;
            }
        }
    }



}//end of class
