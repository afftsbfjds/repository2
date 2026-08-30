using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Item : MonoBehaviour
{
    [SerializeField] private GameObject PrefabPhysicalObject;
    [SerializeField] private TextMeshProUGUI TextPrefab;
    public int NumbersOfItem=0;
    public ItemData Data;
    public void Interact()
    {
        switch (Data.type)
        {
            case ItemData.ITEMTYPE.Tool:
                break ;
            case ItemData.ITEMTYPE.Material:
                return ;
            case ItemData.ITEMTYPE.Consumable:
                break ;
            case ItemData.ITEMTYPE.Seed:
                break ;
        }
    }

    public void visualUpdate()
    {
        this.GetComponent<Image>().sprite = Data.icon;
        this.name = Data.Name;
    }

}



//this is just testing if the repository work