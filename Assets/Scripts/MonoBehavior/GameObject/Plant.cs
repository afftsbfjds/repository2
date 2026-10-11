using UnityEngine;
using UnityEngine.Tilemaps;

public class Plant : MonoBehaviour
{
    
    
    [SerializeField] private Sprite[] GrowthState;
    [SerializeField] private Sprite DeathState;
    private Sprite currentState;
    [SerializeField] private float TimeGrown;
    [SerializeField] private float TimeRequired;
    [SerializeField] private float WaterReqTimer;
    [SerializeField] private TileBase Soil;
    [SerializeField] private TileBase WateredSoil;
    private TileBase Location;
    private void Start()
    {
        GetComponent<Interactable>().enabled = false;
    }
    private void RefreshVisual()
    {
        this.GetComponent<SpriteRenderer>().sprite = currentState;
    }

    void Update()
    {
        Location = TileMap.Instance.Map.GetTile(TileMap.Instance.Map.WorldToCell(transform.position));
        if(GrowthState[GrowthState.Length-1] == currentState)
        {
            GetComponent<Interactable>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
        /// GROWING
        else
        {
            /// GROWING
            TimeGrown+=Time.deltaTime;
            if (TimeGrown >= TimeRequired)  ///     TRYING TO GROW TO NEXT STATE
            {
                if(TimeGrown >=WaterReqTimer && Location != WateredSoil)    /// SOIL IS NOT WATERED, PLANT DIES
                {
                    Debug.Log("THIS PLANT IS NOT WATERED!");
                    currentState = DeathState;
                    GetComponent<Interactable>().Output1 = null;
                    GetComponent<Interactable>().Output2 = null;
                    GetComponent<Interactable>().OutputRandom = null;
                    RefreshVisual();
                    GetComponent<Interactable>().enabled = true;
                    GetComponent<Collider2D>().enabled = true;
                    this.enabled = false;
                    return;
                }
                /// SOIL IS WATERED, ABSORB WATER
                if (TimeGrown >= WaterReqTimer / 100)
                {
                    Location = Soil;
                }

                /// GROW TO NEXT STATE
                currentState = GrowthState[(int)(TimeGrown/TimeRequired)];//resprite to the next state
                RefreshVisual();
            }
        }
    }
        
}
