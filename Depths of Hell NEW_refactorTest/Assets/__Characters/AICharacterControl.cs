using System;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
  
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        
        public Transform target;                                    // target to aim for
        

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();

            agent.updatePosition = true;
            agent.updateRotation = false;
	       
        }


        private void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
                agent.updateRotation = true;
            }
            else
                agent.velocity = Vector3.zero;
                
                

        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
