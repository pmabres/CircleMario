using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager {
    public enum Player
    {
        Mario = 0,
        Luigi = 1
    }

    public enum GameState
    {
        Intro = 0,
        Running = 1,
        Dying = 2,
        LevelSelection = 3,
        GameOver = 4
    }

    public static Game GameInstance { get; set; }

    public static GameObject GameParent { get; set; }

    private static GameState activeGameState;

    public static GameState ActiveGameState
    {
        get
        {
            return activeGameState;
        }
        set
        {
            activeGameState = value;
            if (GameStateChanged != null) GameStateChanged(ActiveGameState);
        }
    }

    public static Player SelectedPlayer { get; set; }

    public static int CurrentLevelScore { get; private set; }

    public static int TotalLevelScore { get; private set; }

    public static void AddScore()
    {
        TotalLevelScore++;
        CurrentLevelScore++;
        if (ScoreChanged != null) ScoreChanged(CurrentLevelScore);
    }

    public static void ResetScore()
    {
        ResetCurrentScore();
        if (ScoreChanged != null) ScoreChanged(CurrentLevelScore);
        TotalLevelScore = 0;
    }

    public static void ResetCurrentScore()
    {
        CurrentLevelScore = 0;
        if (ScoreChanged != null) ScoreChanged(CurrentLevelScore);
    }

    public static DifficultyHelper DifficultyHelper { get; set; }

    public static void CheckLevelStatus()
    {
        if (DifficultyHelper != null && CurrentLevelScore == DifficultyHelper.GetTotalSpawnedCoins())
        {
            if (LevelManager.GetCurrentLevel() >= DifficultyHelper.Levels.Length)
            {
                LevelManager.GameOver();
            }
            else
            {
                LevelManager.SelectLevel();
            }
        }
    }

    public static void SaveScore()
    {
        Score score = new Score()
        {
            ScoreValue = TotalLevelScore,
            ScoreDate = DateTime.Now
        };
        var scoreList = GetAllPlayerPrefScores();
        var scoreListSize = scoreList.Count;
        Debug.Log(scoreListSize);
        var insertedScore = false;
        var index = 0;
        for (index = 0; index < scoreListSize; index++)
        {
            if (score.ScoreValue > scoreList[index].ScoreValue && !insertedScore)
            {
                scoreList.Insert(index, score);
                insertedScore = true;
                scoreListSize++;
                index++;
            }
            if (index == 10)
            {
                scoreList.RemoveAt(index);
            }
        }
        if (index < 10 && index >= scoreListSize && !insertedScore)
        {
            scoreList.Add(score);
        }
        if (scoreListSize == 0)
        {
            
        }
        SaveListPlayerPrefScores(scoreList);
    }

    private static void SaveListPlayerPrefScores(List<Score> scoreList)
    {
        PlayerPrefs.SetInt("SavedScores", scoreList.Count);
        for (int i = 0; i < scoreList.Count; i++)
        {
            PlayerPrefs.SetInt("Score" + i,scoreList[i].ScoreValue);
            PlayerPrefs.SetString("ScoreDate" + i, scoreList[i].ScoreDate.ToString());
        }
        PlayerPrefs.Save();
    }

    public static List<Score> GetAllPlayerPrefScores()
    {
        var scoreList = new List<Score>();
        var savedScoresAmount = PlayerPrefs.GetInt("SavedScores");
        for (int i = 0; i < savedScoresAmount; i++)
        {
            scoreList.Add(new Score()
            {
                ScoreValue = PlayerPrefs.GetInt("Score" + i),
                ScoreDate = DateTime.Parse(PlayerPrefs.GetString("ScoreDate" + i))
            });
        }
        return scoreList;
    }

    public struct Score
    {
        public int ScoreValue { get; set; }
        public DateTime ScoreDate { get; set; }
    }
    
    public static event OnScoreChanged ScoreChanged;
    public delegate void OnScoreChanged(int score);

    public static event OnGameStateChanged GameStateChanged;
    public delegate void OnGameStateChanged(GameState state);
}
