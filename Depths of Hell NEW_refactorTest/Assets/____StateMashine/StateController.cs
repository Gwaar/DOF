using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Complete;



public class StateController : MonoBehaviour

{
    public State currentState;
    public EnemyStats entityStats;
    public Transform eyes;
    public State remainState;



    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Complete.EntityAttacking entityAttacking;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;

    private bool aiActive;



    private void Awake()
    {
        entityAttacking = GetComponent<Complete.EntityAttacking>();
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    public void SetupAi(bool aiActivateFromEntityManager, List<Transform> waypointsFromAiEntity)
    {
        wayPointList = waypointsFromAiEntity;
        aiActive = aiActivateFromEntityManager;
        if (aiActive)
        {
            navMeshAgent.enabled = true;

        }
        else
        {
            navMeshAgent.enabled = false;
        }
    }

    private void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);



    }

    void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;

        }
    }



    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return stateTimeElapsed >= duration;

    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }



}
