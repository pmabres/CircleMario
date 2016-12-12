using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyHelper : MonoBehaviour
{
    public int InitialCoinAmount = 20;
    public float InitialFireInterval = 1f;
    public DifficultyModel[] Levels;

    public int GetTotalSpawnedCoins()
    {
        return (int) (Levels[LevelManager.GetCurrentLevel()].LevelCoinMultiplier*InitialCoinAmount);
    }

    public float GetFireInterval()
    {
        return (Levels[LevelManager.GetCurrentLevel()].FireIntensityMultiplier * InitialFireInterval);
    }

    // Use this for initialization
	void Start ()
	{
	    GameManager.DifficultyHelper = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
