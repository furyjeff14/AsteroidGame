using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public PlayerControls player;
    public GameObject playerShip;
    public GameObject asteroid;
    bool isTutorial = true;
    float gameCameraSize = 5;
    
    Vector3 startingShipPos;
    Vector3 targetShipPos;
    Vector3 asteroidPos;
    Vector3 asteroidEuler;
    float duration = 3;
    float currentLerp = 0;
    bool isShowAsteroid = false;
    bool isWaitForInput = false;
    bool isCutsceneComplete = false;

    private void Awake()
    {
        player = playerShip.GetComponent<PlayerControls>();
    }

    void Start()
    {
        startingShipPos = new Vector3(-4.089f, -0.08f, 0);
        targetShipPos = new Vector3(-1.561f, -0.08f, 0);
        asteroidPos = new Vector3(2.149f, 2.592f, 0);
        asteroidEuler = new Vector3(0, 0, 180);
    }

    private void Update()
    {
        if (isCutsceneComplete)
        {
            return;
        }
        if (isWaitForInput && Input.GetKeyDown(KeyCode.Space))
        {
            isCutsceneComplete = true;
            Time.timeScale = 1;
            GameManager.Instance.StartGame();
            player.Fire();
        }
        if (currentLerp < duration)
        {
            playerShip.transform.position = Vector3.Lerp(startingShipPos, targetShipPos, currentLerp / duration);
            currentLerp += Time.deltaTime;
        } else if(!isShowAsteroid)
        {
            ShowAsteroid();
        }
    }

    private void ShowAsteroid()
    {
        isShowAsteroid = true;
        StartCoroutine(IShowAsteroid());
    }

    private IEnumerator IShowAsteroid()
    {
        SpawnAsteroid();
        yield return new WaitForSeconds(2.0f);
        Camera.main.orthographicSize = gameCameraSize;
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.ToggleIntroductionScreen(true);
        Time.timeScale = 0;
        isWaitForInput = true;

    }

    private void SpawnAsteroid()
    {
        AsteroidGenerator.Instance.GenerateAsteroid(asteroidPos, asteroidEuler);
    }

}
