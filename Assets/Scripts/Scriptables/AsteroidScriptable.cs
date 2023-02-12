using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidScriptableObject", menuName = "ScriptableObjects/Asteroid")]
public class AsteroidScriptable : ScriptableObject
{
    //Asteroids only split when they are below 3 lives
    public int lives = 3;
    //Minimum asteroid speed
    public int minSpeed = 0;
    //Maximum asteroid speed
    public int maxSpeed = 0;
    //Score to get when smallest asteroids get destroyed
    public int score = 10;
}
