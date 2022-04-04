using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using Player;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

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
    public TMP_Text waveNumberText;
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

    [SerializeField] private int weaponUpgradeCost = 1;
    [SerializeField] private int speedUpgradeCost = 1;
    [SerializeField] private int fortifyUpgradeCost = 1;

    [SerializeField] private UpgradeCost weaponUpgradeCostContainer;
    [SerializeField] private UpgradeCost speedUpgradeCostContainer;
    [SerializeField] private UpgradeCost fortifyUpgradeCostContainer;

    void Start()
    {
        playerAttack = player.GetComponent<PlayerAttack>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovementController>();
        houseHealth = house.GetComponent<HouseHealth>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        mins = Mathf.Floor(elapsedTime / 60);
        secs = Mathf.RoundToInt(elapsedTime - mins * 60);
        overviewTime = string.Format("{0:00}:{1:00}", mins, secs);
        coinsText.SetText("$" + coins);
        scoreText.SetText(score.ToString());
        infoText = string.Format("Coins: {0}\nScore: {1}\nTime: {2}", coins.ToString(), score.ToString(),
            overviewTime);
        overviewText.SetText(infoText);
    }

    public void SpeedUpgrade()
    {
        if (coins < speedUpgradeCostContainer.Cost()) return;
        if (speedUpgrades < maxSpeedUpgrades)
        {
            playerMovement.speed += 1;
            RemoveCoins(speedUpgradeCostContainer.Cost());
        }

        speedUpgrades += 1;
        UpdateUpgradeCosts();
    }

    public void GunUpgrade()
    {
        if (coins < weaponUpgradeCostContainer.Cost()) return;
        if (weaponUpgrades < maxWeaponUpgrades)
        {
            playerAttack.attackCooldown -= .05f;
            RemoveCoins(weaponUpgradeCostContainer.Cost());
        }

        weaponUpgrades += 1;
        UpdateUpgradeCosts();
    }

    public void FortifyUpgrade()
    {
        if (coins < fortifyUpgradeCostContainer.Cost()) return;
        if (fortifyUpgrades < maxFortifyUpgrades)
        {
            RemoveCoins(fortifyUpgradeCostContainer.Cost());
            houseHealth.maxHealth += 100f;
        }

        fortifyUpgrades += 1;
        UpdateUpgradeCosts();
    }

    public void WaveComplete(int waveNumCompleted)
    {
        playerAttack.isControlsEnabled = false;
        Time.timeScale = 0f;
        waveNumberText.text = "" + waveNumCompleted;
        UpdateUpgradeCosts();
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

    private void UpdateUpgradeCosts()
    {
        UpdateWeaponUpgradeCost();
        UpdateSpeedUpgradeCost();
        UpdateFortifyUpgradeCost();
    }

    private void UpdateFortifyUpgradeCost()
    {
        fortifyUpgradeCostContainer.SetCost((fortifyUpgrades + 1) * fortifyUpgradeCost);
        if (fortifyUpgrades >= maxFortifyUpgrades || coins < fortifyUpgradeCostContainer.Cost())
        {
            fortifyUpgradeButton.interactable = false;
        }
        else
        {
            fortifyUpgradeButton.interactable = true;
        }
    }

    private void UpdateSpeedUpgradeCost()
    {
        speedUpgradeCostContainer.SetCost((speedUpgrades + 1) * speedUpgradeCost);
        if (speedUpgrades >= maxSpeedUpgrades || coins < speedUpgradeCostContainer.Cost())
        {
            speedUpgradeButton.interactable = false;
        }
        else
        {
            speedUpgradeButton.interactable = true;

        }
    }

    private void UpdateWeaponUpgradeCost()
    {
        weaponUpgradeCostContainer.SetCost((weaponUpgrades + 1) * weaponUpgradeCost);
        if (weaponUpgrades >= maxWeaponUpgrades || coins < weaponUpgradeCostContainer.Cost())
        {
            weaponUpgradeButton.interactable = false;
        }
        else
        {
            weaponUpgradeButton.interactable = true;
        }
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
        Debug.Log("Coins : " + coins);
    }

    public static void RemoveCoins(int amountToRemove)
    {
        coins -= amountToRemove;
        Debug.Log("Coins after removal: " + coins);
    }

    public static void AddScore(int points)
    {
        score += points;
        Debug.Log("Score " + score);
    }
}