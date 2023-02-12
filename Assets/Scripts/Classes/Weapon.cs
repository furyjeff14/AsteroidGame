using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public float weaponCooldown = 1.0f;
    private float currentTimer = 0f;
    private bool isWeaponOnCooldown = false;

    public virtual void Start()
    {
        currentTimer = weaponCooldown;
    }

    public virtual void ShootWeapon(Transform _originTransform)
    {
        if (!isWeaponOnCooldown)
        {
            CreateProjectiles(_originTransform);
            isWeaponOnCooldown = true;
        }
    }

    public virtual void Update()
    {
        if (isWeaponOnCooldown)
        {
            if(currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;
            } else
            {
                currentTimer = weaponCooldown;
                isWeaponOnCooldown = false;
            }
        }
    }

    public virtual void SetProjectile(GameObject _projectile)
    {
        projectile = _projectile;
    }

    public virtual void CreateProjectiles(Transform _originTransform)
    {
        GameObject ins = GameObject.Instantiate(projectile);
        ins.transform.position = _originTransform.position;
        ins.transform.eulerAngles = _originTransform.eulerAngles;
    }

    public virtual void InitializeWeapon(Weapon _weapon)
    {
        weaponCooldown = 1.0f;
        SetProjectile(_weapon.projectile);
    }
}
