using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterPowerup : PowerupWeapon
{
    public override void ApplyPowerup(GameObject _target)
    {
        _target.GetComponent<PlayerWeapon>().PowerupWeapon(this);
    }
}
