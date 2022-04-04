using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseHealth : MonoBehaviour, Health
{
    public float maxHealth = 100;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameManager gameManager;
    public float _currentHealth;
    private bool _isInvincible = false;

    private SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    void Start()
    {
        _currentHealth = maxHealth;
        uiManager.SetHouseMaxHealth(maxHealth);
        uiManager.SetHouseCurrentHealth(_currentHealth);
    }

    public void TakeDamage(float damage)
    {
        if (!_isInvincible)
        {
            _currentHealth -= damage;
            uiManager.SetHouseCurrentHealth(_currentHealth);
        }

        if (_currentHealth <= 0)
        {
            gameManager.GameOver();
            _soundManager.PlayHouseCollapse();
        }
    }

    public void UpdateHealthBar()
    {
        uiManager.SetHouseMaxHealth(maxHealth);
        uiManager.SetHouseCurrentHealth(_currentHealth);
    }

    public void ResetHealth()
    {
        _currentHealth = maxHealth;
    }

    public void RepairHealth(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }
        uiManager.SetHouseCurrentHealth(_currentHealth);
    }
}
