using System.Collections;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject _house;
    private GameObject _player;
    private AIPath _aiPath;
    private Animator _animator;

    private Transform _currentTarget;
    private float _attackDistance;

    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float throwAttackStartUp = 0.5f;
    private float _timeSinceLastAttack = 999f;
    private bool _isAttacking;
    private bool _isHouseAttacker;

    private static readonly int Throwing = Animator.StringToHash("Throwing");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsStanding = Animator.StringToHash("IsStanding");

    void Start()
    {
        _aiPath = GetComponent<AIPath>();
        _animator = GetComponent<Animator>();
        _house = GameObject.FindWithTag("House");
        _player = GameObject.FindWithTag("Player");
        int random = Random.Range(0, 2);
        _currentTarget = (random == 0) ? _player.transform : _house.transform;
        if (_currentTarget == _house.transform)
        {
            _attackDistance = _aiPath.endReachedDistance;
            _aiPath.endReachedDistance -= 3f;
            _isHouseAttacker = true;
            Transform houseTarget = _house.transform;
            float closestDist = 999;
            GameObject houseDestinations = GameObject.FindGameObjectWithTag("HouseDestinations");
            foreach (Transform x in houseDestinations.transform)
            {
                float distance = Vector2.Distance(x.transform.position, this.transform.position);
                if (distance < closestDist)
                {
                    houseTarget = x;
                    closestDist = distance;
                }
            }

            _currentTarget = houseTarget;
        }
        else
        {
            _attackDistance = _aiPath.endReachedDistance;
        }
    }

    void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;
        _aiPath.destination = _currentTarget.position;

        float distanceToTarget;
        if (_isHouseAttacker)
        {
            distanceToTarget = Vector2.Distance(_house.transform.position, transform.position);
        }
        else
        {
            distanceToTarget = Vector2.Distance(_currentTarget.position, transform.position);
        }

        if (distanceToTarget <= _attackDistance || _aiPath.reachedEndOfPath)
        {
            if (!_isAttacking && _timeSinceLastAttack > attackCooldown)
            {
                Transform target = _isHouseAttacker ? _house.transform : _currentTarget;
                StartCoroutine(DoAttack(target));
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

    private IEnumerator DoAttack(Transform target)
    {
        _isAttacking = true;
        _aiPath.canMove = false;
        _animator.SetTrigger(Throwing);
        yield return new WaitForSeconds(throwAttackStartUp);
        Vector2 enemyPosition = transform.position;
        Vector2 targetPosition = target.position;
        Vector2 dirToTarget = targetPosition - enemyPosition;
        float angle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(enemyProjectile, transform.position, rotation);
        _isAttacking = false;
        _aiPath.canMove = true;
        _timeSinceLastAttack = 0;
    }
}