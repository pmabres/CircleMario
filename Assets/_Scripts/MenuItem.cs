using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuItem : MonoBehaviour
{
    public bool Selected = false;
    public bool DrawCoin = false;
    public bool Clicked = false;
    public int CurrentIndex { get; set; }
    public MenuSelector MenuSelector { get; set; }
    public GameObject CoinPrefab;
    public float fixedConPositionCorrection = 0;
    private GameObject _instantiatedCoin;
  

	void Awake ()
	{
    }

    public void Select(bool selection)
    {
        Selected = selection;
        DrawPointer();
        
    }

    public void Select()
    {
        MenuSelector.SelectItem(CurrentIndex);
    }

    public void DrawPointer()
    {
        if (DrawCoin && Selected)
        {
            _instantiatedCoin = Instantiate(CoinPrefab, transform.position, Quaternion.identity, transform);
            _instantiatedCoin.transform.position = new Vector3(transform.position.x - fixedConPositionCorrection, transform.position.y, transform.position.z);
                
        }
        else if (_instantiatedCoin != null)
        {
            Destroy(_instantiatedCoin);
        }
    }

    // Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("space") && Selected)
	    {
	        Click();
	    }
	}

    public void Click()
    {
        if (Selected)
        {
            var pointer = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerClickHandler);
        }
    }
}
