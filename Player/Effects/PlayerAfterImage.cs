using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    private float activeTime = 0.2f;
    private float timeActivated;
    private float alpha;
    private float alphaSet = 0.8f;
    private float alphaMult = 0.95f;

    private Transform player;

    private SpriteRenderer SR;
    private SpriteRenderer playerSR;

    private Color color;

    private PlayerAfterImagePool pool;

    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = player.GetComponent<SpriteRenderer>();
        pool = GameObject.FindGameObjectWithTag("AfterImagePool").GetComponent<PlayerAfterImagePool>();

        alpha = alphaSet;
        color = new Color(1, 1, 1, alpha);
        SR.sprite = playerSR.sprite;
        SR.color = color;

        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMult;
        color = new Color(1, 1, 1, alpha);
        SR.color = color;

        if (Time.time >= timeActivated + activeTime)
        {
            pool.AddToPool(gameObject);
        }
    }
}
