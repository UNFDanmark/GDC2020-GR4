using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Script : MonoBehaviour
{
    public int car_speed_max = 12;
    public int car_speed_min = 5;

    private int dir = 0;//0 right, 1 left
    private int car_speed;

    // Start is called before the first frame update
    void Start()
    {
        car_speed = Random.Range(car_speed_min, car_speed_max + 1);
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
