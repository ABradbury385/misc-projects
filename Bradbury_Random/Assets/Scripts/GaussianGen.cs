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
    public int numberOfLeaders = 8;
    private TerrainData terrainData;

    // Start is called before the first frame update
    void Start()
    {
        leadersArray = new GameObject[numberOfLeaders];
        terrainData = GetComponent<TerrainCollider>().terrainData;

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
            leaderGap = leadersArray[i].transform.localScale.z / 2 + 1;

            float xOffset = Random.Range(-1f, 1f) + leadersArray[i].transform.position.x;
            float yOffset = leadersArray[i].transform.position.y;
            float zOffset = (float) i  * leaderGap + leadersArray[i].transform.position.z;

            leadersArray[i].transform.position = new Vector3(xOffset, yOffset, zOffset);
        }
    }
}
