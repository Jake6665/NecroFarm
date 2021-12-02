using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int curHealth = int.MinValue;
    public int maxHealth = 100;
    public int waitTime;

    private Animator anim;

    public string deathAnim;

    public string thisCharacter = "";

    public HealthBar healthBar;

    private AudioSource sound;

    public AudioClip clip;

    void Start()
    {
        curHealth = maxHealth;
    }
    void Update()
    {
        if (thisCharacter == "")
        {
            thisCharacter = transform.gameObject.name;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DamagePlayer(10);
        }

        healthBar.SetHealth(curHealth);

        if (curHealth <= 0)
        {
            anim = gameObject.GetComponent<Animator>();
            sound = GetComponent<AudioSource>();
            StartCoroutine(DeathWait());
        }
    }

    IEnumerator DeathWait()
    {
        anim.Play(deathAnim);
        sound.PlayOneShot(clip);
        yield return new WaitForSeconds(waitTime);
        DestroyUnit();
    }

    public void DestroyUnit()
    {
        Destroy(GameObject.Find(thisCharacter));
    }

    public void DamagePlayer(int damage)
    {
        curHealth -= damage;
        healthBar.SetHealth(curHealth);
    }
}
