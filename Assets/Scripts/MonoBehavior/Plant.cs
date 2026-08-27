using UnityEngine;
using UnityEngine.Tilemaps;

public class Plant : MonoBehaviour
{
    
    
    [SerializeField] private Sprite[] GrowthState;
    private Sprite currentState;
    [SerializeField] private float TimeGrown;
    [SerializeField] private float TimeRequired;
    public bool Harvestable;
    [SerializeField] private Tile Soil;

    private void RefreshVisual()
    {
        this.GetComponent<SpriteRenderer>().sprite = currentState;
    }

    void Update()
    {
        

        if(GrowthState[GrowthState.Length-1] == currentState)
        {
            Harvestable = true;
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
