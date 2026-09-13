using UnityEngine;
public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance {get ; set;}

    public ItemData[] database;

    public RecipeManager manager = new RecipeManager();


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public ItemData LookUp(int ID)
    {
        return database[ID];
    }
    public void Start()
    {
        if(manager == null)
        {
            return;
        }
        TextAsset CraftingRecipe = Resources.Load<TextAsset>("Json/CraftingRecipe");

        if(CraftingRecipe==null)
        {
            Debug.LogWarning("Crafting recipe not loaded!");
            return;
        }

        manager = JsonUtility.FromJson<RecipeManager>(CraftingRecipe.text);

        if(manager == null)
        {
            Debug.LogWarning("RECIPE MANAGER NOT INITIALIZED!");
        }
        if(manager.Recipe == null)
        {
            Debug.LogWarning("RECIPE LIST NOT CREATED!");
            return;
        }
        foreach(RecipeData recipes in manager.Recipe)
        {
            if(recipes == null)
            {
                Debug.LogWarning("RECIPES NOT CREATED!");
            }
        }
        foreach( RecipeData recipe in manager.Recipe)
        {
            if(recipe == null)
            {
                Debug.LogWarning("RECIPE NOT LOADED!");
                return;
            }
            if(recipe.RecipeName == null)
            {
                Debug.LogWarning("RECIPE NAME NOT LOADED!");
                return;
            }
            if(recipe.result == null)
            {
                Debug.LogWarning("RECIPE OUTPUT NOT LOADED!");
                return;
            }
            if(recipe.ingredients == null)
            {
                Debug.Log("INGREDIENTS NOT LOADED!");
                return;
            }
            Debug.Log(LookUp(recipe.result.ItemOUTID).Name);
        }
    }
}



[System.Serializable]
public class RecipeManager
{
    public RecipeData[] Recipe;
}
