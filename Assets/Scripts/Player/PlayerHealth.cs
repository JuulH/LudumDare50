using System.Collections;
using System.Numerics;
using Cinemachine;
using Player;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerHealth : MonoBehaviour, Health
{
    [SerializeField] public float maxHealth = 100;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject weaponAttachment;
    [SerializeField] private GameObject playerDeath;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private GameObject shadow;
    public float _currentHealth;
    public bool _isDead;

    private SoundManager _soundManager;
    private Animator _animator;
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private PlayerMovementController _playerMovementController;

    private void Awake()
    {
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        _animator = GetComponent<Animator>();
        _playerMovementController = GetComponent<PlayerMovementController>();
    }

    void Start()
    {
        _currentHealth = maxHealth;
        uiManager.SetPlayerMaxHealth(maxHealth);
        uiManager.SetPlayerCurrentHealth(_currentHealth);
    }

    public void TakeDamage(float damage)
    {
        if (_isDead)
        {
            _playerMovementController.dir = Vector2.zero;
            return;
        }
        _currentHealth -= damage;
        uiManager.SetPlayerCurrentHealth(_currentHealth);

        if (_currentHealth <= 0)
        {
            weaponAttachment.SetActive(false);
            _playerMovementController.enabled = false;
            _playerMovementController.dir = Vector2.zero;
            GetComponent<SpriteRenderer>().enabled = false;
            shadow.SetActive(false);
            GameObject createdDeathAnimation = Instantiate(playerDeath, transform.position, transform.rotation, transform);
            cinemachineVirtualCamera.LookAt = createdDeathAnimation.transform;
            
            _soundManager.PlayPlayerDie();
            _isDead = true;
            StartCoroutine(StartGameOver(3));
        }
    }

    private IEnumerator StartGameOver(int delay)
    {
        yield return new WaitForSeconds(delay);
        gameManager.GameOver();
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