using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int score;
    public static int coins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
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
