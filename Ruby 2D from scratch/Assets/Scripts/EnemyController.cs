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

    Animator animator;

    bool isBroken = true;

    public ParticleSystem smokeEffect;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        directionTimer = changeTime;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //don't move if robot is not broken
        if (!isBroken)
            return;

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
        //don't move if robot is not broken
        if (!isBroken)
            return;

        Vector2 position = rigidbody2d.position;
        if (!vertical)
        {
            position.x += moveSpeed * direction * Time.deltaTime;

            //set parameters of animator to direction if x and 0 if y
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        else
        {
            position.y += moveSpeed * direction * Time.deltaTime;

            //set parameters of animator to direction if y and 0 if x
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
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

    //fixes robot, needs to be public so projectile can access it
    public void Fix()
    {
        isBroken = false;
        rigidbody2d.simulated = false;      //projectiles go through robot and Ruby is not hurt
        animator.SetTrigger("Fixed");       //he does a happy dance :)
        smokeEffect.Stop();                 //stop smoking, looks more natural than Destroy
    }
}
