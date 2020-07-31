using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
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
        //give car random color
        changeColor();
    }

    // Update is called once per frame
    void Update()
    {
        //turn the car in the right direction
        if (org_position.x == 33)
        {
            //if the car starts from the right, it has to point to the left -> 180degrees
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            //if the car starts from the left, it has to point to the right -> 0degrees
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void FixedUpdate()
    {
        //move it
        transform.Translate(car_speed * Time.deltaTime, 0, 0);

        //check for new position
        checkPosition();
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

            //give car random color
            changeColor();
        }
    }

    private void checkPosition()
    {
        //calculate the distance of the car to the player, only z-Axis
        float distance;
        if (player_movement.zPosition < transform.position.z)
        {
            distance = transform.position.z - player_movement.zPosition;
        }
        else
        {
            distance = player_movement.zPosition - transform.position.z;
        }
        //if the distance is greater than 20m the car will be replaced
        if (distance >= 20f)
        {
            //call the replacing method
            replace();
        }
    }

    private void replace()
    {
        //var for saving randomly selected direction
        int x;

        //var for saving the direction
        int dir = Random.Range(1, 3);
        //set the x value to the propper value
        if (dir == 1) { x = 33; } else { x = -33; }

        //if it is a lower lane
        if (transform.position.z - 4f == Car_Generator.next_lower_car || transform.position.z + 4f != Car_Generator.next_higher_car)
        {
            //has to be incremented bc we move upwards
            Car_Generator.next_lower_car += 4f;
            //save the new position as the default position
            org_position = new Vector3(x, transform.position.y, Car_Generator.next_higher_car);
            //move the car to the new position
            transform.position = new Vector3(x, transform.position.y, Car_Generator.next_higher_car);
            //enhance the next highest possible position of the next car
            Car_Generator.next_higher_car += 4f;
        }
        //otherwise it must be a higher lane
        else
        {
            //has to be decremented bc we move downwards
            Car_Generator.next_higher_car -= 4f;
            //save the new position as the default position
            org_position = new Vector3(x, transform.position.y, Car_Generator.next_lower_car);
            //move the car to the new position
            transform.position = new Vector3(x, transform.position.y, Car_Generator.next_lower_car);
            //decrement the next lowest possible position of the next car
            Car_Generator.next_lower_car -= 4f;
        }

        
    }

    private void changeColor()
    {
        //Allow Resource.LoadAll to just return a list of objects (filtered to only have materials in)
        UnityEngine.Object[] carColors = Resources.LoadAll("carMaterials", typeof(Material));

        //Randomly choose material for car
        transform.GetChild(0).GetComponent<Renderer>().material = (Material)carColors[UnityEngine.Random.Range(0, carColors.Length)];
        Debug.Log("Changed color!");
    }
}
