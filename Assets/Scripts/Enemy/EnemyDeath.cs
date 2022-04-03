using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        float animTime = animator
            .GetCurrentAnimatorStateInfo(0).length;

        Destroy(gameObject, animTime);
    }
}