using UnityEngine;
[System.Serializable]
public class RecipeData
{
    public string RecipeName;
    public Ingredient[] ingredients;
    public Result result;



}
[System.Serializable]
public class Ingredient
{
    public int IngredientID;
    public int QuantityIN;
} 
[System.Serializable]
public class Result
{
    public int ItemOUTID;
    public int QuantityOUT;
}
