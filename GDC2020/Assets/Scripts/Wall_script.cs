using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_script : MonoBehaviour
{
    void FixedUpdate()
    {
        //call the method for movement
        movement();
    }

    private void movement()
    {
        //position the walls parallel to the player bc cars have to be able to hit it
        transform.position = new Vector3(transform.position.x, transform.position.y, player_movement.zPosition);
    }
}
