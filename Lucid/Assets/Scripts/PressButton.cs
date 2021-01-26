using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{

    public GameObject box;
    public GameObject door;
    public GameObject button;
    private bool pressed = false;
    private Vector3 originalPos;
    private float rayHeight = 0.35f;


    // Vectors to move the button and cast rays
    private Vector3 downShift = new Vector3(0f, -0.05f);
    private Vector3 hShift = new Vector3(0.45f, 0f);

    private AudioSource buttonAudio;
    private AudioSource doorAudio;
    public AudioClip buttonPressedSound;
    public AudioClip doorClosedSound;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;

        buttonAudio = GetComponent<AudioSource>();
        doorAudio = door.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(originalPos, Vector2.up * rayHeight);
        Debug.DrawRay(originalPos + hShift, Vector2.up * rayHeight);
        Debug.DrawRay(originalPos - hShift, Vector2.up * rayHeight);
        RaycastHit2D hit = Physics2D.Raycast(originalPos, Vector2.up, rayHeight);
        RaycastHit2D hit2 = Physics2D.Raycast(originalPos + hShift, Vector2.up, rayHeight);
        RaycastHit2D hit3 = Physics2D.Raycast(originalPos - hShift, Vector2.up, rayHeight);

        // Consider adding more RaycastHit2D variables with an adjusted first variable
        // to cover more of the button's width.
        if (hit.collider != null || hit2.collider != null || hit3.collider != null)
        {
            if (!pressed) // Check if the button was already pressed
            {
                boxContact();
            }
            
        }
        else // Nothing is on top of the button
        {
            boxRelease();
        }

    }

    /*
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.IsChildOf(buttonParent.transform))
        {
            boxContact();
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.transform.IsChildOf(buttonParent.transform))
        {
            boxRelease();
        }
    }
    */

    void boxContact()
    {
        if (!pressed)
        {
            buttonAudio.PlayOneShot(buttonPressedSound);
            pressed = true;
            this.transform.position = originalPos + downShift;
            door.SetActive(false);
        }
    }

    void boxRelease()
    {
        if (pressed)
        {
            pressed = false;
            this.transform.position = originalPos;
            door.SetActive(true);
            doorAudio.PlayOneShot(doorClosedSound);
        }
    }
}
