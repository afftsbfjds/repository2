using UnityEngine;

public class CraftingMenu : MonoBehaviour
{
    private InventoryController inventoryController =>
    InventoryController.Instance;

    private ItemDatabase database =>
    ItemDatabase.Instance;


    [SerializeField] private RecipeManager recipeManager;
    private RecipeData currentRecipe;
    private void Craft()
    {
        if(currentRecipe == null)
        {
            Debug.Log("Not Crafting!");
            return;
        }
        foreach(Ingredient ingredient in currentRecipe.ingredients)
        {
            if (database.LookUp(ingredient.IngredientID) == null)
            {
                Debug.LogWarning("INGREDIENT NOT EXISTING IN DATABASE!");
                return;
            }
            ItemData requiredItemData = database.LookUp(ingredient.IngredientID);
            Item RequiredItem = requiredItemData.owner;

            ItemData MaterialHad = null;
            if (inventoryController.ItemInInven(RequiredItem, inventoryController.parentsMenu)==null)
            {
                if (inventoryController.ItemInInven(RequiredItem, inventoryController.Hotbar) == null)
                {
                    Debug.Log("MATERIAL REQUIRED NOT FOUND!");
                    return;
                }
            }
            MaterialHad = requiredItemData;

            if(MaterialHad == null)
                return;
            
            if(ingredient.QuantityIN <= 0)
            {
                Debug.LogWarning("QUANTITY Needed IS BUGGED!");
                return;
            }
            
        }
    }
}
