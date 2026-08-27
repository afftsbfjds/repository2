using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Output")]
    public Item Output1;
    public  Item Output2;
    public Item OutputRandom;


    [Space]
    [Space]
    [Space]


    [Header("Numbers Of Item Giving From Output")]
    public int amount1;
    public int amount2;
    public int min;
    public int max;

    [Space]
    [Space]
    [Space]
    public HarvestTool toolreq;
    [Space]
    [Space]
    [Space]
    [Header("Object Type Could either be  Harvestable, Convertable       ")]
    public string ObjectType;

    public bool CanInteractWith()
    {
        return HotBarController.Instance.CurrentItemHeld != null && toolreq != null &&
            HotBarController.Instance.HoldingThis(toolreq.GetComponent<Item>());
    }

}
public class Convertable : MonoBehaviour
{

}
