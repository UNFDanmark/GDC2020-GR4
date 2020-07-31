using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class Dog_Script : MonoBehaviour
{
    //var for the speed of the dog
    public float dog_speed = 5;
    //vars for determening the rest-duration of the dog
    public int rest_time_lower_bound = 0;
    public int rest_time_upper_bound = 50;
    //the distance of the dog to the person when carried
    public float carry_distance = 2;
    //the time the dog likes to be held
    public int hold_time_in_ticks = 100;
    //the barks
    public AudioClip barkOne;
    public AudioClip barkTwo;
    public AudioClip barkThree;
    public AudioClip barkFour;

    //var for keeping track whether the dog is beeing held
    private bool held = false;
    //the time the dog restst
    private int rest_time = 0;
    //the dogs rotaion value
    private int rotation_value = 0;
    //the dogs rigidbody
    private Rigidbody rigidbody;
    //var for saving the current holder of the dog
    private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        //randomly generate the time the dog rest by the values from above
        rest_time = Random.Range(rest_time_lower_bound, rest_time_upper_bound);
        //saving the rigidbody
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //if the dogs rest time is over and it is not held
        if (rest_time == 0 && !held)
        {
            //it will move
            move(false);
        }
        //otherwise if it is neither held
        else if (!held)
        {
            //the rest duration will be decremented by one, every tick
            rest_time--;
        }
        //otherwise if it is beeing held
        else if (held)
        {
            //it will be carried
            carry();
            //and the duration of the hold will be decremented
            hold_time_in_ticks--;
        }
        //if the dog is beeing held BUT the time of holding is over
        if (held && hold_time_in_ticks == 0)
        {
            //it will be droped
            drop();
            //and the time will be reset
            //------!-Hardcode-!-------//                          //------!-Hardcode-!-------//
            hold_time_in_ticks = 100;
        }

        //randomly determine whether bark should be played
        int rndIf = Random.Range(1, 31);
        if (rndIf == 1)
        {
            //randomly choose one of the four barks
            AudioClip clip = barkOne;
            int rndBark = Random.Range(1, 5);
            switch (rndBark)
            {
                case 1:
                    clip = barkOne;
                    break;
                case 2:
                    clip = barkTwo;
                    break;
                case 3:
                    clip = barkThree;
                    break;
                case 4:
                    clip = barkFour;
                    break;
            }
            //play the bark
            GetComponent<AudioSource>().PlayOneShot(clip);
        }

    }

    //is called when the dog collides with something
    void OnCollisionEnter(Collision collision)
    {
        //if it collided with a wall
        if (collision.gameObject.tag == "WALL")
        {
            //the current rotation value will be saved
            float current_rotation = transform.rotation.y;
            //and if it is turned above 90 degrees
            if (current_rotation > 90)
            {
                //its value will be decremented by 90
                transform.Rotate(0, transform.rotation.y - 90, 0);
            }
            else
            {
                //otherwise it wille be incremented by 90
                transform.Rotate(0, transform.rotation.y + 90, 0);
            }
        }
        //if it collided with the players hand
        else if (collision.gameObject.tag == "HAND")
        {
            //it will be set to kinimatic, sothat it will not be affected by physics anymore
            rigidbody.isKinematic = true;

            //the player which picked it up will be saved
            player = collision.gameObject;

            //and the private var of whether it is held or not will be set to true
            held = true;
        }
        //if a car hits the dog
        else if (collision.gameObject.tag == "CAR")
        {
            //kill the game
            kill();
        }
    }

    //method for randomly making the dog move
    private void move(bool afterDrop)
    {
        //the rotaion and resttime will be reset to a random value
        rest_time = Random.Range(rest_time_lower_bound, rest_time_upper_bound);

        //onlz rotate if the dog has not just been droped
        if (!afterDrop)
        {
            //randomly determine the rotation value
            rotation_value = Random.Range(0, 361);

            //it will rotate to the value determined
            transform.rotation = Quaternion.Euler(0, rotation_value, 0);
        }

        //calculate the vector for movement
        Vector3 movement = transform.forward * dog_speed;
        //move the rigidbody
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0) + movement;
    }

    //method for carrying the dog
    private void carry()
    {
        //check wether the carryer is not null
        if (player != null)
        {
            //transform the dogs position to the right side of the player
            transform.position = player.transform.position + player.transform.right * carry_distance;
        }
    }

    //method for dropping the dog
    private void drop()
    {
        //reset the rigidbody to kinematic sothat it can be hit by cars again
        rigidbody.isKinematic = false;

        //set the player to null
        player = null;

        //reset the holding var
        held = false;

        //decide in which direction the dog will run of to
        int direction = Random.Range(1, 4);
        float y = 0;
        switch (direction)
        {
            case 1:
                //down
                y = 180;
                break;
            case 2:
                //right
                y = 90;
                break;
            case 3:
                //up
                y = 0;
                break;
        }
        //!not to the left, bc player is to the left and would pick it up instantly

        //turn the dog in the propper position
        transform.rotation = Quaternion.Euler(0, y, 0);

        //lift it abit bc otherwise it would fall back to 0 thus beeing stuck in the road
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

        //call move, without turning the dog again
        move(true);

        //set the resttime to 0 sothat it will move as soon as it is dropped
        rest_time = 100;
    }

    void OnBecameInvisible()
    {
        //if the dog get out of the screen the game will restart
        kill();
    }

    private void kill()
    {
        //play the death sound
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        //the game will be restarted
        FindObjectOfType<Game_Manager>().restartGame();
    }
}
