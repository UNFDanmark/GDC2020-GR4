using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public float move_speed = 10f;

    public Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        movement();
    }

    private void movement()
    {
        Vector3 movement = transform.forward * Input.GetAxis("Vertical") * move_speed * -1;

        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0) + movement;
    }
}
