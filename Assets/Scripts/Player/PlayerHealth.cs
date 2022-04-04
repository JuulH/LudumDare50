using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, Health
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float invincibilityTimeAfterHit = 1f;
    private float _currentHealth;
    private bool _isInvincible;

    private SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    void Start()
    {
        _currentHealth = maxHealth;
        uiManager.SetPlayerMaxHealth(maxHealth);
        uiManager.SetPlayerCurrentHealth(_currentHealth);
    }

    public void TakeDamage(float damage)
    {
        if (!_isInvincible)
        {
            _currentHealth -= damage;
            uiManager.SetPlayerCurrentHealth(_currentHealth);
            StartCoroutine(MakeInvicibleFor(invincibilityTimeAfterHit));
        }

        if(_currentHealth <= 0)
        {
            _soundManager.PlayPlayerDie();
            gameManager.GameOver();
        }
    }

    public IEnumerator MakeInvicibleFor(float xSeconds)
    {
        _isInvincible = true;
        yield return new WaitForSeconds(xSeconds);
        _isInvincible = false;
    }

    public void ResetHealth()
    {
        _currentHealth = maxHealth;
    }

    public void RecoverHealth(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }
        uiManager.SetPlayerCurrentHealth(_currentHealth);
    }
}
