using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Script : MonoBehaviour
{
    public float car_speed = 8f;

    private int dir = 0;//0 right, 1 left

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(car_speed * Time.deltaTime, 0, 0);
    }
        
}
