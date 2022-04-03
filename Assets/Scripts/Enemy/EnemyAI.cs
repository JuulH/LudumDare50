using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject _house;
    private GameObject _player;
    private AIPath _aiPath;
    private Animator _animator;

    private GameObject _currentTarget;
    private float _attackDistance;

    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float throwAttackStartUp = 0.5f;
    private float _timeSinceLastAttack = 999f;
    private bool _isAttacking;

    private SoundManager _soundManager;
    
    private static readonly int Throwing = Animator.StringToHash("Throwing");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsStanding = Animator.StringToHash("IsStanding");

    void Awake()
    {
        _aiPath = GetComponent<AIPath>();
        _animator = GetComponent<Animator>();
        _house = GameObject.FindWithTag("House");
        _player = GameObject.FindWithTag("Player");
        _currentTarget = _player;
        _attackDistance = _aiPath.endReachedDistance;
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;
        _aiPath.destination = _currentTarget.transform.position;

        float distanceToPlayer = Vector2.Distance(_player.transform.position, transform.position);
        if (distanceToPlayer <= _attackDistance)
        {
            if (!_isAttacking && _timeSinceLastAttack > attackCooldown)
            {
                StartCoroutine(DoAttack());
            }
            _animator.SetBool(IsMoving, false);
            _animator.SetBool(IsStanding, true);
        }
        else
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetBool(IsStanding, false);
        }
    }

    private IEnumerator DoAttack()
    {
        _isAttacking = true;
        _animator.SetTrigger(Throwing);
        yield return new WaitForSeconds(throwAttackStartUp);
        Vector2 enemyPosition = transform.position;
        Vector2 playerPosition = _player.transform.position;
        Vector2 dirToPlayer = playerPosition - enemyPosition ;
        float angle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x)  * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(enemyProjectile, transform.position, rotation);
        _isAttacking = false;
        _timeSinceLastAttack = 0;
    }
}