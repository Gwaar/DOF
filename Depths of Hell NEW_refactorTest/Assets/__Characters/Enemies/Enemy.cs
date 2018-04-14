
using RPG.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using RPG.Character;
public class Enemy : MonoBehaviour, IDamageable
{


    [SerializeField]
    float attackRadius = 5f;
    [SerializeField]
    float MoveRadius = 2f;
    [SerializeField]
    float DMGperShot = 7f;
    [SerializeField]
    float SecBetweenShots = 2f;
    [SerializeField]
    float maxHealthpoints = 100f;

[SerializeField] float veriation = 1f ;
 






    [SerializeField]
    AnimatorOverrideController animatorOverrideController = null;
    AnimationEvent ae = new AnimationEvent();
    Animator animator;    
    [SerializeField]
    float DMGpertHit = 9;
    [SerializeField]
    EnemyWeapon enemyWeapon = null;




    [SerializeField]
    Transform waypoint;

    [SerializeField]
    GameObject Projectile;
    [SerializeField]
    GameObject ProjectileSocket;

    [SerializeField]
    Vector3 aimOffset = new Vector3(0, 1f, 0);

    bool isAttacking = false;
    float currenthHealthPoints;


    ThirdPersonCharacter thirdPersonCharacter = null;
    AICharacterControl aiCharacterControl = null;

    Player player = null;

    public float healthAsPercentage { get { return currenthHealthPoints / (maxHealthpoints); } }

    void Start()
    {
        ResetAnimController();


        OverrideWeaponAnimatorController_Combat();




        currenthHealthPoints = maxHealthpoints;
        aiCharacterControl = GetComponent<AICharacterControl>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {


        if (player.healthAsPercentage<= Mathf.Epsilon){
            StopAllCoroutines();
            Destroy(this);
        }
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= attackRadius && !isAttacking)
        {

            
            //animatorOverrideController["DEFAULT_ATTACK"] = enemyWeapon.GetCombatState_AttackAnimation();


            

            float RandomDelay = Random.Range(SecBetweenShots - veriation , SecBetweenShots + SecBetweenShots);

            isAttacking = true;
            InvokeRepeating("FireProjectile", 1f,RandomDelay);
           

        }
        if (distanceToPlayer <= MoveRadius)
        {
            
            aiCharacterControl.SetTarget(player.transform);

        }
        else
        {
            aiCharacterControl.SetTarget(waypoint.transform);
        }
    }

    public void TakeDamage(float damage)
    {
        currenthHealthPoints = Mathf.Clamp(currenthHealthPoints - damage, 0f, maxHealthpoints);
        if (currenthHealthPoints <= 0) { Destroy(gameObject); }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255f, 0f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = new Color(0f, 255f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, MoveRadius);
    }


    // Set out fire logic!!!!
    void FireProjectile()
    {
        



        GameObject newProjectile = Instantiate(Projectile, ProjectileSocket.transform.position, Quaternion.identity);
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        projectileComponent.SetShooter(gameObject);
        projectileComponent.damageCaused = DMGperShot;
        Vector3 unitVectorToPlayer = (player.transform.position + aimOffset - ProjectileSocket.transform.position).normalized;
        float projectileSpeed = projectileComponent.GetDefaultLaunchSpeed();
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
    }
    private void IdleState_OverrideWeaponAnimatorController()
    {

        var animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animatorOverrideController;

        animatorOverrideController["DEFAULT_ATTACK"]        = enemyWeapon.GetCombatState_AttackAnimation();
        animatorOverrideController["DEFAULT_IDLE"]          = enemyWeapon.GetIdleState_IdleAnimClip();
        animatorOverrideController["DEFAULT_WALK"]          = enemyWeapon.GetIdleState_WalkAnimClip();
        animatorOverrideController["DEFAULT_RUN"]     = enemyWeapon.GetIdleState_WalkAnimClip();
       
      //  animatorOverrideController["DEFAULT_SHEATH"] = enemyWeapon.GetSeathAnimation();
    }
    private void OverrideWeaponAnimatorController_Combat()
    {

        var animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animatorOverrideController;

        animatorOverrideController["DEFAULT_ATTACK"]    = enemyWeapon.GetCombatState_AttackAnimation();
        animatorOverrideController["DEFAULT_IDLE"]      = enemyWeapon.GetCombatStrate_IdleAnimation();
        animatorOverrideController["DEFAULT_WALK"]      = enemyWeapon.GetCombatState_WalkAnimation();
        animatorOverrideController["DEFAULT_RUN"]       = enemyWeapon.GetCombatState_RunAnimation();
       
    }



    private void ResetAnimController()
    {
        var animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animatorOverrideController;

        animatorOverrideController["DEFAULT_ATTACK"] = null;
        animatorOverrideController["DEFAULT_IDLE"] = null;
        animatorOverrideController["DEFAULT_WALK"] = null;
        animatorOverrideController["DEFAULT_RUN"] = null;
    }
 


}