using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{
    public virtual void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
