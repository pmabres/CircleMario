using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
    public Sprite[] BackgroundImages;
    public DifficultyHelper DifficultyHelper;
    // Use this for initialization
	void Start () {
		LevelManager.NextLevelEvent += LevelManagerOnNextLevelEvent;
	}

    private void OnDestroy()
    {
        LevelManager.NextLevelEvent -= LevelManagerOnNextLevelEvent;
    }

    private void LevelManagerOnNextLevelEvent(int levelIndex)
    {
        var backgroundImageIndex = 0;
        if (levelIndex > DifficultyHelper.Levels.Length*0.3)
        {
            backgroundImageIndex = 1;
        }
        if (levelIndex > DifficultyHelper.Levels.Length*0.6)
        {
            backgroundImageIndex = 2;
        }
        GetComponent<Image>().sprite = BackgroundImages[backgroundImageIndex];
    }

    // Update is called once per frame
	void Update () {
		
	}
}
