using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    //car for checking if it has ended yet
    private bool hasEnded = false;

    //delay after hit
    public float delay = 1f;

    //called on start
    void Start()
    {
        //divide by tow bc time is also devided by two
        delay /= 2;
    }

    //restart the game
    public void restartGame()
    {
        //if it has not ended yet
        if (!hasEnded)
        {
            //set it to an end
            hasEnded = true;
            //wait the delay before restart
            Invoke("restart", delay);
            //but during this time slow the game down
            Time.timeScale = 0.5f;
        }
    }

    //reload the scene and therwith the game
    private void restart()
    {
        //reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //set the timescale back to one
        Time.timeScale = 1f;
    }
}
