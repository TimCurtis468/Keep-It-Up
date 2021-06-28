using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drumstick : MonoBehaviour
{
    public static event Action<Drumstick> OnDrumstickDeath;
    private SpriteRenderer sr;

    private void Awake()
    {
        this.sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void Die()
    {
        OnDrumstickDeath?.Invoke(this);
        Destroy(this.gameObject);
    }
}
