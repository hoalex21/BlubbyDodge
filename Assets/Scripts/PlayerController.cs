using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 0;

    private int direction = 1;

    public Sprite sadBlub;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetButtonDown("Fire1") || Input.GetKeyDown("space")) && speed > 0)
        {
            AudioManager.Instance.playSoundEffect("Jump");

            direction *= -1;

            if(sr.flipX == false)
            {
                sr.flipX = true;
            } else
            {
                sr.flipX = false;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * speed * Time.deltaTime, 0);
    }
}
