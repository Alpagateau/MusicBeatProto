using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthControl : MonoBehaviour
{
    public Image HealthBar;
    public float maxHP = 100f;
    public float HP;

    public void Start()
    {
        HP = maxHP;
    }

    public void TakeDamage(int dmg)
    {
        HP -= dmg;
        HealthBar.fillAmount = HP / maxHP;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        TakeDamage(10);
    }
}
