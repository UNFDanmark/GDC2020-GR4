using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    //array for saving how many lanes are in what level
    public static int[] lanes_in_level = new int[5];

    //the current level
    public static int current_lvl = 3;

    //public static vars
    public static float next_higher_lane = 0;
    public static float next_lower_lane = 0;

    //prefab for the lanes
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        //calculate number of lanes for each lvl by adding 2 lanes per lvl, while maintain odd numbers
        //---------!-Hardcode-!-----------//
        for (int i = 0; i < 5; i++)
        {
            lanes_in_level[i] = 3 + i * 2;
        }

        //call the mothod for creating the lanes
        create_lvl();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Method for placing the correct amount of lanes in the world
     */
    private void create_lvl()
    {
        //the default offset
        float z = 0f;
        //loop through lanes
        for (int i = 1; i <= lanes_in_level[current_lvl]; i++)
        {
            //if it is an even number (indexing from 0)
            if (i % 2 == 0)
            {
                //calculate the offset
                z = (i * -2);
                //save the highest possible new lane position for future resets
                next_lower_lane = z - 4;
            }
            //if it is an odd number (indexing from 0)
            else if (i != 1)
            {
                //calculate the offset
                z = ((i - 1) * 2);
                //save the lowest possible new lane position for future resets
                next_higher_lane = z + 4;
            }

            //place the prefab (lane) in the world
            Instantiate(prefab, new Vector3(0, 0, z), Quaternion.Euler(90, 0, 0));
        }
    }
}
