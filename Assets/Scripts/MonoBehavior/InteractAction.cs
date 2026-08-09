using UnityEngine;

public class InteractAction : MonoBehaviour
{
    public InventoryController inventoryController;
    //public GameObject OUTPUT;
    public void ChoppingTree(GameObject Output,GameObject Tree)
    {

        inventoryController.SetItem(Output,Random.Range(5,10));
        Destroy(Tree);

    }
}
