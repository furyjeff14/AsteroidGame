using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsScriptableObject", menuName = "ScriptableObjects/GameSettings")]
public class GameSettingsScriptable : ScriptableObject
{

    public int startingDifficulty = 0;
    //base asteroid count to spawn
    public float difficultyIncreaseTimer = 30;
    //timer countdown that spawns the asteroid
}