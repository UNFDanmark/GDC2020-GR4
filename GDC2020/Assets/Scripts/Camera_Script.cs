using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
        void FixedUpdate()
    {
        //call the method for movement
        movement();
    }

    private void movement()
    {
        //position the camera behind the player, while staying 13m behind
        transform.position =  new Vector3(transform.position.x, transform.position.y, player_movement.zPosition - 13);
    }
}
