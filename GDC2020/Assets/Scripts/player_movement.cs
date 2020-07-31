using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    //public vars
    public float move_speed = 7f;
    public float turn_speed = 100f;
    public float tilt_angle = 10f;

    //public static vars, required for wall and camera adjustments
    public static float zPosition = 0;

    //var for saving the rigidbody
    public Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //save rigidbody within a variable
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //call the methods for movement
        movement();

        //update z Position
        zPosition = transform.position.z;
    }

    private void movement()
    {
        //calculate the vector for movement
        Vector3 movementV = transform.forward * Input.GetAxis("Vertical") * move_speed;
        //calculate the vector for movement
        Vector3 movementH = transform.right * Input.GetAxis("Horizontal") * move_speed;

        //move the rigidbody
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0) + movementV + movementH;

        //check wether the model needs to be tilted
        checkTilt();
    }

    private void checkTilt()
    {
        //only rotate child, by manipulating the x and z angles
        transform.GetChild(1).rotation = Quaternion.Euler(Input.GetAxis("Vertical") * tilt_angle, 0, Input.GetAxis("Horizontal") * tilt_angle * -1);
    }

    void OnCollisionEnter(Collision collision)
    {
        //if a car hits the player
        if (collision.gameObject.tag == "CAR")
        {
            //the game will be restarted
            FindObjectOfType<Game_Manager>().restartGame();
        }
    }
}
