using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isLightningBall;

    public AudioClip[] soundFX;
    public AudioClip[] loFX;

    private AudioSource audioSource;

    private SpriteRenderer sr;

//    public ParticleSystem lightningBallEffect;

//    public float lightningBallDuration = 10;

    public static event Action<Ball> OnBallDeath;
    //    public static event Action<Ball> OnLightningBallEnable;
    //    public static event Action<Ball> OnLightningBallDisable;

    private void Awake()
    {
        this.sr = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        int numFx;

        if ((coll.gameObject.tag == "Ball") ||(coll.gameObject.tag == "Paddle"))
        {
            numFx = UnityEngine.Random.Range(0, soundFX.Length);
            audioSource.PlayOneShot(soundFX[numFx]);
        }
        else if (coll.gameObject.tag == "Untagged")
        {
            numFx = UnityEngine.Random.Range(0, loFX.Length);
            audioSource.PlayOneShot(loFX[numFx]);
        }

    }

    public void Die()
    {
        OnBallDeath?.Invoke(this);
        Destroy(this.gameObject);
    }

//    internal void StartLightningBall()
 //   {
#if (PI)
        if (this.isLightningBall == false)
        {
            this.isLightningBall = true;
            this.sr.enabled = false;
            lightningBallEffect.gameObject.SetActive(true);
            StartCoroutine(StopLightningBallAfterTime(this.lightningBallDuration));

            OnLightningBallEnable?.Invoke(this);

        }
#endif
//    }

//    private IEnumerator StopLightningBallAfterTime(float seconds)
//    {
//        yield return new WaitForSeconds(seconds);
//
//        StopLightingBall();
//    }

    private void StopLightingBall()
    {
#if (PI)
        if (this.isLightningBall == true)
        {
            this.isLightningBall = false;
            this.sr.enabled = true;
            lightningBallEffect.gameObject.SetActive(false);

            OnLightningBallDisable?.Invoke(this);
        }
#endif
    }
}
