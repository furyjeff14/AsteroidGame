using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePowerup : MonoBehaviour
{
    public enum PowerupType
    {
        WEAPON = 0,
        BUFF = 1
    }

    public Sprite icon;
    public int powerUpType = 0;
    public int powerUpDuration = 10;


    public virtual void Awake()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = icon;
    }

    public virtual void ApplyPowerup(GameObject _target)
    {

    }

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.transform.tag == StringConstants.playerTag)
        {
            PowerupManager.Instance.PowerupEvent(this);
            ApplyPowerup(_collider.gameObject);
            GameObject.Destroy(gameObject);
        }
    }
}
