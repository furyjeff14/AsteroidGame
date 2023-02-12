using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : Weapon
{
    private int burstCount = 3;
    private float burstDelay = 0.1f;

    public override void CreateProjectiles(Transform _originTransform)
    {
        StartCoroutine(ICreateProjectiles(_originTransform));
    }

    private IEnumerator ICreateProjectiles(Transform _originTransform)
    {
        for (int i = 0; i < burstCount; i++)
        {
            yield return new WaitForSeconds(burstDelay);
            GameObject ins = GameObject.Instantiate(projectile);
            ins.transform.position = _originTransform.position;
            ins.transform.eulerAngles = _originTransform.eulerAngles;
        }
    }
}
