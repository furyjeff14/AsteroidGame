using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerupGeneratorScriptableObject", menuName = "ScriptableObjects/PowerupGenerator")]
public class PowerupGeneratorScriptable : ScriptableObject
{
    public float powerupSpawnTimer = 5;
}
