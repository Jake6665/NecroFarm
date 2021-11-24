using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSFX : MonoBehaviour
{
    private AudioSource sound;
    public AudioClip clip;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    void Step()
    {
        sound.PlayOneShot(clip);
    }
}
