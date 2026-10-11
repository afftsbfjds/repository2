using UnityEngine;

public class YSORT_Controller : MonoBehaviour
{
    private int sortinglayer;
    private SpriteRenderer spriteRenderer;
    public GameObject ShadePrefab;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Shade SHADE = Instantiate(ShadePrefab,this.gameObject.transform).GetComponent<Shade>();
    }
    private void Update()
    {
        sortinglayer = (int)(10000-transform.position.y*10);
        spriteRenderer.sortingOrder = sortinglayer;
    }


}
