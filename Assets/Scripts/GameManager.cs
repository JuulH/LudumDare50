using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using Player;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    private PlayerAttack playerAttack;
    private PlayerMovementController playerMovement;
    private PlayerHealth playerHealth;

    public GameObject house;
    private HouseHealth houseHealth;

    public static int score;
    public static int coins;
    public TMP_Text coinsText;
    public TMP_Text scoreText;

    public GameObject intermissionCanvas;
    public TMP_Text overviewText;
    private float elapsedTime;
    private float mins;
    private float secs;
    private string overviewTime;
    private string infoText;

    [SerializeField] private Button weaponUpgradeButton;
    [SerializeField] private Button speedUpgradeButton;
    [SerializeField] private Button fortifyUpgradeButton;
    private int weaponUpgrades = 0;
    private int speedUpgrades = 0;
    private int fortifyUpgrades = 0;
    [SerializeField] private int maxWeaponUpgrades = 3;
    [SerializeField] private int maxSpeedUpgrades = 3;
    [SerializeField] private int maxFortifyUpgrades = 3;

    // Start is called before the first frame update
    void Start()
    {
        playerAttack = player.GetComponent<PlayerAttack>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovementController>();
        houseHealth = house.GetComponent<HouseHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        mins = Mathf.Floor(elapsedTime / 60);
        secs = Mathf.RoundToInt(elapsedTime - mins * 60);
        overviewTime = string.Format("{0:00}:{1:00}", mins, secs);
        coinsText.SetText("$" + coins);
        scoreText.SetText(score.ToString());
        infoText = string.Format("Coins: {0}\nScore: {1}\nTime: {2}", coins.ToString(), score.ToString(), overviewTime);
        overviewText.SetText(infoText);
    }

    public void SpeedUpgrade()
    {
        if (speedUpgrades < maxSpeedUpgrades)
        {
            playerMovement.speed += 1;
        }
        speedUpgrades += 1;
        if (speedUpgrades >= maxSpeedUpgrades)
        {
            speedUpgradeButton.interactable = false;
        }
    }

    public void GunUpgrade()
    {
        if(weaponUpgrades < maxWeaponUpgrades)
        {
            playerAttack.attackCooldown -= .05f;
        }
        weaponUpgrades += 1;
        if(weaponUpgrades >= maxWeaponUpgrades)
        {
            weaponUpgradeButton.interactable = false;
        }
    }

    public void FortifyUpgrade()
    {
        if (fortifyUpgrades < maxFortifyUpgrades)
        {
            houseHealth.maxHealth += 100f;
        }
        fortifyUpgrades += 1;
        if (fortifyUpgrades >= maxFortifyUpgrades)
        {
            fortifyUpgradeButton.interactable = false;
        }
    }

    public void WaveComplete()
    {
        playerAttack.isControlsEnabled = false;
        Time.timeScale = 0f;
        intermissionCanvas.SetActive(true);
    }

    public void NextWave()
    {
        playerAttack.isControlsEnabled = true;
        Time.timeScale = 1f;
        elapsedTime = 0f;
        houseHealth.ResetHealth();
        playerHealth.ResetHealth();
        houseHealth.updateHealthBar();
        intermissionCanvas.SetActive(false);
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
