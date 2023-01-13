using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public int spriteP1;
    public int spriteP2;

    public Image p1, p2;

    public Sprite[] _sprites;
    
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void selectPersoP1(int dir)
    {
        spriteP1 += dir;
        if (spriteP1 > 3) spriteP1 = 1;
        if (spriteP1 < 1) spriteP1 = 3;
        p1.sprite = _sprites[spriteP1 -1];
        print(spriteP1);
    }

    public void selectPersoP2(int dir2)
    {
        spriteP2 += dir2;
        if (spriteP2 > 3) spriteP2 = 1;
        if (spriteP2 < 1) spriteP2 = 3;
        p2.sprite = _sprites[spriteP2 -1];
        print(spriteP2);
    }
}