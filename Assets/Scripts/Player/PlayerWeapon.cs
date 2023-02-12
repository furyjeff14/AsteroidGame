using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControls))]
public class PlayerWeapon : MonoBehaviour
{
    private PlayerControls playerControls;
    private float currentTempWeaponDuration = 10;
    private bool isUsingTempWeapon = false;
    private Weapon baseWeapon;
    private Weapon currentWeapon;

    private void Awake()
    {
        playerControls = GetComponent<PlayerControls>();
        if (currentWeapon == null)
        {
            baseWeapon = gameObject.AddComponent<BaseWeapon>();
            baseWeapon.projectile = Resources.Load<GameObject>(StringConstants.baseProjectileResource);
            currentWeapon = baseWeapon;
        }
    }

    private void Update()
    {
        if (isUsingTempWeapon)
        {
            if (currentTempWeaponDuration > 0)
            {
                currentTempWeaponDuration -= Time.deltaTime;
            } else
            {
                Destroy(currentWeapon);
                currentTempWeaponDuration = 0;
                currentWeapon = baseWeapon;
                isUsingTempWeapon = false;
            }
        }

    }

    void Start() 
    {
        playerControls.WeaponShootEvent += ShootWeapon;
    }

    public void ShootWeapon(Transform _originTransform)
    {
        currentWeapon.ShootWeapon(_originTransform);
    }

    public void PowerupWeapon(PowerupWeapon _powerup)
    {
        if(currentWeapon != baseWeapon)
        {
            Destroy(currentWeapon);
        }
        Weapon _ins = (Weapon)gameObject.AddComponent(_powerup.weapon.GetType());
        _ins.InitializeWeapon(_powerup.weapon);
        currentWeapon = _ins;
        currentTempWeaponDuration = _powerup.powerUpDuration;
        isUsingTempWeapon = true;
    }

}
