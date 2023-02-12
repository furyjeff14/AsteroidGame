using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : BasePowerup
{
    public override void ApplyPowerup(GameObject _target)
    {
        _target.GetComponent<PlayerStats>().AddShield(1);
    }
}
