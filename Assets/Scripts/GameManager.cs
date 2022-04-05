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

    [SerializeField] private Vector3 _playerStartPos;
    [SerializeField] private GameObject _playerGun;
    [SerializeField] private SpriteRenderer activeGunSprite;
    [SerializeField] private Sprite[] weaponSprites;
    [SerializeField] private float[] weaponCooldowns;

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
    [SerializeField] private Button repairUpgradeButton;
    [SerializeField] private Button recoverUpgradeButton;
    private int _weaponUpgrades;
    private int _speedUpgrades;
    private int _fortifyUpgrades;
    private int _repairUpgrades;
    private int _recoverUpgrades;
    [SerializeField] private int maxWeaponUpgrades = 3;
    [SerializeField] private int maxSpeedUpgrades = 3;
    [SerializeField] private int maxFortifyUpgrades = 3;
    [SerializeField] private int maxRecoverUpgrades = 999;
    [SerializeField] private int maxRepairUpgrades = 999;

    [SerializeField] private int weaponUpgradeCost = 1;
    [SerializeField] private int speedUpgradeCost = 1;
    [SerializeField] private int fortifyUpgradeCost = 1;
    [SerializeField] private int repairUpgradeCost = 1;
    [SerializeField] private int recoverUpgradeCost = 1;

    [SerializeField] private UpgradeCost weaponUpgradeCostContainer;
    [SerializeField] private UpgradeCost speedUpgradeCostContainer;
    [SerializeField] private UpgradeCost fortifyUpgradeCostContainer;
    [SerializeField] private UpgradeCost repairUpgradeCostContainer;
    [SerializeField] private UpgradeCost recoverUpgradeCostContainer;
    
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject startMenuCanvas;
    [SerializeField] private GameObject gameOverMenuCanvas;

    [SerializeField] private SoundManager soundManager;
    
    [SerializeField] private TMP_Text gameOverOverviewText;
    [SerializeField] private GameObject playerHealthBar;
    [SerializeField] private GameObject houseHealthBar;
    [SerializeField] private GameObject cinemachineStartCamera;

    private float _totalTimeElapsed;
    private int wavesCompleted = 0;


    void Start()
    {
        _playerAttack = player.GetComponent<PlayerAttack>();
        _playerHealth = player.GetComponent<PlayerHealth>();
        _playerMovement = player.GetComponent<PlayerMovementController>();
        _houseHealth = house.GetComponent<HouseHealth>();
        Time.timeScale = 0f;
        SetPlayerControlsEnabled(false);
        soundManager.PlayMenuMusic();
        _totalTimeElapsed = 0;
    }

    void Update()
    {
        _totalTimeElapsed += Time.deltaTime;
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
        _playerGun.SetActive(true);
        player.transform.position = _playerStartPos;
        inGameCanvas.SetActive(true);
        startMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        SetPlayerControlsEnabled(true);
        soundManager.PlayGameMusic();
        cinemachineStartCamera.SetActive(false);
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
        soundManager.PlayerUpgradeBuy();
        if (_speedUpgrades < maxSpeedUpgrades)
        {
            _playerMovement.speed += 0.5f;
            RemoveCoins(speedUpgradeCostContainer.Cost());
        }

        _speedUpgrades += 1;
        UpdateUpgradeCosts();
    }

    public void GunUpgrade()
    {
        if (coins < weaponUpgradeCostContainer.Cost()) return;
        soundManager.PlayerUpgradeBuy();
        if (_weaponUpgrades < maxWeaponUpgrades)
        {
            _playerAttack.attackCooldown = weaponCooldowns[_weaponUpgrades + 1];
            RemoveCoins(weaponUpgradeCostContainer.Cost());
            _weaponUpgrades += 1;
            activeGunSprite.sprite = weaponSprites[_weaponUpgrades];
            if (activeGunSprite.sprite == weaponSprites[weaponSprites.Length - 1])
            {
                _playerAttack.SwitchProjectileSpawnToMiniGun();
            }
        }

        UpdateUpgradeCosts();
    }

    public void FortifyUpgrade()
    {
        if (coins < fortifyUpgradeCostContainer.Cost()) return;
        soundManager.PlayerUpgradeBuy();
        if (_fortifyUpgrades < maxFortifyUpgrades)
        {
            _houseHealth.maxHealth += 50f;
            _houseHealth.RepairHealth(50);
            _houseHealth.UpdateHealthBar();
            _houseHealth.IncreaseHealthBarSize(0.1f);
            RemoveCoins(fortifyUpgradeCostContainer.Cost());
        }

        _fortifyUpgrades += 1;
        UpdateUpgradeCosts();
    }
    
    public void RepairUpgrade()
    {
        if (coins < repairUpgradeCostContainer.Cost()) return;
        soundManager.PlayerUpgradeBuy();
        if (_repairUpgrades < maxRepairUpgrades)
        {
            _houseHealth.RepairHealth(25);
            RemoveCoins(repairUpgradeCostContainer.Cost());
        }

        _repairUpgrades += 1;
        UpdateUpgradeCosts();
    }
    
    public void RecoverUpgrade()
    {
        if (coins < recoverUpgradeCostContainer.Cost()) return;
        soundManager.PlayerUpgradeBuy();
        if (_recoverUpgrades < maxRepairUpgrades)
        {
            _playerHealth.RecoverHealth(25);
            RemoveCoins(recoverUpgradeCostContainer.Cost());
        }

        _recoverUpgrades += 1;
        UpdateUpgradeCosts();
    }

    public void WaveComplete(int waveNumCompleted)
    {
        _playerAttack.isControlsEnabled = false;
        Time.timeScale = 0f;
        waveNumberText.text = "Wave " + waveNumCompleted;
        UpdateUpgradeCosts();
        inGameCanvas.SetActive(false);
        playerHealthBar.transform.SetParent(intermissionCanvas.transform, false);
        houseHealthBar.transform.SetParent(intermissionCanvas.transform, false);
        intermissionCanvas.SetActive(true);
        wavesCompleted++;
    }

    public void NextWave()
    {
        _playerAttack.isControlsEnabled = true;
        Time.timeScale = 1f;
        elapsedTime = 0f;
        inGameCanvas.SetActive(true);
        playerHealthBar.transform.SetParent(inGameCanvas.transform, false);
        houseHealthBar.transform.SetParent(inGameCanvas.transform, false);
        intermissionCanvas.SetActive(false);
    }

    private void UpdateUpgradeCosts()
    {
        UpdateUpgradeCostFor(weaponUpgradeCostContainer, _weaponUpgrades, weaponUpgradeCost, maxWeaponUpgrades, weaponUpgradeButton);
        UpdateUpgradeCostFor(fortifyUpgradeCostContainer, _fortifyUpgrades, fortifyUpgradeCost, maxFortifyUpgrades, fortifyUpgradeButton);
        UpdateUpgradeCostFor(speedUpgradeCostContainer, _speedUpgrades, speedUpgradeCost, maxSpeedUpgrades, speedUpgradeButton);
        UpdateUpgradeCostFor(repairUpgradeCostContainer, _repairUpgrades, repairUpgradeCost, maxRepairUpgrades, repairUpgradeButton);
        UpdateUpgradeCostFor(recoverUpgradeCostContainer, _recoverUpgrades, recoverUpgradeCost, maxRecoverUpgrades, recoverUpgradeButton);
        DisableRepairUpgradeIfFullHouseHp();
        DisableRecoverUpgradeIfFullPlayerHp();
    }

    private void DisableRecoverUpgradeIfFullPlayerHp()
    {
        if (_houseHealth._currentHealth >= _houseHealth.maxHealth)
        {
            repairUpgradeButton.interactable = false;
        }
    }

    private void DisableRepairUpgradeIfFullHouseHp()
    {
        if (_playerHealth._currentHealth >= _playerHealth.maxHealth)
        {
            recoverUpgradeButton.interactable = false;
        }
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
        soundManager.StopGameMusic();
        soundManager.PlayGameoverSound();
        
        var totalMins = Mathf.Floor(_totalTimeElapsed / 60);
        var totalSecs = Mathf.RoundToInt(_totalTimeElapsed - mins * 60);
        string totalTime = string.Format("{0:00}:{1:00}", totalMins, totalSecs);
        gameOverOverviewText.text = string.Format("Waves: {0}\nScore: {1}\nTime: {2}", wavesCompleted.ToString(), score.ToString(),
            totalTime);
        
        SetPlayerControlsEnabled(false);
        Time.timeScale = 0;
    }

    public static void AddCoin()
    {
        coins += 1;
    }

    public static void RemoveCoins(int amountToRemove)
    {
        coins -= amountToRemove;
    }

    public static void AddScore(int points)
    {
        score += points;
    }
}