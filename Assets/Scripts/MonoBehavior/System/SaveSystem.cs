using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    string SavePath;
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        SavePath = Path.Combine(Application.persistentDataPath,"SaveData.json");
    }

    private void Save()
    {
        
    }
}

public class SaveData
{
    public Vector3 Playerpos;
}
