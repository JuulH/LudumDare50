using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingPlayerProjectile : MonoBehaviour
{

    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health health = other.GetComponent<Health>();
            health.TakeDamage(damage);
            Destroy(this.gameObject);
        } 
    }
}
