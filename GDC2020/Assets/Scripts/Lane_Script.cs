using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane_Script : MonoBehaviour
{    
    void OnBecameInvisible()
    {
        //if it is a lower lane
        if (transform.position.z - 4 == RoadGenerator.next_lower_lane)
        {
            RoadGenerator.next_lower_lane = transform.position.z;
            transform.position = new Vector3(transform.position.x, transform.position.y, RoadGenerator.next_higher_lane);
            RoadGenerator.next_higher_lane += 4f;
        }
        //otherwise it must be a higher lane
        else
        {
            RoadGenerator.next_higher_lane = transform.position.z;
            transform.position = new Vector3(transform.position.x, transform.position.y, RoadGenerator.next_lower_lane);
            RoadGenerator.next_lower_lane -= 4f;
        }
    }
}
