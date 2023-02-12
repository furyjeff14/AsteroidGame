using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public PlayerSettingsScriptable setting;

    public Action ShieldActive;
    public Action ShieldInactive;
    private SpriteRenderer spriteRenderer;
    private int startingLives = 3;
    private int currentLives = 3;
    private int score = 0;
    public bool isShielded = false;
    private bool canBeHit = true;

    private void Awake()
    {
        Instance = this;
        currentLives = startingLives;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        GameManager.Instance.StartGameEvent += StartGame;
    }

    private void Initialize()
    {
        startingLives = setting.startingLives;
    }

    private void StartGame()
    {
        score = 0;
        startingLives = setting.startingLives;
        currentLives = startingLives;
        UIManager.Instance.UpdateLives(currentLives);
        UIManager.Instance.UpdateScore(0);
    
    }

    public void AddScore(int _score)
    {
        score += _score;
        UIManager.Instance.UpdateScore(score);
    }

    //called via SendMessage
    private void GetHit()
    {
        if (isShielded)
        {
            isShielded = false;
            canBeHit = false;
            //Can be used for animations later on but for now let's just change color
            SetShieldedColor(false);
            ShieldInactive?.Invoke();
            StartCoroutine(IBlinkInvulnerability());
            return;
        }

        if (canBeHit)
        {
            currentLives--;
            if(currentLives == 0)
            {
                GameManager.Instance.GameOver();
                GameObject.Destroy(gameObject);
            } else
            {
                canBeHit = false;
                StartCoroutine(IBlinkInvulnerability());
            }
            UIManager.Instance.UpdateLives(currentLives);
        }
    }

    private IEnumerator IBlinkInvulnerability()
    {
        int blinkTimes = 6;
        for (int i = 0; i < blinkTimes; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
            yield return new WaitForSeconds(0.3f);
        }

        canBeHit = true;
    }

    //use shield count if ever we want to add more shield later on
    public void AddShield(int _shieldCount)
    {
        isShielded = true;
        ShieldActive?.Invoke();
        SetShieldedColor(true);
    }

    private void SetShieldedColor(bool _isShielded)
    {
        if (_isShielded)
        {
            spriteRenderer.color = new Color(1, 0, 1, 1);
        } else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }

}
