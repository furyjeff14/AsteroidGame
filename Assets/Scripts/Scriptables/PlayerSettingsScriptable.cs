using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettingsScriptableObject", menuName = "ScriptableObjects/Player")]
public class PlayerSettingsScriptable : ScriptableObject
{
    public int startingLives = 3;
    public float shipSpeed = 5.4f;
    public float rotationSpeed = 0.4f;
}
