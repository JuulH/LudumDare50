using System;
using UnityEngine;

public class CollidingEnemyProjectile : MonoBehaviour
{
    [SerializeField] private float damage;
    private SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("House"))
        {
            TakeDamage(other);
        }
        else if (other.CompareTag("Player"))
        {
            _soundManager.PlayPlayerHit();
            TakeDamage(other);
        }
    }

    private void TakeDamage(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        health.TakeDamage(damage);
        Destroy(this.gameObject);
    }
}