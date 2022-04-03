using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterLifetime : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }
}
