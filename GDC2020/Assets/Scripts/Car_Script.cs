using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Script : MonoBehaviour
{
    //vars for the cars allowed speeds
    public int car_speed_max = 12;
    public int car_speed_min = 5;

    //private vars for saving the original postion and the speed
    private int car_speed;
    private Vector3 org_position;

    // Start is called before the first frame update
    void Start()
    {
        //randomly determine the cars speed
        car_speed = Random.Range(car_speed_min, car_speed_max + 1);
        //save the basic postion in a Vector3
        org_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //move it
        transform.Translate(car_speed * Time.deltaTime, 0, 0);
    }

    //when collided
    void OnCollisionEnter(Collision collision)
    {
        //if we hit a wall
       if (collision.gameObject.tag == "WALL")
        {
            //move car back to original position
            transform.position = org_position;

            //randomly determine a new speed for the car
            car_speed = Random.Range(car_speed_min, car_speed_max + 1);
        }
    }
}
