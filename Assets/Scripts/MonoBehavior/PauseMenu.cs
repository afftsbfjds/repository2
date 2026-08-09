using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Transform pauseMenu;
    public Transform Hotbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //pauseMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.gameObject.activeSelf)
        {
            pauseMenu.gameObject.SetActive(true);
            Hotbar.gameObject.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pauseMenu.gameObject.activeSelf)
        {
            pauseMenu.gameObject.SetActive(false);
            Hotbar.gameObject.SetActive(true);
        }
    }
}
