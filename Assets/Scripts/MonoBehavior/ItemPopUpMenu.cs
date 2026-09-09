using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopUpMenu : MonoBehaviour
{
    public static ItemPopUpMenu Instance {get; set;}
    private Queue<GameObject> POPUP;
    private int MaxPopup = 4;
    [SerializeField] private GameObject PopUpPrefab;
    [SerializeField] private float fadetime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return ;
        }
        Instance = this;
        POPUP = new Queue<GameObject>();
    }
    private void Start()
    {
        if(PopUpPrefab==null)
            return;
        
    }

    public void AddNewPopUp(ItemData data)
    {
        GameObject popup = Instantiate(PopUpPrefab,transform);
        Image image=popup.transform.Find("ItemImage").GetComponent<Image>();
        if(!image)
            return;
        TextMeshProUGUI Text =popup.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        if(!Text)
            return;
        Debug.Log("text and image exist");
        image.sprite = data.icon;
        Text.text = data.Name;
        popup.transform.SetParent(transform);
        POPUP.Enqueue(popup);
        if (POPUP.Count > MaxPopup)
        {
            Destroy(POPUP.Dequeue());
        }
        StartCoroutine(Fade(popup));
    }

    private IEnumerator Fade(GameObject _popup)
    {
        yield return new WaitForSeconds(2f);
        if(_popup == null)
            yield break;

        for(float i=1;i>=0;i-=Time.deltaTime)
        {
            if(_popup==null)
                yield break;
            _popup.GetComponent<CanvasGroup>().alpha = i;
            yield return null;
        }

        Destroy(_popup);
    }
}
