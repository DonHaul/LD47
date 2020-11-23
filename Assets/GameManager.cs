using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int lives = 1;
    public int lapCount = 0;

    public string[] deathQuotes;

    public List<Vector3> LapPositions;

    public GameObject player;

    public float captureInterval = 0.02f;

    public bool capturing = true;

    public GameObject[] obstacles;
    public bool[] obstaclesRotate;

    public List<GameObject> obstaclesinstantiated;

    public Text scoresText;
    public Text playersText;
    public InputField playerNameInput;



    string gameId = "3850037";
    bool testMode = true;

    public string playerName;

    dreamloLeaderBoard dl;
    dreamloPromoCode pc;

    List<dreamloLeaderBoard.Score> scoreList;

    //0 initial
    //1 play
    //2 end
    public int gameState = 0;

    public float playerSpeed = 17;


    public static GameManager instance;
    void Start()
    {
        dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();

            

        instance = this;

        Player.instance.forwardspeed = 0;
        if (Advertisement.isInitialized == false)
        {
            Advertisement.Initialize(gameId, testMode);

        }
        


        obstaclesinstantiated = new List<GameObject>();

        StartCoroutine(Countdown());
    }

    public void ShowInterstitialAd()
    {
        if(Advertisement.isInitialized==false)
        {
            Advertisement.Initialize(gameId, testMode);

        }
        else   if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }
    IEnumerator Countdown()
    {
        float sec = 0.6f;
        GenerateObstacles();

        UImanager.instance.SetCountDownTxt("3");
        AudioManager.instance.PlaySound("321");
        yield return new WaitForSeconds(sec);

        UImanager.instance.SetCountDownTxt("2");
        AudioManager.instance.PlaySound("321");
        yield return new WaitForSeconds(sec);

        UImanager.instance.SetCountDownTxt("1");
        AudioManager.instance.PlaySound("321");
        yield return new WaitForSeconds(sec);


        UImanager.instance.SetCountDownTxt("GO!");
        AudioManager.instance.PlaySound("go");
        yield return new WaitForSeconds(sec);
        UImanager.instance.SetCountDownTxt("");
        StartGame();
    }

    // Update is called once per frame

    IEnumerator CapturePos()
    {
        while(true)
        {
            if(capturing)
            { 
            LapPositions.Add(player.transform.position);
            }
            yield return new WaitForSeconds(captureInterval);
        }
    }

    public void StartGame()
    {
        Player.instance.forwardspeed = playerSpeed;

        gameState = 1;

        StartCoroutine(CapturePos());

        
    }
        
    public void GenerateObstacles()
    {

        //increase range with speed
        float spaciness = 1+lapCount/2;// Mathf.Max(Player.instance.forwardspeed / playerSpeed * 1,1);


        for (float i = 20; i < 200; i+= Random.Range(10f,20f)* spaciness)
        {

            int pick = Random.Range(0, obstacles.Length);

            GameObject go = Instantiate(obstacles[pick]);

            go.transform.position = new Vector3(Random.Range(-13f, 13f), Random.Range(-0f, 5f), i);

            if(obstaclesRotate[pick])
            {
                go.transform.rotation = Random.rotation;
            }

            obstaclesinstantiated.Add(go);

        }
    }

    public void Lap()
    {

        LapPositions.Clear();
        lapCount++;
        capturing = false;

        for (int i = 0; i < obstaclesinstantiated.Count; i++)
        {
            Destroy(obstaclesinstantiated[i]);
        }

        obstaclesinstantiated.Clear();

        GenerateObstacles();
    }

    public void Death()
    {

               

        lives--;
        if(lives<=0)
        {
            GameOver();
        }
    }


    public void SubmitScore()
    {
        dl.AddScore(playerNameInput.text, lapCount);
    }

    public void GameOver()
    {
        if (gameState == 2) return;

        if(lapCount>PlayerPrefs.GetInt("highscore",0))
        {
            PlayerPrefs.SetInt("highscore", lapCount);
            UImanager.instance.SetHighscore("New Highscore: " + lapCount.ToString());

        }
        else
        {
            UImanager.instance.SetHighscore("Highscore: " + PlayerPrefs.GetInt("highscore"));
        }


        if(lapCount>1)
        {
            Debug.Log("SHOWADS");
            ShowInterstitialAd();
        }

        AudioManager.instance.PlaySound("death");
        gameState = 2;
        capturing = false;
        AudioManager.instance.GetComponent<AudioSource>().volume = 1;
        AudioManager.instance.GetComponent<AudioSource>().pitch = 2.5f;
        UImanager.instance.BtnPanel.SetActive(true);
        UImanager.instance.SetDeathQuote(deathQuotes[Random.Range(0, deathQuotes.Length)]);

        //highscore stuff


        // get the reference here...

 

        // add the score...
        if (dl.publicCode == "") Debug.LogError("You forgot to set the publicCode variable");
        if (dl.privateCode == "") Debug.LogError("You forgot to set the privateCode variable");

         

        string players="";
        string scores="";

        bool playerintop10=false;


        scoreList = dl.ToListHighToLow() ;
        if (scoreList == null)
                {
                    
                }
                else
                {
                    int maxToDisplay = 10;
                    int count = 0;
                    foreach (dreamloLeaderBoard.Score currentScore in scoreList)
                    {
                        count++;
                        players = players + count.ToString() + "." + currentScore.playerName+ "\n";
                        scores = scores + currentScore.score.ToString() + "\n";

                        if(currentScore.playerName==playerName)
                {
                    playerintop10 = true;
                }

          
                        if (count >= maxToDisplay) break;
                    }
                }

                 if(playerintop10==false)
        {
            players += "...\n";
            scores += "...\n";

            players += playerName+"\n";
            scores += lapCount + "\n";

        }


        playersText.text = players;
        scoresText.text = scores;

    }

}
