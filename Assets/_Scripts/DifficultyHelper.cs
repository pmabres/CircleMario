using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyHelper : MonoBehaviour
{
    public int InitialCoinAmount = 20;
    public float InitialFireInterval = 1f;
    public DifficultyModel[] Levels;
    public float BulletSpeed = 2;
    public int GetTotalSpawnedCoins()
    {
        return (int) (Levels[LevelManager.GetCurrentLevel()].LevelCoinMultiplier*InitialCoinAmount);
    }

    public float GetFireInterval()
    {
        return (Levels[LevelManager.GetCurrentLevel()].FireIntensityMultiplier * InitialFireInterval);
    }

    public float GetMushroomChance()
    {
        return (Levels[LevelManager.GetCurrentLevel()].MushroomChance);
    }

    public float GetPowChance()
    {
        return (Levels[LevelManager.GetCurrentLevel()].PowChance);
    }

    public float GetEnemyChance()
    {
        return (Levels[LevelManager.GetCurrentLevel()].EnemyChance);
    }

    // Use this for initialization
	void Start ()
	{
	    GameManager.DifficultyHelper = this;
	}

    public float GetBulletSpeed()
    {
        return BulletSpeed;
    }

    // Update is called once per frame
	void Update () {
		
	}
}
