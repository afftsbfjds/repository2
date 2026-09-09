using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [Header("Movement and Animations")]         //header #1
    [SerializeField] private float speed;
    public Animator animator;
    public Rigidbody2D player;
    private Vector2 MoveInput;
    public Vector2 LastDirection;
    private bool CANMOVE = true;
    [SerializeField]private bool toolUseActive;

    [Space]
    [Space]
    [Space]
    

    [Header("Interactions")]
    [SerializeField] private float CoolDown;
    [SerializeField] private float CDTimer = 0f;
    private bool CanInteract = true;
    [SerializeField] private LayerMask InteractableTileLayer;
    private Object priority;
    private Vector3 ObjectPos;
    [SerializeField] float InteractRange;

    [Space]
    [Space]
    [Space]


    [Header("Item Management, pickup variables")]       //header #3
    [SerializeField] LayerMask ItemPickupLayer;
    [SerializeField] LayerMask InteractLayer;



    [Space]
    [Space]
    [Space]

    [Header("Misc")]                                    //header#4
    private GameObject itemnearby;
    private Object GetPriority(Vector3 pos)
    {
        if(GetDistance(pos,transform.position)>InteractRange)
            return null;
        if(TileMap.Instance==null||TileMap.Instance.Map==null)
            return null;
        Collider2D hit = Physics2D.OverlapCircle(pos, 0.2f, InteractLayer);
        if (hit != null)
        {
            var interactable = hit.GetComponent<Interactable>();
            if (interactable != null)
                return interactable.gameObject;   // return actual GameObject
            return null;   /// THIS IS INTERACTABLE AREA
        }

            
        Vector3Int TileLoc = TileMap.Instance.Map.WorldToCell(pos);
        TileBase  TargetTile = TileMap.Instance.Map.GetTile(TileLoc);
        if(TargetTile !=null)  ///      RETURN PRIORITY 2:TILE
            return TargetTile;
        return null;
    }
    private void BindHeldToolAnimator()
    {
        if (HotBarController.Instance == null || HotBarController.Instance.CurrentItemHeld == null)
        {
            Debug.LogWarning("Player.BindHeldToolAnimator: HotBarController or CurrentItemHeld is null.");
            return;
        }

        Item heldItem = HotBarController.Instance.CurrentItemHeld;
        if (heldItem == null || heldItem.Data == null || heldItem.Data.config is not ToolConfig toolConfig)
        {
            Debug.LogWarning("Player.BindHeldToolAnimator: Held item, item data, or tool config is null.");
            return;
        }

        toolConfig.BindAnimator(animator);
    }

    public void Interact()
    {
        ///     SET CD
        if(!CanInteract){
            Debug.LogWarning("CANNOT INTERACT!");
            return;
        }
        ///     SET CD
        if (TileMap.Instance == null || TileMap.Instance.Map == null)
        {
            Debug.LogWarning("MAP NOT LOADED!");
            return;
        }

        ///     CHECK FOR OBJECT AT MOUSE
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);     mousepos.z = 0;
        Vector3Int TargetTileloc = TileMap.Instance.Map.WorldToCell(mousepos);     TargetTileloc.z = 0;
        TargetTileloc.z = 0;
        if(GetDistance(mousepos,transform.position)>InteractRange)
            return;
        ///     CHECK FOR OBJECT AT MOUSE

        ///         TARGET
            priority = GetPriority(mousepos);
        ///         TARGET

        if(priority == null)
        {
            Debug.LogWarning("Player.Interact: NO TARGET!");
            return; ///NO OBJECT OR TILE HERE
        }

        

        CanInteract=false;
        CDTimer = CoolDown;
        Debug.Log("INTERACT NOW");
        if(priority is GameObject gameobject)              /// TARGETTING OBJECT, OBJECT BRANCH
        {
            Interactable Object = gameobject.GetComponent<Interactable>();
            if(Object == null || Object.enabled ==false)
                return;
            if(!Object.CanInteractWith())
                return;
            ObjectPos = Object.transform.position;

            if (Object.ToolRequired == null || Object.ToolRequired.Length == 0)
            {
                Object.Interact();
                return;
            }

            // If a tool is needed, use the held item's behavior.
            var heldItem = HotBarController.Instance?.CurrentItemHeld;
            if (heldItem != null && heldItem.Data != null && heldItem.Data.behavior != null)
            {
                BindHeldToolAnimator();
                player.linearVelocity = Vector2.zero;
                LastDirection = mousepos-transform.position;
                animator.SetBool("UseTool", false);
                toolUseActive = true;
                heldItem.Data.behavior.Use(gameobject, TileMap.Instance.Map.WorldToCell(mousepos));
                CANMOVE = !animator.GetBool("UseTool");
                
                return ;
            }
        }


        else if(priority is TileBase tile)             ///     TARGETTING TILE , TILEBASE BRANCH
        {
            var heldItem = HotBarController.Instance?.CurrentItemHeld;
            if (heldItem == null || heldItem.Data == null || heldItem.Data.behavior == null)
                return;
            bool hasCollision = TileMap.Instance.Map.GetColliderType(TargetTileloc) != Tile.ColliderType.None;
            if(!hasCollision){
                Debug.LogWarning("TileChangeToolBehavior.Use: Tile Is Not Interactable.");
                return;
            }
            ObjectPos = TargetTileloc;
            BindHeldToolAnimator();
            player.linearVelocity = Vector2.zero;
            LastDirection = mousepos-transform.position;
            animator.SetBool("UseTool", false);
            toolUseActive = true;
            heldItem.Data.behavior.Use(null,TargetTileloc);
            CANMOVE = !animator.GetBool("UseTool");
            return;
        }



    }       ///END OF INTERACT

    public void StopTool()
    {
        if (!toolUseActive)
            return;

        toolUseActive = false;
        animator.SetBool("UseTool", false);


        ItemData item = HotBarController.Instance?.Holding();
        if (item.owner !=null &item != null && item.behavior is ToolBehavior toolBehavior)
            toolBehavior.FinishUsingTool(priority,ObjectPos);

        CANMOVE = true;
    }




    /// <Function Used> //////////////////////////////////////////////////////////////////////////////////////////////
    private Vector2 Normalize(float a,float b)
    {
        float c = Mathf.Sqrt(a*a + b*b);
        return new Vector2(a/c,b/c);
    }
    public float GetDistance(Vector2 a,Vector2 b)
    {
        float distance = Mathf.Abs(Mathf.Sqrt((a.x-b.x)*(a.x-b.x)+(a.y-b.y)*(a.y-b.y)));
        return distance;
    }
    public void Move(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
    
    /// <Function Used> //////////////////////////////////////////////////////////////////////////////////////////////
    // Update is called once per frame
    void Update()
    {
        if (MoveInput != Vector2.zero && CANMOVE)
        {
            LastDirection = MoveInput;
        }

        if(CANMOVE)
            player.linearVelocity = MoveInput * speed;
        else
            player.linearVelocity = Vector2.zero;

        animator.SetBool("IsMoving", CANMOVE && MoveInput != Vector2.zero);
        animator.SetFloat("Horizontal", LastDirection.x);
        animator.SetFloat("Vertical", LastDirection.y);
        
        
        
        if(!CanInteract){
            CDTimer-=Time.deltaTime;
            if(CDTimer<=0)
            {
                CDTimer=0;
                CanInteract=true;
            }
        }




    }//end of Update

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Item"))
        {
            itemnearby = collider.gameObject;
            if(PickAndDrop.Instance == null
            || itemnearby.GetComponent<PhysicalItem>() == null
            || itemnearby.GetComponent<PhysicalItem>().Data == null)
            {
                Debug.LogWarning("Player.OnTriggerEnter2D: PickUp check failed because one of the required references is null.");
                return ;
            }
            PhysicalItem physicalItem = itemnearby.GetComponent<PhysicalItem>();
            PickAndDrop.Instance.PickUpItem(physicalItem, physicalItem.NumbersOfItem);
        }

        if (!CanInteract)
        {
            CDTimer-=Time.deltaTime;
            if(CDTimer<=0)
                CanInteract=true;
        }

    }

}//end of Class
