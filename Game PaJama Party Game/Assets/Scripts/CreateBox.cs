using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBox : MonoBehaviour {

    public static Vector3 playerPos;
    public bool boxSpawned = false;
    public GameObject playerBox;
    public GameObject currentBox;
    private float playerFacing = 1f;
    public float rayHeight = 1.5f;
    private RaycastHit2D hit;

    // Player's BoxCollider

    // Vectors adjusting the position the box is spawned
    private Vector3 rightShift = new Vector3(1.5f, 0.25f);
    private Vector3 leftShift = new Vector3(-1.5f, 0.25f);

    public ParticleSystem deathParticles;
    public ParticleSystem badSpawnParticles;

    private AudioSource audioSource;
    public AudioClip badSpawnSound;
    public AudioClip moveBoxSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float tempFacing;
         

        // Check the player's facing if it is a non zero value
        // Positive means right, negative means left
        if ((tempFacing = Input.GetAxisRaw("Horizontal")) != 0)
        {
            playerFacing = tempFacing;
            hit = Physics2D.Raycast(transform.position, Vector2.right, rayHeight);
        }

        if(playerFacing > 0)
        {
            playerPos = transform.position + rightShift;
            // Cast a ray to the right to check if something is in the way
            Debug.DrawRay(playerPos - (rightShift / 1.4f), Vector2.right * rayHeight);
            hit = Physics2D.Raycast(playerPos - (rightShift/1.4f), Vector2.right, rayHeight);
        }
        else if (playerFacing < 0)
        {
            playerPos = transform.position + leftShift;
            // Cast a ray to the left to check if there's something in the way
            Debug.DrawRay(playerPos - (leftShift / 1.4f), Vector2.left * rayHeight);
            hit = Physics2D.Raycast(playerPos - (leftShift/1.4f), Vector2.left, rayHeight);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SpawnBox();
        }
    }

    void SpawnBox()
    {

        // Check if something was in the way of the box
        if (hit.collider == null)
        {
            if (!boxSpawned) // Initially spawn the box
            {
                audioSource.volume = .50f;
                audioSource.PlayOneShot(moveBoxSound);
                currentBox = Instantiate(playerBox, playerPos, Quaternion.identity);
                boxSpawned = true;
            }
            else // Box has already spawned, need to move it to new position
            {
                //currentBox.transform.rotation = Quaternion.identity;
                //currentBox.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //currentBox.transform.position = playerPos;

                audioSource.PlayOneShot(moveBoxSound);
                Instantiate(deathParticles, currentBox.transform.position + Vector3.back, Quaternion.identity);

                Destroy(currentBox);
                currentBox = Instantiate(playerBox, playerPos, Quaternion.identity);
            }
        }
        else
        {
            // Put the particle effect for not having room to spawn a box here
            audioSource.volume = .50f;
            audioSource.PlayOneShot(badSpawnSound);
            Instantiate(badSpawnParticles, playerPos + Vector3.back, Quaternion.identity);
        }    
    }

}
