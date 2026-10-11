using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMenu : MonoBehaviour
{
    public static CraftingMenu Instance {get ; private set; }
    private InventoryController inventoryController =>
    InventoryController.Instance;

    private ItemDatabase database =>
    ItemDatabase.Instance;

    [SerializeField] private RecipeManager recipeManager;

    public RecipeData currentRecipe;

    private List<Item> MaterialHad = new List<Item>();

    [SerializeField] private GameObject recipePrefab;
    [SerializeField] private GameObject menu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return ;
        }
        Instance = this;
    }

    private void Start()
    {
        recipeManager = ItemDatabase.Instance.manager;
        if(recipePrefab == null)
        {
            Debug.LogWarning("RECIPES ARE NOT LOADED!");
            return;
        }

        for(int i=0;i<ItemDatabase.Instance.manager.Recipe.Length;i++)
        {
            Recipes recipes = Instantiate(recipePrefab,menu.transform).GetComponent<Recipes>();
            recipes.RecipeID = i;
        }
    }
    
    public void Craft()
    {
        if(currentRecipe == null || currentRecipe.ingredients == null)
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
            Item RequiredItem = database.LookUp(ingredient.IngredientID);
            Item materialhad = null;
            
            if (inventoryController.ItemInInven(RequiredItem, inventoryController.parentsMenu)!=null)
            {
                materialhad = inventoryController.ItemInInven(RequiredItem,inventoryController.parentsMenu);
                MaterialHad.Add(materialhad);
            }
            else if (inventoryController.ItemInInven(RequiredItem, inventoryController.Hotbar) != null)
            {
                materialhad = inventoryController.ItemInInven(RequiredItem,inventoryController.Hotbar);
                MaterialHad.Add(materialhad);
            }


            if(MaterialHad == null || MaterialHad.Count==0)
            {
                Debug.Log("NO MATERIAL FOUND!");
                return;
            }

            if(materialhad.NumbersOfItem < ingredient.QuantityIN)
            {
                Debug.Log("NOT ENOUGH!");
                return;
            }

            
            if(ingredient.QuantityIN <= 0)
            {
                Debug.LogWarning("QUANTITY Needed IS BUGGED!");
                return;
            }   /// end of crafting ingredient check loop
        }

            ///         START CRAFTING HERE

        if(currentRecipe.result == null || database.LookUp(currentRecipe.result.ItemOUTID) == null)
        {
            Debug.LogWarning("RECIPE RESULT NOT EXISTING IN DATABASE");
            return ;
        }
        Item result = Instantiate(database.LookUp(currentRecipe.result.ItemOUTID));
        result.Data = database.LookUp(currentRecipe.result.ItemOUTID).Data;

        ///         SET RESULT ITEM
        inventoryController.SetItem(result,currentRecipe.result.QuantityOUT);


        ///         USE INGREDIENTS
        for(int i=0;i<MaterialHad.Count; i++)
        {
            MaterialHad[i].NumbersOfItem -=currentRecipe.ingredients[i].QuantityIN;
        }

        ///         CLEAR MATERIALHAD LIST
        MaterialHad.Clear();
    }   ///         CRAFTING FUNCTION


}   /// end of class
