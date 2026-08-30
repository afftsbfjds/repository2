using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Tool : MonoBehaviour
{

    public ToolInteract ToolInteraction;
    public ToolData1 Data;
    [SerializeField] private Tile OutputTile;

    private void StartAnim(Animator animator1)
    {
        Data.StartUseTool();
        animator1.runtimeAnimatorController = Data.overrideController;       //prepare animations
        animator1.SetBool("UseTool",true);
    }
    public void Interact(Animator animator,Object priority)
    {
        if(priority == null)
            return;
        
        //Debug.Log("Interacting!");
        
        switch (Data.Tooltype)
        {
            case ToolData1.ToolType.Harvest:
                    if(!(priority is Interactable))
                        return;

///////////////////////////////////////////////         Trying To Interact with Objects         //////////////////////////////////
                Interactable Object = (Interactable)priority;
                StartAnim(animator);
                if(Object.Type != Interactable.ObjectType.Harvestable)  //IF OBJECT NOT HARVESTABLE THEN END
                    return  ;    


                if(GetComponent<Item>().Data.Name != Object.toolreq.GetComponent<Item>().Data.Name)   //IF NOT MATCHING TOOL REQUIRED THEN END
                    break   ;


                    ToolInteraction.Harvest(Object);
                break;



///////////////////////////////////////////////         TILE CHANGING TOOL      //////////////////////////////////////
            case ToolData1.ToolType.TileChanging:
                if(!(priority is TileBase))
                    return  ;
                StartAnim(animator);
                ToolInteraction.ChangeTile(OutputTile);

                break;
        }
    }
}
