using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform ParentsSlot;
    private CanvasGroup canvasGroup;
    private Slot AfterProceedSlot;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        ParentsSlot=transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
       transform.position = eventData.position;//+new Vector2(20f,20f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Working");
        AfterProceedSlot = eventData.pointerEnter?.GetComponent<Slot>();
        Debug.Log("Pointing to " +eventData.pointerEnter?.ToString());
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Slot originalslot = ParentsSlot.GetComponent<Slot>();       //set at the start of the dragging
        //Debug.Log(AfterProceedSlot?.ToString());
        if (AfterProceedSlot == null)
        {
            AfterProceedSlot = eventData.pointerEnter?.GetComponentInParent<Slot>();
        }
        if(AfterProceedSlot==null)
        {
            //Debug.LogWarning("NULL");
            transform.SetParent(ParentsSlot); 
            originalslot.currentitem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        else{
            //Debug.Log("There is a slot");
            if (AfterProceedSlot.currentitem != null)
            {
                //Debug.Log("There is an item");
                originalslot.currentitem = AfterProceedSlot.currentitem;
                //AfterProceedSlot.transform.GetChild(0).transform.SetParent(originalslot.transform);
                AfterProceedSlot.currentitem.transform.SetParent(originalslot.transform);
                originalslot.currentitem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                originalslot.currentitem = null;
            }

            //AfterProceedSlot.currentitem = originalslot.currentitem;
            AfterProceedSlot.currentitem = this.gameObject.GetComponent<Item>();
            //originalslot.currentitem.transform.SetParent(AfterProceedSlot.transform);
            this.gameObject.transform.SetParent(AfterProceedSlot.transform);
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            AfterProceedSlot.currentitem.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        
    }//end of func


}//end of class
