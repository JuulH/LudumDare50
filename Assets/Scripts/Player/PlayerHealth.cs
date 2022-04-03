using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, Health
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float invincibilityTimeAfterHit = 1f;
    private float _currentHealth;
    private bool _isInvincible;

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
            GameManager.GameOver();
        }
    }

    public IEnumerator MakeInvicibleFor(float xSeconds)
    {
        _isInvincible = true;
        yield return new WaitForSeconds(xSeconds);
        _isInvincible = false;
    }
}
