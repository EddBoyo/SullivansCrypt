using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedDragonPatrolState : StateMachineBehaviour
{
    float timer;
    float chaseRange = 20;
    NavMeshAgent agent;
    Transform player;
    List<Transform> wp = new List<Transform>();
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent = animator.GetComponent<NavMeshAgent>();
       agent.speed = 1.5f;
       player = GameObject.FindGameObjectWithTag("Player").transform;
       timer = 0;
       //Write Code underneath here to go to destination
       GameObject go = GameObject.FindGameObjectWithTag("WayPoints");
       foreach(Transform t in go.transform){
            wp.Add(t);
       }
       agent.SetDestination(wp[Random.Range(0, wp.Count)].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(agent.remainingDistance<= agent.stoppingDistance){
         agent.SetDestination(wp[Random.Range(0, wp.Count)].position);
       }
       timer+= Time.deltaTime;
       if(timer>10){
        animator.SetBool("isPatrolling", false);
       }
       float distance = Vector3.Distance(player.position, animator.transform.position);
       if(distance<chaseRange){
        animator.SetBool("isChasing", true);
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(agent.transform.position);
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
