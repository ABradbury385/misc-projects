using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public CharacterController2D characterController;
    public PhysicsMaterial2D slippery;
    public PhysicsMaterial2D sticky;
    private Rigidbody2D rb;

    public float moveSpeed = 40f;
    private float horizontal;

    private bool jump = false;
    private bool crouch = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;

        //make sure you don't slide on slopes
        if (horizontal == 0.0f)
        {
            rb.sharedMaterial = sticky;
        }

        //don't stick to platforms
        else
        {
            rb.sharedMaterial = slippery;
        }

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }

        else if(Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        
    }

    void FixedUpdate()
    {
        characterController.Move(horizontal * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
