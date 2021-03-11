using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score, record;

    public Text currScore;
    public Text bestScore;
    public static GameManager singleton;
    public Button gameOverButton;
    public int sound = 1;
    public int playable = 1;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
        record = PlayerPrefs.GetInt("Highscore");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currScore.text = "SCORE\n" + score;
        bestScore.text = "RECORD\n" + record;
        
    }

    public void Restart()
    {
        FindObjectOfType<HelixController>().LoadStage();
        FindObjectOfType<BallController>().ResetBall();
        
        playable = 1;
        gameOverButton.gameObject.SetActive(false);
        singleton.score = 0;
    }
    public void GameOverPrikaz()
    {
        gameOverButton.gameObject.SetActive(true);
        gameOverButton.GetComponentInChildren<Text>().text = "SCORE\n" + score;
        playable = 0;

    }

    public void NextStage()
    {
        FindObjectOfType<HelixController>().LoadStage();
        FindObjectOfType<BallController>().ResetBall();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if (score > record)
        {
            PlayerPrefs.SetInt("Highscore", score);
            record = score;
        }
    }
}
