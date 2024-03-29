using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject weaponAttachment;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawnPos;
    [SerializeField] private Transform miniGunProjectileSpawnPos;

    public float attackCooldown = 1.5f;
    private float _timeSinceLastAttack = 999f;

    private SoundManager _soundManager;
    
    public bool isControlsEnabled;
    private readonly Quaternion bulletRotationOffset = Quaternion.Euler(0,0,90);


    private void Awake()
    {
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        if (!isControlsEnabled) return;

        _timeSinceLastAttack += Time.deltaTime;
        if (_timeSinceLastAttack > attackCooldown)
        {
            if (Input.GetMouseButton(0))
            {
                StartAttack();
            }
        }
    }

    private void StartAttack()
    {
        if (Input.GetMouseButton(0))
        {
            Quaternion bulletRotation = weaponAttachment.transform.rotation * bulletRotationOffset;
            Instantiate(projectile, projectileSpawnPos.position, bulletRotation);
            _soundManager.PlayPlayerShoot();
            _timeSinceLastAttack = 0;
        }
    }
    
    public void SwitchProjectileSpawnToMiniGun()
    {
        projectileSpawnPos.transform.position = miniGunProjectileSpawnPos.transform.position;
    }
}