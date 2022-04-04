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

    private bool isMenu;
    private bool isGame;

    private int currentMusicIndex;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(isMenu)
         {
            if(currentMusicIndex == 0 && !menuMusicAS.isPlaying)
            {
                currentMusicIndex++;
                AudioClip secondTrack = menuMusicAC[currentMusicIndex];
                menuMusicAS.clip = secondTrack;
                menuMusicAS.loop = true;
                menuMusicAS.Play();
            }  
         }
         else if(isGame)
        {
            if(currentMusicIndex == 0 && !battleMusicAS.isPlaying)
            {
                currentMusicIndex++;
                AudioClip secondTrack = battleMusicAC[currentMusicIndex];
                battleMusicAS.clip = secondTrack;
                battleMusicAS.loop = true;
                battleMusicAS.Play();
            }
        }

    }

    public void PlayMenuMusic()
    {
        isMenu = true;
        isGame = false;
        currentMusicIndex = 0;
        AudioClip firstTrack = menuMusicAC[currentMusicIndex];
        menuMusicAS.clip = firstTrack;
        menuMusicAS.loop = false;
        battleMusicAS.Stop();
        menuMusicAS.Play();
    }

    public void PlayGameMusic()
    {
        isGame = true;
        isMenu = false;
        currentMusicIndex = 0;
        AudioClip firstTrack = battleMusicAC[currentMusicIndex];
        battleMusicAS.clip = firstTrack;
        battleMusicAS.loop = false;
        menuMusicAS.Stop();
        battleMusicAS.Play();
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
