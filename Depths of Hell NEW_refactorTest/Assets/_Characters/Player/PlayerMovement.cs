using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
using RPG.Character;

[RequireComponent(typeof(ThirdPersonCharacter))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(NavMeshAgent))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]    const int walkableLayerNumber = 8;
    [SerializeField]    const int enemybleLayerNumber = 9;

    [SerializeField]    float walkMoveStopRadius = 0.2f;
    [SerializeField]    float AttackMoveStopRadius = 5f;

    float WalkSpeed;
    public bool isWalking = false;
    GameObject walkTarget = null;

    NavMeshAgent navMeshAgent;
    AICharacterControl aiCharacterControl = null;

    ThirdPersonCharacter thirdPersonCharacter = null;   
    CameraRaycaster cameraRaycaster;



    GameObject player;


    Animator animator;

    bool isInDirectMode = false; // TODO consdier making static later

    private Vector3 clickPoint;
    private Vector3 distansToGoal;

    float playerSpeed;
    bool _iswalking = false;

    void Start()
    {

      
        navMeshAgent            = GetComponent<NavMeshAgent>();
        animator                = GetComponent<Animator>();
        walkTarget              = new GameObject("walktarget");
        aiCharacterControl      = GetComponent<AICharacterControl>();
        cameraRaycaster         = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter    = GetComponent<ThirdPersonCharacter>();

        

        cameraRaycaster.notifyMouseClickObservers += PorcessMouseClick;


       
    }

    private void Update()
    {
        if(Input.GetKey("1"))
        animator.SetTrigger("Attack");
        if (Input.GetKey("2"))



          
        animator.SetTrigger("UnSeath");
        if (Input.GetKey("3"))
            animator.SetTrigger("Seath");
        playerSpeed = WalkSpeed;
        playerSpeed =  animator.GetFloat("SpeedProcent");

    if (_iswalking)
        {
                WalkSpeed += 10 * Time.deltaTime; // Cap at some max value too

            if (walkTarget.transform.position == transform.position)
            {
                print("targetreached");
            }
        }
        Mathf.Clamp(playerSpeed, 1f, 10f);    

        
    }

    void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 Move = v * CamForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(Move, false, false);
    }
    void PorcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        switch (layerHit)
        {
            case enemybleLayerNumber:
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget(enemy.transform);            
                break;

            case walkableLayerNumber:
                _iswalking = true;
              
                walkTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget(walkTarget.transform);
                break;

            default: return;        
        }
    }

   
    public void FootR()
    {
        Debug.Log("R");
    }
    public void FootL()
    {
        Debug.Log("L");
    }

}








