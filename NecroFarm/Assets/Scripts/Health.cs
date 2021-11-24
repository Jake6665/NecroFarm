using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int curHealth = int.MinValue;
    public int maxHealth = 100;

    public string thisCharacter = "";

    public HealthBar healthBar;

    void Update()
    {
        if (curHealth == int.MinValue)
        {
            curHealth = maxHealth;
        }

        if (thisCharacter == "")
        {
            thisCharacter = transform.gameObject.name;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DamagePlayer(10);
        }
        healthBar.SetHealth(curHealth);
    }

    public void DamagePlayer(int damage)
    {
        curHealth -= damage;
        healthBar.SetHealth(curHealth);
    }
}
