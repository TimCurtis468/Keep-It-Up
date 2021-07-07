using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drumstick : MonoBehaviour
{
    public static event Action<Drumstick> OnDrumstickDeath;
    private SpriteRenderer sr;

    public AudioClip[] soundFX;

    private AudioSource audioSource;

    private float MIN_DRUMSTICK_Y_VEL = 100.0f;

    private void Awake()
    {
        this.sr = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponentInChildren<AudioSource>();
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        int numFx;

        if (coll.gameObject.tag == "Paddle")
        {
            numFx = UnityEngine.Random.Range(0, soundFX.Length);
            audioSource.PlayOneShot(soundFX[numFx]);
        }
       else //if (coll.gameObject.tag == "Untagged")
       {
            //            numFx = UnityEngine.Random.Range(0, loFX.Length);
            //            audioSource.PlayOneShot(loFX[numFx]);
            float vel_x;
            float vel_y;
            Rigidbody2D drumstickRb = this.GetComponent<Rigidbody2D>();

            vel_x = drumstickRb.velocity.x;
            vel_y = drumstickRb.velocity.y;

            if ((vel_y > 0) && (vel_y < MIN_DRUMSTICK_Y_VEL))
            {
                vel_y = MIN_DRUMSTICK_Y_VEL;
            }
            else if ((vel_y < 0) && (vel_y > -MIN_DRUMSTICK_Y_VEL))
            {
                vel_y = -MIN_DRUMSTICK_Y_VEL;
            }

            drumstickRb.AddForce(new Vector2(vel_x, vel_y));
        }

    }

    public void Die()
    {
        OnDrumstickDeath?.Invoke(this);
        Destroy(this.gameObject);
    }
}
