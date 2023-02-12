using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance;

    //Use an event here to trigger sounds, animation etc
    public event Action<BasePowerup> OnPowerupGained;

    private void Awake()
    {
        Instance = this;
    }

    public void PowerupEvent(BasePowerup _powerup)
    {
        OnPowerupGained.Invoke(_powerup);
    }
}
