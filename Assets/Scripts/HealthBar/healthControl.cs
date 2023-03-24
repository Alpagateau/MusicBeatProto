
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthControl : MonoBehaviour
{
    public float damageAnimationDuration = 0.5f;
    public Image HealthBar;
    public float maxHP = 100f;
    public float HP;
    Color orange = new Color32(255, 255, 0, 100);
    Color green = new Color32(0,255,0,100);
    Color red = new Color32(255, 0, 0, 100);
    public void Start()
    {
        HP = maxHP;
        HealthBar.color = green;

    }

    public void TakeDamage(int dmg)
    {
        HP -= dmg;
        float fillAmount = HP / maxHP;
        float targetFillAmount = fillAmount - 0.1f;
        StartCoroutine(LerpFillAmount(targetFillAmount, damageAnimationDuration));
        if (HP <= 50)
        {
            HealthBar.color = orange;
        }
        if (HP <= 25)
        {
            HealthBar.color = red;

        }
    }
    private IEnumerator LerpFillAmount(float targetFillAmount, float duration)
    {
        float startTime = Time.time;
        float startFillAmount = HealthBar.fillAmount;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            HealthBar.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, t);
            yield return null;
        }

        HealthBar.fillAmount = targetFillAmount;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        TakeDamage(10);
    }
}