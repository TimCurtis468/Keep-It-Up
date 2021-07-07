using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public static event Action OnBlockHit;

    private SpriteRenderer sr;

    public Sprite[] brickImages;


    public void Awake()
    {
        int r = UnityEngine.Random.Range(1, 3);
        int g = UnityEngine.Random.Range(1, 3);
        int b = UnityEngine.Random.Range(1, 3);
        int img;
        sr = this.GetComponent<SpriteRenderer>();

        if(brickImages.Length > 0 )
        {
            img = UnityEngine.Random.Range(0, brickImages.Length);
            sr.sprite = brickImages[img];
        }

        //        sr.color = new Color(r, g, b);
        sr.color = new Color(r * 0.5f, g * 0.5f, b * 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            OnBlockHit?.Invoke();
        }
    }

            public void RemoveBlock()
    {
        Destroy(this.gameObject);
    }
}
