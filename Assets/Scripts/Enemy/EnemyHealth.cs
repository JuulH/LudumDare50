using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, Health
{

    public void TakeDamage(float damage)
    {
        Destroy(this.gameObject);
    }
}
