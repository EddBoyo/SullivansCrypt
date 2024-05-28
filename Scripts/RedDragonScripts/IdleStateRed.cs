using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class IdleStateRed : StateMachineBehaviour
{
    //NavMeshAgent agent;
    float timer;
    float chaseRange = 20;
    float rangeAttackRange = 10f;
    Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        Debug.Log("timer");
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Debug.Log("timer");
       timer+= Time.deltaTime;
       float distance = Vector3.Distance(player.position, animator.transform.position);
       if(distance<rangeAttackRange){
        animator.SetBool("isRanging", true);
       }
       if(distance<chaseRange){
        animator.SetBool("isChasing", true);
       }
       if(timer>5)
        animator.SetBool("isPatrolling", true);
       
       
       /*if(distance<rangeAttackRange){
        agent.SetDestination(animator.transform.position);
        animator.SetBool("isRanging", true);
       }*/
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
