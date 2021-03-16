using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    public Rigidbody2D rb;
    private Vector2 moveVelocity;
    private Vector2 direction;

    public float teleportDist;                  //how far to teleport
    public float teleportCoolDown;              //cooldown in seconds
    private float teleportCoolDownTimer = 0;    //cooldown timer
    private bool recentlyTeleported = false;

    public float afterTeleStunFrames;
    private float afterTeleStunTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        afterTeleStunTimer = afterTeleStunFrames;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        direction = moveVelocity.normalized;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(teleportCoolDownTimer <= 0)
            {
                Teleport();
                teleportCoolDownTimer = teleportCoolDown;
            }
            else
            {
                Debug.Log("Teleport not recharged yet");
            }
        }

        teleportCoolDownTimer -= Time.deltaTime;

        //can't move for a short time after teleporting
        if (recentlyTeleported)
        {
            if(afterTeleStunTimer > 0)
            {
                moveVelocity = Vector2.zero;
                afterTeleStunTimer -= Time.deltaTime;
            }
            else
            {
                afterTeleStunTimer = afterTeleStunFrames;
                recentlyTeleported = false;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Teleports the player a short distance in the direction they are looking
    /// Note: the player will not go anywhere if idle (maybe a side step?)
    /// </summary>
    void Teleport()
    {
        recentlyTeleported = true;
        rb.position += teleportDist * direction;
    }
}
