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
    [SerializeField] public float InteractRange;

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
    [SerializeField] private GameObject PL_InventoryController;

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
    private bool IsMoving()
    {
        if (player.linearVelocity!= Vector2.zero)
        {
            return true;
        }
        return false;
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
    

    /// 
    /// <Function Used> //////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        DistToClstObj = 99999;
        InteractRange=1.5f;
    }
    // Update is called once per frame
    void Update()
    {
        if ((player.linearVelocity.x !=0 && player.linearVelocity.y ==0) ||(player.linearVelocity.x ==0 && player.linearVelocity.y !=0))
        {
            LastDirection = player.linearVelocity;
        }
        ///////////////////////////////////////////////////////////
        animator.SetBool("IsMoving",IsMoving());                ///
        animator.SetFloat("Horizontal",LastDirection.x); ///         Animating
        animator.SetFloat("Vertical",LastDirection.y);   ///
        ///////////////////////////////////////////////////////////
        
        


        
        ////////////////////////////////////////////////////////////////////////////////////
        player.linearVelocity = MoveInput* new Vector2(speed,speed);                                              ////    movement
        ////////////////////////////////////////////////////////////////////////////////////
        
        
        
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
        if (ClstObject != null)
        {
            switch (ClstObject.tag)
            {
                case "Tree":
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        ClstObject.GetComponent<Destructable>().HarvestObject();
                        //Destroy(ClstObject);
                    }
                    break;
                case "Plant":
                    if (Input.GetKeyDown(KeyCode.Space) && ClstObject.GetComponent<Plant>().Harvestable)
                    {
                        ClstObject.GetComponent<Destructable>().HarvestObject();
                    }
                    break;
            }       

                
        }
        
        //this is the part where I make pickup item func

        itemnearby = TouchItem()?.gameObject;
        if (itemnearby != null)
        {
            itemnearby.GetComponent<PhysicalItem>().ConvertFromObjectToItem();
        }
        
    }//end of Update

}//end of Class
