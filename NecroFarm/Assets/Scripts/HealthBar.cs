using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Health playerHealth;

    private int hitPoints = int.MinValue;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
    }

    private void Update()
    {
        if (hitPoints == int.MinValue)
        {
            hitPoints = playerHealth.maxHealth;
        }

        SetHealth(hitPoints);
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}