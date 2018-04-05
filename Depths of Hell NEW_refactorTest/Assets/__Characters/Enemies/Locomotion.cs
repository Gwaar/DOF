using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Locomotion : MonoBehaviour {

    const float AnimationSmoothTime = .1f;

    Animator animator;
    NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float SpeedProcent = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        
        animator.SetFloat("SpeedProcent", SpeedProcent, AnimationSmoothTime, Time.deltaTime);
	}
}
