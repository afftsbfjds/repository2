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


    [SerializeField] private ItemDataBase DataBase;
    [SerializeField] public HarvestTool toolreq;

    void Start()
    {
    }
    public void HarvestObject()
    {
        if(OutputRandom!=null)
            OutputRandom.DropItem(Random.Range(min,max));
        if(Output1!=null)
            Output1.DropItem(amount1);
        if(Output2!=null)
            Output2.DropItem(amount2);
        Destroy(this.gameObject);
    }

}
