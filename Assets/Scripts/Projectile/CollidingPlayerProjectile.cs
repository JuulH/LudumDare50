using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingPlayerProjectile : MonoBehaviour
{
    [SerializeField] private float damage;
    private SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health health = other.GetComponent<Health>();
            health.TakeDamage(damage);
            _soundManager.PlayCatMeow();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("EnemyProjectile"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}