using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseDirection : MonoBehaviour
{
    public Nave naveComponent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("space"))
	    {
            naveComponent.Reverse();
        }
	}
}
