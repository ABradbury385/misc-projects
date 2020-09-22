using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;
    public float moveSpeed = 3.0f;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    public int Health { get { return currentHealth; } }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");         //gets base Unity input controls (-1, 0, or 1)
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        //fix up the jittery collisions
        Vector2 position = rigidbody2d.position;
        position.x += moveSpeed * horizontal * Time.deltaTime;       //when using delta time, char. should move 3-4 units/sec, "frame independent"
        position.y += moveSpeed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
