using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerControls.IsAccelerating += BoosterAnim;
        PowerupManager.Instance.OnPowerupGained += PowerupAnim;
    }


    void BoosterAnim()
    {
        //Spaceship booster anim
    }

    void PowerupAnim(BasePowerup _powerup)
    {
        if (_powerup.powerUpType == (int)BasePowerup.PowerupType.BUFF)
        {
            //Shield Animation
        }
    }
}
