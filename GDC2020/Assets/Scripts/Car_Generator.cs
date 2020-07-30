using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Generator : MonoBehaviour
{
    //private vars
    private int lane_count = 0;
    private bool[] lanes;
    private int current_lvl = RoadGenerator.current_lvl;

    //public vars
    public GameObject prefab;

    public static float next_higher_car = 0;
    public static float next_lower_car = 0;

    // Start is called before the first frame update
    void Start()
    {
        //calculate lanes
        lane_count = 3 + current_lvl * 2;
        //array representing if a lane has a car
        lanes = new bool[lane_count];
    }

    // Update is called once per frame
    void Update()
    {
        //loop through the lanes
        for (int i = 0; i < lane_count; i++)
        {
            //if there is not a car, one will be created
            if (!lanes[i])
            {
                //z position
                float z = 0;
                //if it is an even number (indexing from 0)
                if ((i + 1) % 2 == 0 && (i + 1) != 1)
                {
                    //calculate the offset
                    z = ((i + 1) * -2) - (RoadGenerator.road_offset * ((i + 1) / 2));
                    next_lower_car = z + 4f;
                }
                //if it is an odd number (indexing from 0)
                else if (((i + 1) + 1) != 1)
                {
                    //calculate the offset
                    z = (((i + 1) - 1) * 2) + (RoadGenerator.road_offset * (i / 2));
                    next_higher_car = z + 4;
                }

                //car object
                GameObject car;

                //var for the sinde
                int side = Random.Range(0, 2);
                if (side == 0)
                {
                    //right
                    //set to position and rotate, sothat it drives the right way
                    car = Instantiate(prefab, new Vector3(33, 1, z), Quaternion.Euler(0, 180, 0));
                }
                else
                {
                    //left
                    car = Instantiate(prefab, new Vector3(-33, 1, z), Quaternion.identity);
                }

                //set position true, representing a car
                lanes[i] = true;
            }
        }

    }


}
