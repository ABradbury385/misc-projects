using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");         //gets base Unity input controls (-1, 0, or 1)
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        //fix up the jittery collisions
        Vector2 position = rigidbody2d.position;
        position.x += 3f * horizontal * Time.deltaTime;       //when using delta time, char. should move 3-4 units/sec, "frame independent"
        position.y += 3f * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }
}
