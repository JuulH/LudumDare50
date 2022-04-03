using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip enemyDie;
    [SerializeField] private AudioClip playerShoot;
    [SerializeField] private AudioClip catMeow;
    [SerializeField] private AudioClip playerHit;
    [SerializeField] private AudioClip houseCollapse;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPlayerShoot()
    {
        audioSource.PlayOneShot(playerShoot);
    }

    public void PlayPlayerHit()
    {
        audioSource.PlayOneShot(playerHit);
    }

    public void PlayCatMeow()
    {
        audioSource.PlayOneShot(catMeow);
    }
}
