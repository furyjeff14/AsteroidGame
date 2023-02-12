using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float projectileSpeed = 4.0f;

    public virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    { 
        transform.position += transform.up * Time.deltaTime * projectileSpeed;
    }
}
