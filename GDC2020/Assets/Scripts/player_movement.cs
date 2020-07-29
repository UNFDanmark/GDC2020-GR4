using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    //var
    public float move_speed = 7f;
    public float turn_speed = 100f;

    public Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //save rigidbody within a variable
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //call the methods for movement
        movement();
        turn();
        
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
}
