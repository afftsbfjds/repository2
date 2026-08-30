using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [Header("Movement and Animations")]         //header #1
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private float InteractionRadius = 0.25f;
    [SerializeField] private float tileObstacleRadius = 0.45f;
    public Rigidbody2D player;
    private Vector2 MoveInput;
    public Vector2 LastDirection;

    [Space]
    [Space]
    [Space]
    

    [Header("Interactions")]
    [SerializeField] private float CoolDown;
    [SerializeField] private float CDTimer = 0f;
    private bool CanInteract = true;
    [SerializeField] private LayerMask InteractableTileLayer;
    public void StopUsingTool()
    {
        animator.SetBool("UseTool",false);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.started || !CanInteract)
            return;
        CanInteract = false;
        CDTimer = CoolDown;

        if (Camera.main == null || TileMap.Instance == null || TileMap.Instance.Map == null)
            return;
        /////////////////////////       START FUNCTION FROM HERE            ///////////////////////////////////////

        /////////////////////////       GET MOUSE POS       //////////////////////////////////
        Vector3 mousepos = Input.mousePosition;
        /////////////////////////       GET MOUSE POS       //////////////////////////////////
        //GET TILE AT MOUSE POS
        Vector3Int targetTilePos = TileMap.Instance.Map.WorldToCell(Camera.main.ScreenToWorldPoint(mousepos));                       //detect interactable tiles
        targetTilePos.z = 0;
        if(GetDistance(new Vector2(targetTilePos.x,targetTilePos.y),transform.position)>InteractionRadius)
            return ;

        TileBase TargetTile= null;
        //THIS IS THE VARIABLE OF THE INTERACTABLE TILE

        Vector3 targetTileWorldPosition = TileMap.Instance.Map.GetCellCenterWorld(targetTilePos);
        //GET THE CURRENT TILE

        /////////////////////////////////       CHECK IF TILE IS INTERACTABLE               ///////////////////////////
        if (TileMap.Instance.Map.GetTile(targetTilePos) != null)
        {
            Collider2D TileCollider = Physics2D.OverlapPoint(targetTileWorldPosition,InteractableTileLayer);
            if(TileCollider!=null)
            {
                Tilemap Map = TileCollider.GetComponent<Tilemap>();
                TargetTile = Map.GetTile(TileMap.Instance.Map.WorldToCell(targetTileWorldPosition));   
                Debug.Log(TargetTile); 
            }
        }
        
        /////////////////////////////////       CHECK IF TILE IS INTERACTABLE               /////////////////////////


        /////////////////////////////////       GET OBJECT AT MOUSE     ////////////////////////////////////
        Interactable interactable = Physics2D.OverlapCircle(targetTileWorldPosition,tileObstacleRadius,InteractLayer)?.GetComponentInParent<Interactable>();           
        /////////////////////////////////       GET OBJECT AT MOUSE     ////////////////////////////////////



        if(TargetTile == null && interactable == null)      //NO TARGET THEN END
            return;


        if (HotBarController.Instance == null || HotBarController.Instance.CurrentItemHeld == null)
            return ;            //NOT HOLDING ITEM THEN END


        Tool toolused = HotBarController.Instance.CurrentItemHeld.GetComponent<Tool>();
        if (toolused==null)
            return ;                 //NOT HOLDING TOOL THEN END


        Object priorityTarget = null;
        

        if(interactable!=null)      //IF THERES INTERACTABLE OBJECT
        {
            priorityTarget = interactable;
        }
        else                        //IF THERES NO INTERACTABLE OBJECT THEN INTERACT WITH TILES
            priorityTarget = TargetTile;


        


        if (priorityTarget != null)
            toolused.Interact(animator, priorityTarget);
    }

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
        if (MoveInput != Vector2.zero)
        {
            LastDirection = MoveInput;
        }

        player.linearVelocity = MoveInput * speed;

        animator.SetBool("IsMoving", MoveInput != Vector2.zero);
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
            //itemnearby.GetComponent<PhysicalItem>().ConvertFromObjectToItem();
        }
    }

}//end of Class
