using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupGenerator : MonoBehaviour
{
    public static PowerupGenerator Instance;

    public PowerupGeneratorScriptable setting;
    public List<GameObject> powerups;
    public Transform customPowerupsContainer;
    public GameObject powerupTemplate;
    private float powerupSpawnTimer = 5;
    private float currentSpawnTimer = 5;
    private int xScreenOffset = 0;
    private int yScreenOffset = 0;
    private bool isGeneratePowerups = false;

    private void Awake()
    {
        Instance = this;
        xScreenOffset = Screen.width / 12;
        yScreenOffset = Screen.height / 12;
    }

    private void Start()
    {
        Initialize();

        GameManager.Instance.StartGameEvent += StartGame;
        GameManager.Instance.EndGameEvent += EndGame;
    }

    private void Initialize()
    {
        powerupSpawnTimer = setting.powerupSpawnTimer;
    }

    private void Update()
    {
        if (!isGeneratePowerups)
        {
            return;
        }

        if (currentSpawnTimer > 0)
        {
            currentSpawnTimer -= Time.deltaTime;
        } else 
        {
            SpawnPowerup();
        }
       
    }
    private void EndGame()
    {
        isGeneratePowerups = false;
    }

    private void StartGame()
    {
        currentSpawnTimer = powerupSpawnTimer;
        isGeneratePowerups = true;
    }

    private void SpawnPowerup()
    {
        currentSpawnTimer = powerupSpawnTimer;
        int rand = Random.Range(0, powerups.Count);
        GameObject _ins = GameObject.Instantiate(powerups[rand]);
        _ins.transform.position = GetRandomPosition();
    }

    //Simplest way to add powerups is to just create a prefab and add to powerups list or load a gameobject from somewhere to pass it here
    public void AddToPowerups(GameObject _powerup)
    {
        GameObject _powerupIns = GameObject.Instantiate(_powerup, customPowerupsContainer);
        powerups.Add(_powerupIns);
    }

    private Vector3 GetRandomPosition()
    {
        int xRand = Random.Range(xScreenOffset, Screen.width - xScreenOffset);
        int yRand = Random.Range(yScreenOffset, Screen.height - yScreenOffset);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(xRand, yRand, 0));
        worldPos = new Vector3(worldPos.x, worldPos.y, 0);
        return worldPos;
    }
}
