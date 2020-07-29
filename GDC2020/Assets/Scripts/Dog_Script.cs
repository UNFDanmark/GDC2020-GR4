using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dog_Script : MonoBehaviour
{

    public float dog_speed = 2;
    public int rest_time_lower_bound = 0;
    public int rest_time_upper_bound = 50;

    public static bool held = false;

    private int rest_time = 0;
    private int rotation_value = 0;
    private Rigidbody rigidbody;

    private GameObject dog = null;

    // Start is called before the first frame update
    void Start()
    {
        rest_time = Random.Range(rest_time_lower_bound, rest_time_upper_bound);
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (rest_time == 0 && !held)
        {
            rest_time = Random.Range(rest_time_lower_bound, rest_time_upper_bound);
            rotation_value = Random.Range(0, 361);

            transform.Rotate(0, rotation_value, 0);

            //calculate the vector for movement
            Vector3 movement = transform.forward * dog_speed;
            //move the rigidbody
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0) + movement;

            rest_time = Random.Range(rest_time_lower_bound, rest_time_upper_bound);
        }
        else if (!held)
        {
            rest_time--;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        float current_rotation = transform.rotation.y;
        if (collision.gameObject.tag == "WALL")
        {
            if (current_rotation > 90)
            {
                transform.Rotate(0, transform.rotation.y - 90, 0);
            }
            else
            {
                transform.Rotate(0, transform.rotation.y + 90, 0);
            }
        }
        else if (collision.gameObject.tag == "HAND")
        {
            dog = collision.gameObject;
            float differnece = current_rotation - 90;
            transform.Rotate(new Vector3(0, differnece, 0));
            dog.GetComponent<Rigidbody>().velocity = Vector3.zero;
            dog.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.SetParent(dog.transform);
            dog.transform.position = transform.position;

            held = true;
        }
    }
}
