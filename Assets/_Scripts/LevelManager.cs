using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager {
    private static int _currentLevel = 0;

    public static int GetCurrentLevel()
    {
        return _currentLevel;
    }

    public static void NextLevel()
    {
        _currentLevel++;
        if (NextLevelEvent != null) NextLevelEvent(_currentLevel);
        GameManager.ResetCurrentScore();
        foreach (var gameObject in GameManager.GameInstance.GetComponent<LevelComplete>().LevelCompleteUI)
        {
            gameObject.SetActive(false);
        }
        GameManager.GameParent.SetActive(true);
        GameManager.ActiveGameState = GameManager.GameState.Running;

    }

    public static void SelectLevel()
    {
        GameManager.ActiveGameState = GameManager.GameState.LevelSelection;
        foreach (var gameObject in GameManager.GameInstance.GetComponent<LevelComplete>().LevelCompleteUI)
        {
            gameObject.SetActive(true);
        }
        GameManager.GameParent.SetActive(false);
    }

    public static void GameOver()
    {
        _currentLevel = -1;
        GameManager.ActiveGameState = GameManager.GameState.GameOver;
        GameManager.SaveScore();
        SceneManager.LoadScene(2);
    }

    public static void Intro()
    {
        GameManager.ResetScore();
        GameManager.ActiveGameState = GameManager.GameState.Intro;
        _currentLevel = 0;
        SceneManager.LoadScene(0);
    }

    public static void Play()
    {
        _currentLevel = 1;
        GameManager.ActiveGameState = GameManager.GameState.Running;
        SceneManager.LoadScene(1);
    }

    public static void Die()
    {
        GameManager.ActiveGameState = GameManager.GameState.Dying;
    }

    public delegate void OnNextLevel(int levelIndex);

    public static event OnNextLevel NextLevelEvent;
}
