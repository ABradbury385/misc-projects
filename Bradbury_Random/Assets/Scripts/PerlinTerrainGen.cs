using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Andrew Bradbury
//Purpose: Generate random terrain using Perlin Noise 
//Limitations: 

[RequireComponent(typeof(TerrainCollider))]
public class PerlinTerrainGen : MonoBehaviour
{
    private TerrainData myTerrainData;
    public int resolution = 129;
    private Vector3 worldSize;
    private float[,] heightMapArray;

    [SerializeField]
    [Range(0f, .99f)]
    private float perlinIncrement = .015f;

    // Start is called before the first frame update
    void Start()
    {
        //set up terrain fields, fill height map and set heights
        myTerrainData = gameObject.GetComponent<TerrainCollider>().terrainData;
        worldSize = new Vector3(200, 50, 200);
        myTerrainData.size = worldSize;
        myTerrainData.heightmapResolution = resolution;
        heightMapArray = new float[resolution, resolution];

        PerlinGen();

        myTerrainData.SetHeights(0, 0, heightMapArray);
    }

    /// <summary>
    /// PerlinGen().
    /// Purpose: Fill a height map with values using Perlin Noise.
    /// </summary>
    void PerlinGen()
    {
        //increment each x and z value and add it to the height map
        float xOff = 0f;
        for(int i = 0; i < resolution; i++)
        {
            float zOff = 10000f;        //start at arbitrary value for variance in x and z value
            for(int j = 0; j < resolution; j++)
            {
                heightMapArray[i, j] = Mathf.PerlinNoise(xOff, zOff);
                zOff += perlinIncrement;
            }
            xOff += perlinIncrement;
        }
    }
}
