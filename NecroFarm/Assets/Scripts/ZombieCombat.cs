using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCombat : MonoBehaviour
{

    public bool hasCollided = false;

    private Animator anim;

    public string attackAnim;

    public GameObject target;

    public int damage;

    public int waitTime;

    public Health playerHealth;

    void Update()
    {
        if (hasCollided)
        {
            Debug.Log("Enemy Unit is Attacking: " + target);

            anim = gameObject.GetComponent<Animator>();
            StartCoroutine(AttackWait());
        }

        if (target == null)
        {
            hasCollided = false;
        }
    }

    IEnumerator AttackWait()
    {
        transform.LookAt(target.transform);
        yield return new WaitForSeconds(waitTime);
        anim.Play(attackAnim);
        playerHealth.DamagePlayer(damage, target);
        yield return new WaitForSeconds(waitTime);

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided!");

        if (other.tag == "Enemy")
        {
            target = GameObject.Find(other.transform.gameObject.name);

            hasCollided = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        hasCollided = false;
        target = null;
    }
}
