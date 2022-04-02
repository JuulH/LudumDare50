using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject _house;
    private GameObject _player;
    private AIPath _aiPath;

    private GameObject _currentTarget;
    private float _attackDistance;

    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private float attackCooldown = 1.5f;
    private float _timeSinceLastAttack = 999f;

    void Awake()
    {
        _aiPath = GetComponent<AIPath>();
        _house = GameObject.FindWithTag("House");
        _player = GameObject.FindWithTag("Player");
        _currentTarget = _player;
        _attackDistance = _aiPath.endReachedDistance;
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;
        _aiPath.destination = _currentTarget.transform.position;

        float distanceToPlayer = Vector2.Distance(_player.transform.position, transform.position);
        if (distanceToPlayer <= _attackDistance)
        {
            if (_timeSinceLastAttack > attackCooldown)
            {
                DoAttack();
            }
        }
    }

    private void DoAttack()
    {
        Vector2 enemyPosition = transform.position;
        Vector2 playerPosition = _player.transform.position;
        Vector2 dirToPlayer = playerPosition - enemyPosition ;
        float angle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x)  * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(enemyProjectile, transform.position, rotation);
        _timeSinceLastAttack = 0;
    }
}