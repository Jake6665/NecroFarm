using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMoveScript : MonoBehaviour
{
    public NavMeshAgent agent;

    GameObject target;

    public LayerMask whatIsGround, whatIsTarget;

    public float attackRange, attackCooldown;
    public bool targetInAttackRange, hasAttacked;

    private Animator anim;

    public string attackAnim;

    [SerializeField]
    private Health playerHealth;
    [SerializeField]
    private int attackDamage;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Awake()
    {
        targetInAttackRange = false;
        //Set target
        target = FindClosestTarget();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Update()
    {
        if(target == null)
        {
            target = FindClosestTarget();
        }
        //Check if in attack range
        if(Vector3.Distance(this.transform.position, FindClosestTarget().transform.position) < attackRange)
        {
            AttackTarget();
        }
        else
        {
            MoveToTarget();
        }            


    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.transform.position);
        anim.SetBool("isWalking", true);
    }
    private void AttackTarget()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(target.transform);
        anim.SetBool("isWalking", false);

        if (!hasAttacked)
        {
            anim.Play(attackAnim);
            playerHealth.DamagePlayer(attackDamage, target);
            hasAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);

        }



    }
    private void ResetAttack()
    {
        hasAttacked = false;
    }

        public GameObject FindClosestTarget()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

}
