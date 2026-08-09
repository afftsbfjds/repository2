using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    public GameObject[] Pages;   //these contain stats page,setting,inventory
    public void SwitchPagetemp(int ID)
    {
        foreach(GameObject page in Pages)
        {
            page.SetActive(false);
        }
        Pages[ID].SetActive(true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //foreach(GameObject page in Pages)
        //{
        //    page.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
