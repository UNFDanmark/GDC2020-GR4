using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane_Script : MonoBehaviour
{    
    void OnBecameInvisible()
    {
        //if it is a lower lane
        if (transform.position.z - 4f == RoadGenerator.next_lower_lane)
        {
            //has to be incremented bc we move upwards
            RoadGenerator.next_lower_lane += 4f;
            //reposition the lane at the next higher possible location
            transform.position = new Vector3(transform.position.x, transform.position.y, RoadGenerator.next_higher_lane);
            //enhance the next highest possible position of the next lane
            RoadGenerator.next_higher_lane += 4f;
        }
        //otherwise it must be a higher lane
        else
        {
            //has to be decremented bc we move down
            RoadGenerator.next_higher_lane -= 4f;
            //reposition the lane at the next lower possible location
            transform.position = new Vector3(transform.position.x, transform.position.y, RoadGenerator.next_lower_lane);
            //decrease the next lowest possible position of the next lane
            RoadGenerator.next_lower_lane -= 4f;
        }
    }
}
