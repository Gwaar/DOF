using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   [SerializeField] GameObject shooter;
    [SerializeField]
    float ProjectileSpeed;
    public float damageCaused;

    const float DESTROY_DELAY = 0.01f;

    public void SetShooter(GameObject shooter)
    {
        this.shooter = shooter;
    }

    public void SetDamage(float damage)
    {
        damageCaused = damage;
    }
    void OnCollisionEnter(Collision collision)
    {
        var layerCollidewith = collision.gameObject.layer;     
        if (layerCollidewith != shooter.layer)
        {
            DamageDamageables(collision);
        }  
    }

    private void DamageDamageables(Collision collision)
    {
        Component damageableComponent = collision.gameObject.GetComponent(typeof(IDamageable));
        if (damageableComponent)
        {
            (damageableComponent as IDamageable).TakeDamage(damageCaused);
        }
        Destroy(gameObject, DESTROY_DELAY);
        }
        public float GetDefaultLaunchSpeed()
        {
            return ProjectileSpeed;
        }
    }
