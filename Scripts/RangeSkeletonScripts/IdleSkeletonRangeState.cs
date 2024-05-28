using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleSkeletonRangeState : StateMachineBehaviour
{
    NavMeshAgent agent;
    float timer;
    float chaseRange = 20f;
    float rangeAttackRange = 10f;
    Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        //Debug.Log("timer");
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("idling");
        //Debug.Log("timer");
       timer+= Time.deltaTime;
       float distance = Vector3.Distance(player.position, animator.transform.position);
       if(distance<rangeAttackRange){
        animator.SetBool("IsRanging", true);
       }
       if(distance<chaseRange){
        animator.SetBool("IsChasing", true);
       }
       if(timer>0.1){
        Debug.Log("true");
        animator.SetBool("IsPatrolling", true);
       }
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
