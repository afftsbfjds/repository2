using UnityEngine;
using UnityEngine.UI;

public class Recipes : MonoBehaviour
{
    public int RecipeID;
    public RecipeData recipeData;

    [SerializeField] private Item output;
    

    private void Start()
    {
        recipeData = ItemDatabase.Instance.manager.Recipe[RecipeID];
        if(recipeData ==null){
            Debug.LogWarning("THIS RECIPE IS NOT SET!");
            return;
        }
        output = ItemDatabase.Instance.LookUp(recipeData.result.ItemOUTID);
        this.GetComponent<Image>().sprite = output.Data.icon;
    }

    public void SetRecipe()
    {
        CraftingMenu.Instance.currentRecipe = this.recipeData;
    }
}
