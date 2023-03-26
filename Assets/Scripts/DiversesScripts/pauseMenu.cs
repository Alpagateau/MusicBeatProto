using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]
public class pauseMenu : MonoBehaviour
{
    public GameObject menu; 
    private bool isShowing;
    public AudioSource audioSource; // Public audio source variable
    void Start()
    {
        Time.timeScale = 1;
        audioSource.Play();

    }
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (isShowing == false)
            {
                Time.timeScale = 0;
                menu.SetActive(true);
                isShowing = true;
                audioSource.Pause();
            }
            else
            {
                Time.timeScale = 1;
                menu.SetActive(false);
                isShowing = false;
                audioSource.UnPause(); 
        }
            }
    }
    public void BackButton()
    { 
        SceneManager.LoadScene(0);
    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
        audioSource.UnPause();
        isShowing = false;
        menu.SetActive(false);
    }
}
