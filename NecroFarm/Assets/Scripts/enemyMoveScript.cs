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

    [SerializeField]
    int health;

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
        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsTarget);

        if (targetInAttackRange)
        {
            AttackTarget();
        }
        else
        {
            MoveToTarget();
        }

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.transform.position);
    }
    private void AttackTarget()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(target.transform);

        if (!hasAttacked)
        {
            /**
             * 
             * ADD ATTACK METHOD HERE
             * 
            **/
            //Destroys target not needed after attack method is in
            Destroy(target.gameObject);

            //Sets new target
            target = FindClosestTarget();

            hasAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);

        }



    }
    private void ResetAttack()
    {
        hasAttacked = false;
    }

    private void takeDamage(int dmgTaken)
    {
        health -= dmgTaken;
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
