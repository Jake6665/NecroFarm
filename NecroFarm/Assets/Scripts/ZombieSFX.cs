using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSFX : MonoBehaviour
{
    private AudioSource sound;
    public AudioClip footSteps;
    public AudioClip attackSound;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    void Step()
    {
        sound.PlayOneShot(footSteps);
    }

    void Attack()
    {
        sound.PlayOneShot(attackSound);
    }
}
