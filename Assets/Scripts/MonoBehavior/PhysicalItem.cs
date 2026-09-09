using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PhysicalItem : MonoBehaviour
{
     
    public ItemData Data;
    [SerializeField] private GameObject ItemPrefab;
    public int NumbersOfItem=0;
    public void visualUpdate()
    {
        if(Data == null || NumbersOfItem <= 0 || GetComponent<SpriteRenderer>() == null || Data.name == null)
        {
            Destroy(gameObject);
            return;
        }
        if (Data?.icon == null)
        {
            Destroy(gameObject);
            return;
        }
        GetComponent<SpriteRenderer>().sprite = Data.icon;
        this.name = Data.Name;
    }
    private void Start()
    {
        visualUpdate();
    }
}
