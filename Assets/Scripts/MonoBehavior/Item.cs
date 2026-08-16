using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Item : MonoBehaviour
{
    public Sprite icon;
    public string Name;
    [SerializeField] private GameObject PrefabPhysicalObject;
    private GameObject Text;
    public ItemDataBase DataBase;
    
    public int NumbersOfItem=0;


    public void ClickItem(BaseEventData data)
    {

        PointerEventData pointerData = (PointerEventData)data;

        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            DropItem(this.NumbersOfItem);
            Destroy(this.gameObject);
        }
    }
    public void DropItem(int amountDrop)
    {
        GameObject PhysicalItem = Instantiate(PrefabPhysicalObject,GameObject.Find("Player").transform.position + new Vector3(2,2,0),GameObject.Find("Player").transform.rotation);
        PhysicalItem.GetComponent<PhysicalItem>().PI_icon = icon;
        PhysicalItem.GetComponent<PhysicalItem>().ItemName = Name;
        PhysicalItem.GetComponent<PhysicalItem>().Stack = amountDrop;
    }
    public Item OverrideData(Item I_Item, int AMOUNT)      //this takes data from database in order to replace this current item data
    {
        if (I_Item == null)//check if The Item Wanted To Set Exist, If Not THen Return Nothing
            return this;

        icon = I_Item.icon;         //If it Exist, Override this Current Item with That Item Data
        Name = I_Item.Name;
        DataBase = I_Item.DataBase;
        NumbersOfItem = AMOUNT;
        RefreshVisual();            //RefreshVisual Makes The UI Update
        return this;
    }

    private void RefreshVisual()        //this will run after item Update to immediately change the UI, name,...
    {
        Text = transform.GetChild(0).gameObject;
        this.gameObject.GetComponent<Image>().sprite = icon;
        this.gameObject.name = Name;
    }
    private void Start()
    {

        RefreshVisual();
        if (NumbersOfItem <= 0 || Name == null || icon==null)
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        Text.GetComponent<TextMeshProUGUI>().text = NumbersOfItem.ToString();
    }

}



//this is just testing if the repository work