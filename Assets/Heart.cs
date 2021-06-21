using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public bool isLightningBall;
    private SpriteRenderer sr;

    public static event Action<Heart> OnHeartDeath;

    private void Awake()
    {
        this.sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void Die()
    {
        OnHeartDeath?.Invoke(this);
        Destroy(this.gameObject);
    }
}
