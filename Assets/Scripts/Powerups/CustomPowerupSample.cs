using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPowerupSample : BasePowerup
{
    public override void ApplyPowerup(GameObject _target)
    {
        //Custom Powerup
        Debug.Log("Custom powerup!");
    }
}
