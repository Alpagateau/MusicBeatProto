using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    private AudioSource audioSrcbg;
    private float musicVolume = 1f;
    
    public AudioSource soundPlayer;


    public int spriteP1;
    public int spriteP2;

    public KeepObjectAlive k;

    public Image p1, p2;

    public Sprite[] _sprites;

    public void StartButton()
    {
        k.p1 = spriteP1;
        k.p2 = spriteP2;
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
    public void selectPersoP1(int dir)
    {
        spriteP1 += dir;
        if (spriteP1 > 3) spriteP1 = 1;
        if (spriteP1 < 1) spriteP1 = 3;
        p1.sprite = _sprites[spriteP1 - 1];
        print(spriteP1);
    }

    public void selectPersoP2(int dir2)
    {
        spriteP2 += dir2;
        if (spriteP2 > 3) spriteP2 = 1;
        if (spriteP2 < 1) spriteP2 = 3;
        p2.sprite = _sprites[spriteP2 - 1];
        print(spriteP2);
    }

    // SOUNDS

    public void playSound()
    {
        soundPlayer.Play();
    }
    // BG volume and effects volume slider 
    void Start()
    {
        audioSrcbg = GetComponent<AudioSource>();
    }
    void Update()
    {
        audioSrcbg.volume = musicVolume;
    }
    public void SetVolumeBg(float vol)
    {
        musicVolume = vol;
    }

}