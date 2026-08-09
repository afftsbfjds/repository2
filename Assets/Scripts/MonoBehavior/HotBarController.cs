using UnityEngine;
using UnityEngine.UI;
public class HotBarController : MonoBehaviour
{
        public GameObject Slot;
        public GameObject parentsMenu;
        public int Hotbarsize;
        public GameObject[] ItemGroup;//starteritem
        private Transform currentItemHolding;  //reference to the item player holds

        private int KeyboardOutput=1;
    private int KeyboardNumberOutput(int numberPressed)
    {
        int a = numberPressed;
        // Check number keys 1-9 and 0 (as 10)

            for (int i = 1; i <= Hotbarsize; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
                {
                    a = i;
                    break;
                }
            }
        
        return a;
    }


    void Start()
    {
        for (int i = 0; i < Hotbarsize; i++)
        {
            GameObject slot = Instantiate(Slot,parentsMenu.transform);//create x numbers of slots(include image,name,..)as children of Hotbar
            if(ItemGroup[i] != null)
            {
                GameObject Item = Instantiate(ItemGroup[i],slot.transform);
                Item.transform.localPosition = new Vector2(0,0);
                slot.GetComponent<Slot>().currentitem = Item.GetComponent<Item>();
            }

            
        }



    }

    void Update()
    {
        for(int i = 0; i < 9; i++)
        {
            this.gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.gray;
        }
        KeyboardOutput = KeyboardNumberOutput(KeyboardOutput);
        currentItemHolding = this.gameObject.transform.GetChild(KeyboardOutput-1);
        currentItemHolding.GetComponent<Image>().color = Color.white;
    }
    

}
