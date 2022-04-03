using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Audio CLips


    //Audio Sources
    [SerializeField] private AudioSource enemyDie;
    [SerializeField] private AudioSource playerShoot;
    [SerializeField] private AudioSource catMeow;
    [SerializeField] private AudioSource playerHit;
    [SerializeField] private AudioSource houseCollapse;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPlayerShoot()
    {
        this.playerShoot.Play();
    }

    public void PlayPlayerHit()
    {
        this.playerHit.Play();
    }

    public void PlayCatMeow()
    {
        // this.catMeowb;
    }
}
