using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Andrew Bradbury
//Purpose: Generate and place a horde of gameobjects that are more concentrated in the front
//and dwindle out towards the back
public class NonUniformHordeGen : MonoBehaviour
{
    [SerializeField]
    GameObject myPrefab;

    [Range(50, 100)]
    public int numberInTheHorde = 50;
    private Vector3 pos;
    private Vector3 hordeSize;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        hordeSize = new Vector3(15, 10, 40);
        rotation = Quaternion.Euler(0, 180, 0);

        for(int i = 0; i < numberInTheHorde; i++)
        {
            //position along x axis is completely random
            float xOffset = Random.Range(-hordeSize.x, hordeSize.x);
            xOffset += myPrefab.transform.position.x;

            float probability = Random.Range(0f, 1f);

            //z position is also random, but there are more gameobejcts toward the front than in the back
            float zOffset;
            if(probability < .5f)
            {
                //front quarter
                zOffset = Random.Range(0, .25f * hordeSize.z);
            }

            else if(probability < .8f)
            {
                //second quarter
                zOffset = Random.Range(.25f * hordeSize.z, .5f * hordeSize.z);
            }

            else if(probability < .95f)
            {
                //third quarter
                zOffset = Random.Range(.5f *hordeSize.z, .75f * hordeSize.z);
            }

            else
            {
                //back quarter
                zOffset = Random.Range(.75f * hordeSize.z, hordeSize.z);
            }
            
            zOffset += myPrefab.transform.position.z;

            //y posiiton is based on height of the terrain
            float yOffset = Terrain.activeTerrain.SampleHeight(new Vector3(xOffset, 0, zOffset));

            pos = new Vector3(xOffset, yOffset, zOffset);

            Instantiate(myPrefab, pos, rotation);
        }
    }
}
