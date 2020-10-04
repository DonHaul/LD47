using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int lives = 1;
    public int lapCount = 0;

    public List<Vector3> LapPositions;

    public GameObject player;

    float captureInterval = 0.02f;

    //0 initial
    //1 play
    public int gameState = 0;

    public float playerSpeed = 17;


    public static GameManager instance;
    void Start()
    {
        instance = this;

        Player.instance.forwardspeed = 0;

        StartCoroutine(CapturePos());

        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        float sec = 0.6f;

        UImanager.instance.SetCountDownTxt("3");
        yield return new WaitForSeconds(sec);

        UImanager.instance.SetCountDownTxt("2");
        yield return new WaitForSeconds(sec);

        UImanager.instance.SetCountDownTxt("1");
        yield return new WaitForSeconds(sec);


        UImanager.instance.SetCountDownTxt("GO!");
        yield return new WaitForSeconds(sec);
        UImanager.instance.SetCountDownTxt("");
        StartGame();
    }

    // Update is called once per frame

    IEnumerator CapturePos()
    {
        while(true)
        {
            LapPositions.Add(player.transform.position);
            yield return new WaitForSeconds(captureInterval);
        }
    }

    public void StartGame()
    {
        Player.instance.forwardspeed = playerSpeed;

        gameState = 1;
    }
        

    public void Death()
    {

        Debug.LogWarning("Death");

        lives--;
        if(lives<=0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.LogWarning("GameOver");
    }
}
