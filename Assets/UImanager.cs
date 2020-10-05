using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UImanager : MonoBehaviour
{
    public static UImanager instance;

    public GameObject BtnPanel;
    public Text ButtonTexT;

    public Text LapsText;

    public Text CountDowntext;

    public Text deathquote;

    public Text highscoreText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    public void GameStart()
    {
        BtnPanel.SetActive(false);
    }

    // Update is called once per frame
    public void GameEnd()
    {
        BtnPanel.SetActive(true);
        ButtonTexT.text = "Retry";
    }

    public void SetLaps(int laps)
    {
        LapsText.text = laps.ToString();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }

    public void SetCountDownTxt(string text)
    {
        CountDowntext.text = text;


    }

    public void SetDeathQuote(string text)
    {
        deathquote.text = text;
    }

    public void SetHighscore(string text)
    {
        highscoreText.text = text;
    }
}
