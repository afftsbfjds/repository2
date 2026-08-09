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
    
    public int NumbersOfItem=0;


    public void ClickItem(BaseEventData data)
    {

        PointerEventData pointerData = (PointerEventData)data;

        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            DropItem();
        }
    }

    
    private void DropItem()
    {
        GameObject PhysicalItem = Instantiate(PrefabPhysicalObject,GameObject.Find("Player").transform.position + new Vector3(2f,2f,0),GameObject.Find("Player").transform.rotation);
        PhysicalItem.GetComponent<PhysicalItem>().PI_icon = icon;
        PhysicalItem.GetComponent<PhysicalItem>().ItemName = Name;
        PhysicalItem.GetComponent<PhysicalItem>().Stack = NumbersOfItem;
        Destroy(this.gameObject);
    }
    private void Start()
    {
        Text = transform.GetChild(0).gameObject;
        this.gameObject.GetComponent<Image>().sprite = icon;
        this.gameObject.name = Name;
        
    }
    private void Update()
    {
        Text.GetComponent<TextMeshProUGUI>().text = NumbersOfItem.ToString();
        if (NumbersOfItem <= 0 || Name == null || icon==null)
        {
            Destroy(this.gameObject);
        }
    }

}



//this is just testing if the repository work