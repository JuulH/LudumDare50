using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int score;
    public static int coins;
    public TMP_Text coinsText;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.SetText("$" + coins);
        scoreText.SetText(score.ToString());
    }

    public static void GameOver()
    {
        GameManager.score = 0;
        GameManager.coins = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void AddCoin()
    {
        coins += 1;
        Debug.Log(coins);
    }

    public static void AddScore(int points)
    {
        score += points;
        Debug.Log(score);
    }
}
