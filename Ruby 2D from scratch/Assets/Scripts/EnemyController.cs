using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float moveSpeed = 3.0f;
    public bool vertical;

    int direction = 1;
    float changeTime = 3.0f;
    float directionTimer;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        directionTimer = changeTime;
    }

    private void Update()
    {
        directionTimer -= Time.deltaTime;

        //after x seconds, change direction and reset timer
        if(directionTimer < 0)
        {
            direction = -direction;
            directionTimer = changeTime;
        }
    }

    //call movement once every set rate
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        if (!vertical)
            position.x += moveSpeed * direction * Time.deltaTime;
        else
            position.y += moveSpeed * direction * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    //Damage player upon contact with robot
    void OnCollisionEnter2D(Collision2D other)
    {
        //need to call other.gameObject.Get... bc Collision2D doesn't have a GetComponent method
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

}
