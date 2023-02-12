using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidGeneratorScriptableObject", menuName = "ScriptableObjects/AsteroidGenerator")]
public class AsteroidGeneratorScriptable : ScriptableObject
{

    public int startingAsteroidCount = 5;

    //asteroids to spawn is starting startingAsteroidCount + difficulty * asteroidsPerDifficulty
    public int asteroidsPerDifficulty = 3;

    //timer countdown that increases difficulty 
    public float asteroidSpawnTimer = 30;

    //Split asteroids into how many smaller parts
    public int timesToSplit = 2;
}
