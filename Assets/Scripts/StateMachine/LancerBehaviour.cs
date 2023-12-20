using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LancerBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    GameObject playerPos;
    EnemyPatrol enemyPatrol;
    float distanceToKeep = 10f; // La distance à maintenir avec le joueur
    float rushCooldown = 5f; // Le temps de recharge de la ruée
    float rushTimer; // Le compteur pour la ruée
    float healthPercentage; // Le pourcentage de santé du lancier

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        enemyPatrol = animator.GetComponent<EnemyPatrol>();
        playerPos = GameObject.FindGameObjectWithTag("Player");
        agent.isStopped = false;
        rushTimer = rushCooldown; // Initialiser le compteur de ruée
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceToPlayer = Vector3.Distance(agent.transform.position, playerPos.transform.position);
        // healthPercentage = animator.GetComponent<Health>().currentHealth / animator.GetComponent<Health>().maxHealth * 100; // Calculer le pourcentage de santé

        if (distanceToPlayer < distanceToKeep)
        {
            Vector3 dirToPlayer = (agent.transform.position - playerPos.transform.position).normalized;
            Vector3 newPos = playerPos.transform.position + (dirToPlayer * distanceToKeep);
            agent.SetDestination(newPos);
        }

        if (healthPercentage <= 50 && rushTimer >= rushCooldown)
        {
            // Effectuer une ruée vers le joueur
            agent.SetDestination(playerPos.transform.position);
            rushTimer = 0;
        }

        rushTimer += Time.deltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.isStopped = true;
    }
}