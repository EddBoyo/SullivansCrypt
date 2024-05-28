using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SkeletonMeleeChaseState : StateMachineBehaviour
{
     NavMeshAgent agent;
    Transform player;
    float attackRange = 4f;
    float chaseRange = 20;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent = animator.GetComponent<NavMeshAgent>();
       agent.speed = 3.5f;
       player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(player.position);
       float distance = Vector3.Distance(player.position, animator.transform.position);
       if(distance>chaseRange){
        animator.SetBool("IsChasing", false);
       }
       if(distance<attackRange){
        animator.SetBool("IsAttacking", true);
       }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(animator.transform.position);
       
    }
}
