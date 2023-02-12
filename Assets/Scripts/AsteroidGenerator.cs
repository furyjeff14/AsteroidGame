using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    public static AsteroidGenerator Instance;

    public AsteroidScriptable asteroidSetting;
    public AsteroidGeneratorScriptable generatorSetting;

    public List<GameObject> asteroids;
    public GameObject asteroidPrefab;
    private int baseAsteroids = 5;
    private int additionalAsteroidsPerDifficulty = 3;
    private int timesToSplit = 2;
    private float asteroidSpawnTimer = 30;
    private float currentSpawnTimer = 0;
    private bool isSpawningAsteroid = false;

    private void Awake()
    {
        Instance = this;
        asteroids = new List<GameObject>();
    }

    private void Start()
    {
        Initialize();
        GameManager.Instance.StartGameEvent += StartSpawn;
        GameManager.Instance.EndGameEvent += StopSpawn;
    }

    private void Initialize()
    {
        asteroidSpawnTimer = generatorSetting.asteroidSpawnTimer;
        baseAsteroids = generatorSetting.startingAsteroidCount;
        additionalAsteroidsPerDifficulty = generatorSetting.asteroidsPerDifficulty;
        timesToSplit = generatorSetting.timesToSplit;
    }

    private void StartSpawn()
    {
        isSpawningAsteroid = true;
    }

    private void StopSpawn()
    {
        isSpawningAsteroid = false;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.E))
        {
            GenerateAsteroid();
        }
#endif
        if (!isSpawningAsteroid)
        {
            return;
        }

        if(currentSpawnTimer > 0)
        {
            currentSpawnTimer -= Time.deltaTime;
        } else
        {
            currentSpawnTimer = asteroidSpawnTimer;
            SpawnAsteroidWave();
        }
    }

    private void SpawnAsteroidWave()
    {
        int totalAsteroids = baseAsteroids + (additionalAsteroidsPerDifficulty * GameManager.Instance.gameDifficulty);
        for (int i = 0; i < totalAsteroids; i++)
        {
            GenerateAsteroid();
        }
        currentSpawnTimer = asteroidSpawnTimer;
    }

    public void GenerateAsteroid()
    {
        GameObject _ins = GameObject.Instantiate(asteroidPrefab);
        asteroids.Add(_ins);
        _ins.transform.position = GetRandomPositionOutsideScreen();
        _ins.GetComponent<Asteroid>().SetScriptableSettings(asteroidSetting);
        _ins.GetComponent<Asteroid>().InitializeAsteroid(); 
    }

    public void GenerateAsteroid(Vector3 _pos, Vector3 _euler)
    {
        GameObject _ins = GameObject.Instantiate(asteroidPrefab);
        asteroids.Add(_ins);
        _ins.transform.position = _pos;
        _ins.transform.eulerAngles = _euler;
        _ins.GetComponent<Asteroid>().SetScriptableSettings(asteroidSetting);
        _ins.GetComponent<Asteroid>().AddForce();
    }

    private Vector2 GetRandomPositionOutsideScreen()
    {
        //Direction to spawn the asteroid outside the screen
        //up = 0, right = 1, down = 2, left = 3
        int direction = Random.Range(0, 4);
        
        int boundsToSpawnX = Screen.width / 6;
        int boundsToSpawnY = Screen.height / 6;
        int spawnOffsetX = boundsToSpawnX / 6;
        int spawnOffsetY = boundsToSpawnY / 6;

        int xPosMin = 0;
        int xPosMax = 0;
        int yPosMin = 0;
        int yPosMax = 0;

        switch (direction)
        {
            //Top
            case 0:
                xPosMin = 0;
                xPosMax = Screen.width;
                yPosMax = Screen.height + boundsToSpawnY;
                yPosMin = Screen.height + spawnOffsetY;
                break;
            //Right
            case 1:
                xPosMin = Screen.width + spawnOffsetX;
                xPosMax = Screen.width + boundsToSpawnX;
                yPosMin = 0;
                yPosMax = Screen.height;
                break;
            //Bottom
            case 2:
                xPosMin = 0;
                xPosMax = Screen.width;
                yPosMin = -spawnOffsetY;
                yPosMax = -boundsToSpawnY;
                break;
                //Left
            case 3:
                xPosMin = -boundsToSpawnX;
                xPosMax = -spawnOffsetX;
                yPosMin = 0;
                yPosMax = Screen.height;
                break;
        }

        int randomX = Random.Range(xPosMin, xPosMax);
        int randomY = Random.Range(yPosMin, yPosMax);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(randomX, randomY, 0));
        worldPos = new Vector3(worldPos.x, worldPos.y, 0);
        return worldPos;
    }


    public void SplitAsteroid(Transform _pos, Asteroid _asteroid) {
        
        for (int i = 0; i < timesToSplit; i++)
        {
            GameObject _ins = GameObject.Instantiate(asteroidPrefab);
            asteroids.Add(_ins);
            _ins.GetComponent<Asteroid>().InitializeSplitAsteroid(_pos.position, _asteroid);
        }
    }

    public void RemoveAsteroid(GameObject _asteroid)
    {
        asteroids.Remove(_asteroid);
        if(asteroids.Count == 0)
        {
            SpawnAsteroidWave();
        }
    }

}