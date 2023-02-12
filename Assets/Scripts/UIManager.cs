using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject gameOverScreen;
    public GameObject introScreen;
    public GameObject livesScreen;

    public TextMeshProUGUI score;
    public TextMeshProUGUI lives;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.StartGameEvent += StartGame;
        GameManager.Instance.EndGameEvent += EndGame;
    }

    private void StartGame()
    {
        ToggleGameOverScreen(false);
        ToggleIntroductionScreen(false);
        ToggleLivesScreen(true);
    }

    private void EndGame()
    {
        ToggleGameOverScreen(true);
    }

    public void ToggleLivesScreen(bool _enable)
    {
        livesScreen.SetActive(_enable);
    }

    public void ToggleIntroductionScreen(bool _enable)
    {
        introScreen.SetActive(_enable);
    }

    public void ToggleGameOverScreen(bool _enable)
    {
        gameOverScreen.SetActive(_enable);
    }

    public void UpdateLives(int _lives)
    {
        lives.text = _lives.ToString();
    }

    public void UpdateScore(int _score)
    {
        score.text = _score.ToString();
    }

}
