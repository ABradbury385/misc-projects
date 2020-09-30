using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;
    public float moveSpeed = 3.0f;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    SpriteRenderer rubySprite;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);      //need to store direction for animations

    public GameObject projectilePrefab;
    public float projectileForce = 300f;

    public ParticleSystem hurtEffect;
    public ParticleSystem healthEffect;

    public int Health { get { return currentHealth; } }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rigidbody2d = GetComponent<Rigidbody2D>();
        rubySprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");         //gets base Unity input controls (-1, 0, or 1)
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            //if move x or move y don't equal (approximately) 0, Ruby is moving, set look direction and normalize (magnitue is 1)
            //if she is not moving, look direction remains as direction she last looked in
            lookDirection.Set(move.x, move.y);  //could be lookDirection = move;
            lookDirection.Normalize();
        }

        //set parameters in animator to look direction and speed
        animator.SetFloat("Look X", lookDirection.x);               //set to lookDirection instead of move vector (this will break anims if not right)
        animator.SetFloat("Look Y", lookDirection.y);               //set to lookDirection instead of move vector
        animator.SetFloat("Speed", move.magnitude);

        //count down invincibility frames
        if(isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            //make ruby flash grey while invincible
            if (rubySprite.color == Color.white)
                rubySprite.color = Color.grey;
            else
                rubySprite.color = Color.white;

            if (invincibleTimer < 0)
            {
                isInvincible = false;
                rubySprite.color = Color.white;
            }
        }

        //fire projectile if user presses c key
        if(Input.GetKeyDown("c"))
        {
            Launch();
        }

        //try to talk when user presses x key
        if(Input.GetKeyDown("x"))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * .2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if(hit.collider != null)
            {
                //display dialogue if talked to
                NonPlayerCharacter npc = hit.collider.GetComponent<NonPlayerCharacter>();
                if(npc != null)
                {
                    npc.DisplayDialog();
                }
            }
        }
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
        if(amount < 0)
        {
            //if entering area during invincibility frames
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");         //play hit anim when taing damage

            Instantiate(hurtEffect, rigidbody2d.position + Vector2.up *2, Quaternion.identity);
        }

        if(amount > 0)
        {
            //make health particles if picking up health pack
            Instantiate(healthEffect, rigidbody2d.position + Vector2.up * 2, Quaternion.identity);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);        //change health bar
    }

    void Launch()
    {
        //make a new projectile at Ruby's hands with no rotation
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * .5f, Quaternion.identity);

        //get projectile script and use it to call launch with Ruby's look direction and a force
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, projectileForce);

        animator.SetTrigger("Launch");      //play launch anim
    }
}
