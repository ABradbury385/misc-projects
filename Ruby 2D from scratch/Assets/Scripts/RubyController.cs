using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");         //gets base Unity input controls (-1, 0, or 1)
        float vertical = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.x += 3f * horizontal * Time.deltaTime;       //when using delta time, char. should move 3-4 units/sec, "frame independent"
        position.y += 3f * vertical * Time.deltaTime;

        transform.position = position;
    }
}
