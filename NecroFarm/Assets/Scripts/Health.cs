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
            DamagePlayer(10, GameObject.Find(thisCharacter));
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
        DestroyUnit(thisCharacter);
    }

    public void DestroyUnit(string deadUnit)
    {
        Destroy(GameObject.Find(deadUnit));
    }

    public void DamagePlayer(int damage, GameObject name)
    {        
        name.GetComponent<Health>().curHealth -= damage;
        name.GetComponent<Health>().healthBar.SetHealth(curHealth);
    }
}
