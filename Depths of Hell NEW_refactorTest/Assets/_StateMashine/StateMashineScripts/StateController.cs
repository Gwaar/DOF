using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using RPG.Character;


public class StateController : MonoBehaviour

{
    public State currentState;
    public EnemyStats enemyStats;
    public Transform eyes;
    public State remaInState;
    public AICharacterControl aiCharacterControl = null;

    public Player player;




    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public EnemyStats entityAttacking;
    [SerializeField]
    public List<Transform> wayPointList;
    [HideInInspector]
    public int nextWayPoint;
    [HideInInspector]
    public Transform chaseTarget;
    [HideInInspector]
    public float stateTimeElapsed;
    public bool aiActive;






    PatrolAction patrol;
    Enemy _enemy;

    public bool _enemyPatrol;



    private void Awake()
    {
        // entityAttacking = GetComponent<EnemyStats>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        aiCharacterControl = GetComponent<AICharacterControl>();


        _enemy = GetComponent<Enemy>();
        player = GetComponent<Player>();


    }

    public void SetupAi(bool aiActivateFromEntityManager, List<Transform> waypointsFromAiEntity)
    {

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
            Gizmos.DrawSphere(wayPointList[nextWayPoint].position, 3f);

        }
    }



    public void TransitionToState(State nextState)
    {
        if (nextState != remaInState)
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



    public void StateWorking(string State)
    {
        print("hej");
    }




}

