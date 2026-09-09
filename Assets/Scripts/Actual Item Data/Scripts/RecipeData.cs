using UnityEngine;

public class RecipeData
{
    public string RecipeName;
    public Ingredient[] ingredients;
    public Result result;


    public RecipeData()
    {
        TextAsset CraftingRecipe = Resources.Load<TextAsset>("Json/CraftingRecipe");

        if(CraftingRecipe==null)
        {
            Debug.LogWarning("Crafting recipe not loaded!");
            return;
        }

        JsonUtility.FromJsonOverwrite(CraftingRecipe.text, this);

        Debug.Log(RecipeName);

    }
}

public class Ingredient
{
    public int IngredientID;
    public int QuantityIN;
}

public class Result
{
    public int ItemOUTID;
    public int QuantityOUT;
}
