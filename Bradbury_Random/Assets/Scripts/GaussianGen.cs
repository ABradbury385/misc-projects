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
    //private TerrainData terrainData;

    // Start is called before the first frame update
    void Start()
    {
        leadersArray = new GameObject[numberOfLeaders];
        //terrainData = GetComponent<TerrainCollider>().terrainData;

        for(int i = 0; i < numberOfLeaders; i++)
        {
            leadersArray[i] = Instantiate(myLeaderPrefab);
            float scaleXAndZ = Gaussian(1f, .15f);
            Debug.Log("X and Z Scale: " + scaleXAndZ);
            float scaleY = Gaussian(1f, .20f);
            Debug.Log("Y Scale: " + scaleY);

            leadersArray[i].transform.localScale = new Vector3(scaleXAndZ, scaleY, scaleXAndZ);
        }
    }

    float Gaussian(float mean, float stdDev)
    {
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);
        float gaussValue = Mathf.Sqrt(-2f * Mathf.Log(val1))
            * Mathf.Sin(2f * Mathf.PI * val2);
        return mean + stdDev * gaussValue;
    }

    void PlaceLeaders()
    {

    }
}
