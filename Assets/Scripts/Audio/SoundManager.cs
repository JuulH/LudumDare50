using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Audio CLips
    [SerializeField] private AudioClip[] menuMusicAC;
    [SerializeField] private AudioClip[] battleMusicAC;

    //Audio Sources
    [SerializeField] private AudioSource enemyDie;
    [SerializeField] private AudioSource playerShoot;
    [SerializeField] private AudioSource catMeow;
    [SerializeField] private AudioSource playerHit;
    [SerializeField] private AudioSource houseHit;
    [SerializeField] private AudioSource houseCollapse;
    [SerializeField] private AudioSource coinPickup;
    [SerializeField] private AudioSource playerDie;
    [SerializeField] private AudioSource upgradeBuy;
    [SerializeField] private AudioSource menuClick; // I think this one is going to be alone at the menu prefab
    [SerializeField] private AudioSource menuMusicAS;
    [SerializeField] private AudioSource battleMusicAS;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEnemyDie()
    {
        this.enemyDie.Play();
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

    public void PlayHouseHit()
    {
        this.houseHit.Play();
    }

    public void PlayCoinPickup()
    {
        this.coinPickup.Play();
    }

    public void PlayPlayerDie()
    {
        this.playerDie.Play();
    }

    public void PlayerUpgradeBuy()
    {
        this.upgradeBuy.Play();
    }


}
