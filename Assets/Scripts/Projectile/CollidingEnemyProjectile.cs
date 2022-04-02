using System;
using UnityEngine;

public class CollidingEnemyProjectile : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("House") || other.CompareTag("Player"))
        {
            Health health = other.GetComponent<Health>();
            health.TakeDamage(damage);
            Destroy(this.gameObject);
        } 
    }

}
