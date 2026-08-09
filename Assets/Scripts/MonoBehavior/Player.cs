using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement and Animations")]         //header #1
    [SerializeField] private float speed;
    public Animator animator;
    [SerializeField] private Rigidbody2D player;
    private Vector2 LastDirection;


    [Header("Object-Interacting variables")]        //header #2
    private float DistToClstObj;
    private GameObject ClstObject;
    private float InteractRange;
    private bool Interactable;


    [Space][Space][Space]
    [SerializeField] private InteractAction actions;
    [Header("Item Management, pickup variables")]       //header #3
    [SerializeField] LayerMask ItemPickupLayer;
    [SerializeField] LayerMask InteractLayer;

    private GameObject itemnearby;
    [SerializeField] private GameObject PL_InventoryController;

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

    private float GetDistance(Vector2 a,Vector2 b)
    {
        float distance = Mathf.Abs(Mathf.Sqrt((a.x-b.x)*(a.x-b.x)+(a.y-b.y)*(a.y-b.y)));
        return distance;
    }
    private bool IsMoving()
    {
        if (Input.GetAxisRaw("Horizontal") !=0 || Input.GetAxisRaw("Vertical") != 0)
        {
            return true;
        }
        return false;
    }
    
    void Start()
    {
        DistToClstObj = 99999;
        Interactable = false;
        InteractRange=3f;
    }
    // Update is called once per frame
    void Update()
    {
        ///////////////////////////////////////////////////////////
        animator.SetBool("IsMoving",IsMoving());                ///
        animator.SetFloat("Horizontal",LastDirection.x);        ///         Animating
        animator.SetFloat("Vertical",LastDirection.y);          ///
        ///////////////////////////////////////////////////////////
        IsMoving();
        if(IsMoving())
        {
            player.linearVelocity = (Normalize(Input.GetAxisRaw("Horizontal")*speed,Input.GetAxisRaw("Vertical")*speed));   
        }
        else
        {
            player.linearVelocity = new Vector2(0,0);
        }
        
        ////////////////////////////////////////////////////////////////////////////////////
        if (Input.GetAxisRaw("Horizontal") !=0 && Input.GetAxis("Vertical") ==0){///////////    Check Movement
            LastDirection = new Vector2(Input.GetAxis("Horizontal")/Mathf.Abs(Input.GetAxis("Horizontal")),0);//return (-)1,0
            if (Input.GetAxisRaw("Horizontal") ==0 && Input.GetAxis("Vertical") != 0)
            {
                LastDirection = new Vector2(0,Input.GetAxis("Horizontal")/Mathf.Abs(Input.GetAxis("Horizontal")));
            }

        }
        else if(Input.GetAxis("Horizontal")==0 && Input.GetAxis("Vertical") !=0){///////////    Check Movement
            LastDirection = new Vector2(0,Input.GetAxis("Vertical")/Mathf.Abs(Input.GetAxis("Vertical")));//return 0,(-1)
            if (Input.GetAxisRaw("Horizontal") !=0 && Input.GetAxis("Vertical") == 0)
            {
                LastDirection = new Vector2(Input.GetAxis("Horizontal")/Mathf.Abs(Input.GetAxis("Horizontal")),0);
            }
        }
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
        Debug.Log(ClstObject?.ToString());
        Debug.Log(DistToClstObj);

        Interactable = (DistToClstObj<InteractRange);

        ////////////////////////////////////
        ///                              ///
        ///     check if Interactable    ///
        ///                              ///
        ////////////////////////////////////
        
        
        
        ///////////////////////////////
        // Interacting with objects  //
        ///////////////////////////////
        if (Interactable && ClstObject != null)
        {
            switch (ClstObject.tag)
            {
                case "Tree":
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        actions.ChoppingTree(actions.inventoryController.m_ItemDex.GetComponent<ItemDex>().itemdex[2],ClstObject);
                    
                    }
                    break;
            }

                
        }
        
        //this is the part where I make pickup item func

        itemnearby = TouchItem()?.gameObject;
        if (itemnearby != null)
        {
            itemnearby.GetComponent<PhysicalItem>().ConvertFromObjectToItem(PL_InventoryController);
        }
        
    }//end of Update

}//end of Class
