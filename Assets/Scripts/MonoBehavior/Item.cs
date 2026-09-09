using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class Item : MonoBehaviour
{
    public int NumbersOfItem=0;
    public ItemData Data;



    private void Start()
    {
        if (Data == null)
        {
            Debug.LogError("Item has no ItemData", this);
            Destroy(gameObject);
            return;
        }

        if (Data.config == null)
        {
            Debug.LogError("Item config is missing", this);
            Destroy(gameObject);
            return;
        }

        if (Data.behavior == null)
        {
            Debug.LogError("Item behavior is missing", this);
            Destroy(gameObject);
            return;
        }


        
        Data.owner = this;
        Data.behavior.BehaviorOwner = this;
    }



    public void visualUpdate()
    {
        if(Data == null || NumbersOfItem <= 0)
            Destroy(gameObject);
        this.GetComponent<Image>().sprite = Data.icon;
        this.name = Data.Name;
    }

    public void ClickItem(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        if (pointerEventData != null &&
            pointerEventData.button == PointerEventData.InputButton.Right &&
            PickAndDrop.Instance != null)
        {
            PickAndDrop.Instance.DropItem(this,NumbersOfItem,GameObject.Find("Player").transform.position);
        }
    }

}



//this is just testing if the repository work