using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    //public vars
    public float move_speed = 7f;
    public float turn_speed = 100f;

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
        turn();

        //update z Position
        zPosition = transform.position.z;
    }

    private void movement()
    {
        //calculate the vector for movement
        Vector3 movementV = transform.forward * Input.GetAxis("Vertical") * move_speed;

        //move the rigidbody
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0) + movementV;
    }

    private void turn()
    {
        //rotate rigidbody
        transform.Rotate(0, Input.GetAxis("Horizontal") * turn_speed * Time.deltaTime, 0);
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
