using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelector : MonoBehaviour
{
    public MenuItem[] MenuItems;
    public int startingMenuItem = 0;
    public GameObject prefabPointer;
	// Use this for initialization
	void Start ()
	{
	    for (var i = 0; i < MenuItems.Length; i++)
	    {
	        MenuItems[i].CurrentIndex = i;
	        MenuItems[i].MenuSelector = this;
	        MenuItems[i].CoinPrefab = prefabPointer;
	    }
        MenuItems[startingMenuItem].Select(true);
        if (MenuItems[startingMenuItem].Clicked) MenuItems[startingMenuItem].Click();
    }

    public void SelectItem(int index)
    {
        MenuItems[startingMenuItem].Select(false);
        startingMenuItem = index;
        MenuItems[startingMenuItem].Select(true);
    }
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("down"))
	    {
            MenuItems[startingMenuItem].Select(false);
            if (++startingMenuItem > MenuItems.Length - 1)
	        {
	            startingMenuItem = 0;
	        }
	        MenuItems[startingMenuItem].Select(true);
	    }
	    if (Input.GetKeyDown("up"))
	    {
            MenuItems[startingMenuItem].Select(false);
            if (--startingMenuItem < 0)
	        {
	            startingMenuItem = MenuItems.Length - 1;
	        }
            MenuItems[startingMenuItem].Select(true);
        }
	}
}
