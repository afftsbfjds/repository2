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
        this.GetComponent<Image>().sprite = Data.icon;
        this.name = Data.Name;
    }
}
