using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	// Use this for initialization
	void Awake ()
	{
	    GameManager.GameInstance = this;
        GameManager.GameParent = GameObject.Find("GameParent");
	}

    public void Intro()
    {
        LevelManager.Intro();
    }

    public void NextLevel()
    {
        LevelManager.NextLevel();
    }
}
