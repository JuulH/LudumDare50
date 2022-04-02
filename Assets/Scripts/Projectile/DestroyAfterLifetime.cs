using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterLifetime : MonoBehaviour
{
    [SerializeField] private float lifeTIme;
    
    void Start()
    {
        Destroy(this.gameObject, lifeTIme);
    }

    void Update()
    {
        
    }
}
