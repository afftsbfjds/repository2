using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Item : MonoBehaviour
{
    public Sprite icon;
    public string Name;
    [SerializeField] private GameObject PrefabPhysicalObject;
    [SerializeField] private TextMeshProUGUI Text;
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

    private void Start()
    {
        Text = this.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (NumbersOfItem <= 0 || Name == null || icon==null)
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        Text.text = NumbersOfItem.ToString();
    }

}



//this is just testing if the repository work