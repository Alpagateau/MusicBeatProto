using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public int SpriteP1;
    public int SpriteP2;
    
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void selectPersoP1(int dir)
    {
        SpriteP1 += dir;
        if (SpriteP1 > 3) SpriteP1 = 1;
        if (SpriteP1 < 1) SpriteP1 = 3;
        print(SpriteP1);
    }

    public void selectPersoP2(int dir2)
    {
        SpriteP2 += dir2;
        if (SpriteP2 > 3) SpriteP2 = 1;
        if (SpriteP2 < 1) SpriteP2 = 3;
        print(SpriteP2);
    }
}
