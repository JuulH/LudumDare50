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
    [SerializeField] private float houseAttackOffsetDistance = 1.5f;
    private float _timeSinceLastAttack = 999f;
    private bool _isAttacking;
    private bool _isHouseAttacker;

    private SoundManager _soundManager;
    
    private static readonly int Throwing = Animator.StringToHash("Throwing");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsStanding = Animator.StringToHash("IsStanding");
    private Rigidbody2D _rb;

    void Awake()
    {
        _aiPath = GetComponent<AIPath>();
        _animator = GetComponent<Animator>();
        _house = GameObject.FindWithTag("House");
        _player = GameObject.FindWithTag("Player");
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        _rb = GetComponent<Rigidbody2D>();
        int random = Random.Range(0, 2);
        _currentTarget = (random == 0) ? _player : _house;
        if (_currentTarget == _house)
        {
            _attackDistance = _aiPath.endReachedDistance + houseAttackOffsetDistance;
            _isHouseAttacker = true;
        }
        else
        {
            _attackDistance = _aiPath.endReachedDistance;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;
        if (_isAttacking)
        {
            _aiPath.canMove = false;
        }
        else
        {
            _aiPath.canMove = true;
        }
        _aiPath.destination = _currentTarget.transform.position;

        float distanceToTarget = Vector2.Distance(_currentTarget.transform.position, transform.position);
        if (distanceToTarget <= _attackDistance)
        {
            if (!_isAttacking && _timeSinceLastAttack > attackCooldown)
            {
                StartCoroutine(DoAttack());
            }
            _animator.SetBool(IsMoving, false);
            _animator.SetBool(IsStanding, true);
            if (_isHouseAttacker) {
                _aiPath.enabled = false; //So house attackers stop once they attack house, and just stand there. As pathfinding for house is weird and buggy
                _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
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
        Vector2 targetPosition = _currentTarget.transform.position;
        Vector2 dirToTarget = targetPosition - enemyPosition ;
        float angle = Mathf.Atan2(dirToTarget.y, dirToTarget.x)  * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(enemyProjectile, transform.position, rotation);
        _isAttacking = false;
        _timeSinceLastAttack = 0;
    }
}