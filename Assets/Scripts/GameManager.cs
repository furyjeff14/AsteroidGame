using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameSettingsScriptable setting;
    public Action StartGameEvent;
    public Action EndGameEvent;

    public int gameDifficulty = 0;
    public bool isGamePaused = false;
    private float difficultyIncreaseTimer = 30;
    private float currentTimer = 30;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 90;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        gameDifficulty = setting.startingDifficulty;
        difficultyIncreaseTimer = setting.difficultyIncreaseTimer;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        } else
        {
            IncreaseDifficulty();
            currentTimer = difficultyIncreaseTimer;
        }
      
    }

    public void StartGame()
    {
        StartGameEvent();
    }

    public void GameOver()
    {
        EndGameEvent?.Invoke();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncreaseDifficulty()
    {
        gameDifficulty++;
    }
}
