using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int lives = 1;
    public int lapCount = 0;


    public static GameManager instance;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
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
