using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//Author: Andrew Bradbury
//Purpose: Create and place leaders of a horde in a line with a Gaussian distribution determining scale
public class GaussianGen : MonoBehaviour
{
    [SerializeField]
    private GameObject myLeaderPrefab;

    private GameObject[] leadersArray;
    [Range(8, 10)]
    public int numberOfLeaders = 8;

    // Start is called before the first frame update
    void Start()
    {
        leadersArray = new GameObject[numberOfLeaders];

        for(int i = 0; i < numberOfLeaders; i++)
        {
            //create a new leader and calculate its scale
            leadersArray[i] = Instantiate(myLeaderPrefab);
            float scaleXAndZ = Gaussian(1f, .15f);
            float scaleY = Gaussian(1f, .20f);

            leadersArray[i].transform.localScale = new Vector3(scaleXAndZ, scaleY, scaleXAndZ);
        }

        PlaceLeaders();
    }

    /// <summary>
    /// Gaussian (float, float).
    /// Purpose: Creates a Gaussian distribution value for a given mean and standard deviation.
    /// </summary>
    /// <param name="mean">Average value of data</param>
    /// <param name="stdDev">Standard deviation of the data</param>
    /// <returns>A random value on a Gaussian distribution</returns>
    float Gaussian(float mean, float stdDev)
    {
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);
        float gaussValue = Mathf.Sqrt(-2f * Mathf.Log(val1))
            * Mathf.Sin(2f * Mathf.PI * val2);
        return mean + stdDev * gaussValue;
    }

    /// <summary>
    /// PlaceLeaders()
    /// Purpose: Places leaders in a line along the z axis with a random horizontal offset at the appropriate height
    /// </summary>
    void PlaceLeaders()
    {
        float leaderGap;
        for(int i = 0; i < numberOfLeaders; i++)
        {
            //space the leaders a certain distance apart based on z scale
            leaderGap = 1.5f;

            //find x and z first so that terrain height can be found
            float xOffset = Random.Range(-.5f, .5f) + leadersArray[i].transform.position.x;

            float zOffset = i  * leaderGap + leadersArray[i].transform.position.z;

            //retrieve height of terrain at position of each leader, add y scale so that model is out of the ground
            float yOffset = Terrain.activeTerrain.SampleHeight(new Vector3(xOffset, 0, zOffset))
            + leadersArray[i].transform.localScale.y;     

            leadersArray[i].transform.position = new Vector3(xOffset, yOffset, zOffset);
        }
    }
}
