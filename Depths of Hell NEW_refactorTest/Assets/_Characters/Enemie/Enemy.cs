
using RPG.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public class Enemy : MonoBehaviour, IDamageable
{

    [SerializeField]    float attackRadius      = 5f;
    [SerializeField]    float MoveRadius        = 2f;
    [SerializeField]    float DMGperShot        = 7f;
    [SerializeField]    float SecBetweenShots   = 7f;
    [SerializeField]    float maxHealthpoints   = 100f;

    [SerializeField]   GameObject Projectile;
    [SerializeField]   GameObject ProjectileSocket;

    [SerializeField]    Vector3 aimOffset = new Vector3(0, 1f, 0);

    bool isAttacking = false;
    float currenthHealthPoints;
    

    ThirdPersonCharacter thirdPersonCharacter = null;
    AICharacterControl aiCharacterControl = null; 

    GameObject player = null;
     
    public float healthAsPercentage { get { return currenthHealthPoints/(maxHealthpoints); }}

   void Start()
    {
        currenthHealthPoints = maxHealthpoints;
        aiCharacterControl = GetComponent<AICharacterControl>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= attackRadius&& !isAttacking)
        {
            isAttacking = true;
            InvokeRepeating("FireProjectile", 0f, SecBetweenShots);
       
        }
        if (distanceToPlayer <= MoveRadius)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else
        {
            aiCharacterControl.SetTarget(transform);
        }
    }

    public void TakeDamage(float damage)
    {
        currenthHealthPoints = Mathf.Clamp(currenthHealthPoints - damage, 0f, maxHealthpoints);
        if (currenthHealthPoints<= 0) { Destroy(gameObject); }
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
}
