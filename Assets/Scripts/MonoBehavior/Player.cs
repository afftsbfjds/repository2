using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement and Animations")]         //header #1
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    public Rigidbody2D player;
    private Vector2 MoveInput;
    public Vector2 LastDirection;


    [Space]
    [Space]
    [Space]


    [Header("Object-Interacting variables")]        //header #2
    private float DistToClstObj;
    private GameObject ClstObject;
    private Interactable PendingHarvest;
    [SerializeField] public float InteractRange;

    public void StopUsingTool()
    {
        animator.SetBool("UseTool",false);
        if (PendingHarvest != null)
        {
            PendingHarvest.toolreq.Use_Harvest_Tool_On(PendingHarvest);
            PendingHarvest = null;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed || ClstObject == null || animator.GetBool("UseTool"))
            return;

        Interactable interactable = ClstObject.GetComponent<Interactable>();
        if (interactable == null || !interactable.CanInteractWith())
            return;


        switch (interactable.ObjectType)//check interact type
        {
            case "Harvestable":// if tryna harvest
                interactable.toolreq.StartUseTool();       //set the animation in override controller to prep for override
                animator.runtimeAnimatorController = interactable.toolreq.overrideController;//override interact animation for each tools
                PendingHarvest = interactable;
                animator.SetBool("UseTool",true);   //start playing the animation after overrided
                break;//break the inner case
            
        }
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
    private Collider2D TouchItem()
    {
        return Physics2D.OverlapCircle(transform.position,InteractRange,ItemPickupLayer);
    }
    private Collider2D InteractWithObject()
    {
        return Physics2D.OverlapCircle(transform.position,2f,InteractLayer);
    }
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
    void Start()
    {
        DistToClstObj = 99999;
        InteractRange=1.5f;
    }
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
        
        
        
        if(InteractWithObject()!=null)
        {
            ClstObject = InteractWithObject().gameObject;
            DistToClstObj = GetDistance(ClstObject.transform.position,transform.position);
        }
        else
        {
            DistToClstObj= 99999;
            ClstObject = null;
        }
        //Debug.Log(ClstObject?.ToString());
        //Debug.Log(DistToClstObj);


        ////////////////////////////////////
        ///                              ///
        ///     check if Interactable    ///
        ///                              ///
        ////////////////////////////////////
        
        
        
        ///////////////////////////////
        // Interacting with objects  //
        ///////////////////////////////
        
        
        //this is the part where I make pickup item func

        itemnearby = TouchItem()?.gameObject;
        if (itemnearby != null)
        {
            itemnearby.GetComponent<PhysicalItem>().ConvertFromObjectToItem();
        }
        
    }//end of Update

}//end of Class
