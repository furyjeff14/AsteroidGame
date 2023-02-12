using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptable : ScriptableObject
{
    //Use from presets for weapons for now, we will be spending too much time for its customization
    public Weapon weaponPrefab;
}
