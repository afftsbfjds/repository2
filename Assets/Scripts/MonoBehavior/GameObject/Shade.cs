using UnityEngine;

public class Shade : MonoBehaviour
{
    private Sprite shade;
    public SpriteRenderer SpriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer.sortingLayerName = gameObject.GetComponentInParent<SpriteRenderer>().sortingLayerName;
        SpriteRenderer.sortingOrder = gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder;
    }
    void Update()
    {
        transform.localPosition = new Vector3(0,0.25f,0);
    }
    
}
