using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupWeapon : BasePowerup
{
    public Weapon weapon;

    public override void ApplyPowerup(GameObject target)
    {
        target.GetComponent<PlayerWeapon>().PowerupWeapon(this);
    }

}
