using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityScript.Steps;

public class UImanager : MonoBehaviour
{
    public static UImanager instance;

    public GameObject BtnPanel;
    public Text ButtonTexT;

    public Text LapsText;

    public Text CountDowntext;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    public void GameStart()
    {
        BtnPanel.active = false;
    }

    // Update is called once per frame
    public void GameEnd()
    {
        BtnPanel.active = true;
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
}
