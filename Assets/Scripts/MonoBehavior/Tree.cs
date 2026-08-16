using UnityEngine;

public class Destructable : MonoBehaviour
{
    [Header("Output")]
    [SerializeField] private Item Output1;
    [SerializeField] private  Item Output2;
    [SerializeField] private Item OutputRandom;


    [Space]
    [Space]
    [Space]


    [Header("Numbers Of Item Giving From Output")]
    [SerializeField] private int amount1;
    [SerializeField] private int amount2;
    [SerializeField] private int min;
    [SerializeField] private int max;

    [Space]
    [Space]
    [Space]


    [Header("Inventory , Hotbar , DataBase")]
    [SerializeField] private InventoryController Inventory;
    [SerializeField] private HotBarController Hotbar;
    [SerializeField] private ItemDataBase DataBase;

    void Start()
    {
        Inventory = InventoryController.Instance;
    }
    public void DestroyObject()
    {
        
        OutputRandom.DropItem(Random.Range(min,max));
        Output2.DropItem(amount2);
        Destroy(this.gameObject);
    }

}
