using UnityEngine;

public class EnemyDeath : MonoBehaviour
{

    private SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    void Start()
    {
        Animator animator = GetComponent<Animator>();
        _soundManager.PlayEnemyDie();
        float animTime = animator
            .GetCurrentAnimatorStateInfo(0).length;

        Destroy(gameObject, animTime);
    }
}