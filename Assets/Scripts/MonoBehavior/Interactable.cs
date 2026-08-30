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
    public Tool toolreq;


    public enum ObjectType
    {
        Harvestable,
        Convertable
    };
    public ObjectType Type;

    public bool CanInteractWith()
    {
        return HotBarController.Instance.CurrentItemHeld != null && toolreq != null &&
            HotBarController.Instance.HoldingThis(toolreq.GetComponent<Item>());
    }

}
public class Convertable : MonoBehaviour
{

}
