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

    [SerializeField] private float attackCooldown = 1.5f;
    private float _timeSinceLastAttack = 999f;


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackSound;
    
    public bool isControlsEnabled;

    private Animator _animator;
    private PlayerMovementController _playerMovementController;

    
    // Update is called once per frame
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
            Instantiate(projectile, projectileSpawnPos.position, weaponAttachment.transform.rotation);
            _timeSinceLastAttack = 0;
        }
    }
}