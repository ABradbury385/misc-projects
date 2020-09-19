using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // Camera array that holds a reference to every camera in the scene
    public Camera[] cameras;

    // Current camera 
    private int currentCameraIndex;
    
    // Use this for initialization
    void Start () 
    {
        currentCameraIndex = 0;

        // Turn all cameras off, except the first default one
        for (int i=1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        // If any cameras were added to the controller, enable the first one
        if (cameras.Length > 0)
        {
            cameras [0].gameObject.SetActive (true);
        }
    }
    
    // Update is called once per frame
    void Update () 
    {
        // Press the 'C' key to cycle through cameras in the array
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            // Cycle to the next camera
            currentCameraIndex ++;

            // If cameraIndex is in bounds, set this camera active and last one inactive
            if (currentCameraIndex < cameras.Length)
            {
                cameras[currentCameraIndex-1].gameObject.SetActive(false);
                cameras[currentCameraIndex].gameObject.SetActive(true);
            }

            // If last camera, cycle back to first camera
            else
            {
                cameras[currentCameraIndex-1].gameObject.SetActive(false);
                currentCameraIndex = 0;cameras[currentCameraIndex].gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// OnGUI()
    /// Purpose: display information and directions for the user
    /// </summary>
    private void OnGUI()
    {
        string descriptionLine;
        Rect textbox = new Rect(5, 5, 300, 40); //rectangle for IMGUI box

        //change IMGUI text based on camera
        //"Press 'c' key to change cameras"
        //"Camera x: Description"
        switch(currentCameraIndex)
        {
            case 0:
                descriptionLine = "Camera 1: Overhead view of the terrain";
                GUI.Box(textbox, "Press the 'c' key to change cameras" +
            "\n" + descriptionLine);
                break;

            case 1:
                descriptionLine = "Camera 2: Side view of the terrain";
                GUI.Box(textbox, "Press the 'c' key to change cameras" +
            "\n" + descriptionLine);
                break;

            case 2:
                descriptionLine = "Camera 3: Leaders close-up view";
                GUI.Box(textbox, "Press the 'c' key to change cameras" +
            "\n" + descriptionLine);
                break;

            case 3:
                descriptionLine = "Camera 4: Horde close-up view";
                GUI.Box(textbox, "Press the 'c' key to change cameras" +
            "\n" + descriptionLine);
                break;

            case 4:
                descriptionLine = "Camera 5: Horde mid view";
                GUI.Box(textbox, "Press the 'c' key to change cameras" +
            "\n" + descriptionLine);
                break;

            case 5:
                descriptionLine = "Camera 6: First-person view";
                GUI.Box(textbox, "Press the 'c' key to change cameras" +
            "\n" + descriptionLine);
                break;

            //catch error with displaying IMGUI
            default:
                Debug.Log("Something went wrong when changing GUI Text!");
                break;
        }
    }
}
