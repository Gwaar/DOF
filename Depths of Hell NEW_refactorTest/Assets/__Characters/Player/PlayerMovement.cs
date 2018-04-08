using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
using RPG.Character;
using RPG.Weapon;

[RequireComponent(typeof(ThirdPersonCharacter))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(NavMeshAgent))]

public class PlayerMovement : MonoBehaviour
{
    
    float WalkSpeed;
    public bool isWalking = false;
    GameObject walkTarget = null;
    NavMeshAgent navMeshAgent;
    AICharacterControl aiCharacterControl = null;
    ThirdPersonCharacter thirdPersonCharacter = null;   
    CameraRaycaster cameraRaycaster;
    Animator animator;
    //bool isInDirectMode = false; // TODO consdier making static later


    private Vector3 clickPoint;
    private Vector3 distansToGoal;
    float playerSpeed;
    bool _iswalking = false;

    void Start()
    {
       
      
        //navMeshAgent            = GetComponent<NavMeshAgent>();
        animator                = GetComponent<Animator>();
        walkTarget              = new GameObject("walktarget");
        aiCharacterControl      = GetComponent<AICharacterControl>();
        cameraRaycaster         = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter    = GetComponent<ThirdPersonCharacter>();

      
        cameraRaycaster.onOverPotentiallyWalkable += OnOverPotentiallyWalkable;
        cameraRaycaster.onOverPotentiallyEnemy += OnOverPotentiallyEnemy;


    }

   void  OnOverPotentiallyWalkable(Vector3 destination)
    {
        if (Input.GetMouseButton(0))
        {
            walkTarget.transform.position = destination;
            aiCharacterControl.SetTarget(walkTarget.transform);
        }
       
    }


    void OnOverPotentiallyEnemy(Enemy enemy)

    {
        if (Input.GetMouseButton(0) || (Input.GetMouseButton(1)))
        {
            aiCharacterControl.SetTarget(enemy.transform);
        }

    }



    private void Update()
    {
        
            animator.SetTrigger("Seath");
       // playerSpeed = WalkSpeed;
        playerSpeed =  animator.GetFloat("SpeedProcent");

    //if (_iswalking)
    //    {
    //            WalkSpeed += 10 * Time.deltaTime; // Cap at some max value too

    //        if (walkTarget.transform.position == transform.position)
    //        {
    //            print("targetreached");
    //        }
    //    }
    //    Mathf.Clamp(playerSpeed, 1f, 10f);    

        
    }

    //void ProcessDirectMovement()
    //{
    //    float h = Input.GetAxis("Horizontal");
    //    float v = Input.GetAxis("Vertical");

    //    Vector3 CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
    //    Vector3 Move = v * CamForward + h * Camera.main.transform.right;

    //    thirdPersonCharacter.Move(Move, false, false);
    //}


    public void FootR()    {        Debug.Log("R");    }
    public void FootL()    {        Debug.Log("L");   }

}








