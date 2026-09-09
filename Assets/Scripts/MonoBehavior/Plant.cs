using UnityEngine;
using UnityEngine.Tilemaps;

public class Plant : MonoBehaviour
{
    
    
    [SerializeField] private Sprite[] GrowthState;
    private Sprite currentState;
    [SerializeField] private float TimeGrown;
    [SerializeField] private float TimeRequired;
    [SerializeField] private Tile Soil;
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
        

        if(GrowthState[GrowthState.Length-1] == currentState)
        {
            GetComponent<Interactable>().enabled = true;
        }
        else
        {
            TimeGrown+=Time.deltaTime;
            if (TimeGrown >= TimeRequired)
            {
                currentState = GrowthState[(int)(TimeGrown/TimeRequired)];//resprite to the next state
                RefreshVisual();
            }
        }
    }
        
}
