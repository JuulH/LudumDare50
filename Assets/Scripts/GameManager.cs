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
    private PlayerAttack _playerAttack;
    private PlayerMovementController _playerMovement;
    private PlayerHealth _playerHealth;

    public GameObject house;
    private HouseHealth _houseHealth;

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
    private int _weaponUpgrades;
    private int _speedUpgrades;
    private int _fortifyUpgrades;
    [SerializeField] private int maxWeaponUpgrades = 3;
    [SerializeField] private int maxSpeedUpgrades = 3;
    [SerializeField] private int maxFortifyUpgrades = 3;

    [SerializeField] private int weaponUpgradeCost = 1;
    [SerializeField] private int speedUpgradeCost = 1;
    [SerializeField] private int fortifyUpgradeCost = 1;

    [SerializeField] private UpgradeCost weaponUpgradeCostContainer;
    [SerializeField] private UpgradeCost speedUpgradeCostContainer;
    [SerializeField] private UpgradeCost fortifyUpgradeCostContainer;
    
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject startMenuCanvas;
    [SerializeField] private GameObject gameOverMenuCanvas;

    void Start()
    {
        _playerAttack = player.GetComponent<PlayerAttack>();
        _playerHealth = player.GetComponent<PlayerHealth>();
        _playerMovement = player.GetComponent<PlayerMovementController>();
        _houseHealth = house.GetComponent<HouseHealth>();
        Time.timeScale = 0f;
        SetPlayerControlsEnabled(false);
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

    public void StartGame()
    {
        inGameCanvas.SetActive(true);
        startMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        SetPlayerControlsEnabled(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        GameManager.score = 0;
        GameManager.coins = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetPlayerControlsEnabled(bool isEnabled)
    {
        _playerMovement.isControlsEnabled = isEnabled;
        _playerAttack.isControlsEnabled = isEnabled;
        _playerMovement.enabled = isEnabled;
        _playerAttack.enabled = isEnabled;
    }

    public void SpeedUpgrade()
    {
        if (coins < speedUpgradeCostContainer.Cost()) return;
        if (_speedUpgrades < maxSpeedUpgrades)
        {
            _playerMovement.speed += 1;
            RemoveCoins(speedUpgradeCostContainer.Cost());
        }

        _speedUpgrades += 1;
        UpdateUpgradeCosts();
    }

    public void GunUpgrade()
    {
        if (coins < weaponUpgradeCostContainer.Cost()) return;
        if (_weaponUpgrades < maxWeaponUpgrades)
        {
            _playerAttack.attackCooldown -= .05f;
            RemoveCoins(weaponUpgradeCostContainer.Cost());
        }

        _weaponUpgrades += 1;
        UpdateUpgradeCosts();
    }

    public void FortifyUpgrade()
    {
        if (coins < fortifyUpgradeCostContainer.Cost()) return;
        if (_fortifyUpgrades < maxFortifyUpgrades)
        {
            _houseHealth.maxHealth += 100f;
            RemoveCoins(fortifyUpgradeCostContainer.Cost());
        }

        _fortifyUpgrades += 1;
        UpdateUpgradeCosts();
    }

    public void WaveComplete(int waveNumCompleted)
    {
        _playerAttack.isControlsEnabled = false;
        Time.timeScale = 0f;
        waveNumberText.text = "" + waveNumCompleted;
        UpdateUpgradeCosts();
        intermissionCanvas.SetActive(true);
    }

    public void NextWave()
    {
        _playerAttack.isControlsEnabled = true;
        Time.timeScale = 1f;
        elapsedTime = 0f;
        _houseHealth.ResetHealth();
        _playerHealth.ResetHealth();
        _houseHealth.updateHealthBar();
        intermissionCanvas.SetActive(false);
    }

    private void UpdateUpgradeCosts()
    {
        UpdateUpgradeCostFor(weaponUpgradeCostContainer, _weaponUpgrades, weaponUpgradeCost, maxWeaponUpgrades, weaponUpgradeButton);
        UpdateUpgradeCostFor(fortifyUpgradeCostContainer, _fortifyUpgrades, fortifyUpgradeCost, maxFortifyUpgrades, fortifyUpgradeButton);
        UpdateUpgradeCostFor(speedUpgradeCostContainer, _speedUpgrades, speedUpgradeCost, maxSpeedUpgrades, speedUpgradeButton);
    }

    private void UpdateUpgradeCostFor(UpgradeCost upgradeCostContainer, int upgradesHappened, int upgradeCost, 
        int maxUpgrades, Button upgradeButton) 
    {
        upgradeCostContainer.SetCost((upgradesHappened + 1) * upgradeCost);
        if (upgradesHappened >= maxUpgrades)
        {
            upgradeCostContainer.SetMaxUpgradeLimitReached();
            upgradeButton.interactable = false;
        }
        else if (coins < upgradeCostContainer.Cost())
        {
            upgradeButton.interactable = false;

        }
        else
        {
            upgradeButton.interactable = true;
        }
    }

    public void GameOver()
    {
        inGameCanvas.SetActive(false);
        gameOverMenuCanvas.SetActive(true);
        SetPlayerControlsEnabled(false);
        Time.timeScale = 0;
    }

    // private IEnumerator StartGameOver()
    // {
    //     yield return new WaitForSeconds()
    // }

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