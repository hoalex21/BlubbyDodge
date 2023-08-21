using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public int speed = 0;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // this.transform.Translate(new Vector2(0, -speed * Time.deltaTime));
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(0, -speed * Time.deltaTime);
    }
}
