using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private bool hasCollided = false;

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
    }

    IEnumerator AttackWait()
    {
        yield return new WaitForSeconds(waitTime);
        anim.Play(attackAnim);
        //playerHealth.DamagePlayer(damage, target);
        yield return new WaitForSeconds(waitTime);

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided!");

        if (other.tag == "Zombie" || other.tag == "Well")
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
