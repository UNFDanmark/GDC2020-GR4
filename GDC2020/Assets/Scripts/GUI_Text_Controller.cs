using UnityEngine;
using UnityEngine.UI;

public class GUI_Text_Controller : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //var for saving time
        float time = Time.timeSinceLevelLoad;
        //set time on ui
        GetComponent<Text>().text = "Time: " + time + " s";
    }
}
