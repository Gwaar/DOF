using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;

namespace RPG.Characters
{
    public class AoEBehavior : MonoBehaviour, ISpecialAbility
    {
        AoEConfig config;
        public void SetConfig(AoEConfig conficToSet)
        {
            config = conficToSet;
        }

        public void Use(AbilityToUse useParams)
        {
            //static SPhere cast for target
            RaycastHit[] hits = Physics.SphereCastAll(
                transform.position,
                config.getAoeRadius(), 
                Vector3.up, 
                config.getAoeRadius());

            foreach (RaycastHit hit in hits)
            {
                var damageble = hit.collider.gameObject.GetComponent<IDamageable>();
                if(damageble!= null)
                {
                    float DamageToDeal = useParams.baseDamage + config.getDamageToEachTarget();

                    damageble.TakeDamage(DamageToDeal);
                }
            }




         
            float damageToDeal = useParams.baseDamage + config.getDamageToEachTarget();
            useParams.target.TakeDamage(damageToDeal);

        }

        // Use this for initialization
        void Start()
        {
            print("AoEehavior Attatched to " + gameObject.name);

        }

        // Update is called once per fra e
        //void Update()
        //{
        //    {
        //        RaycastHit hit;
        //        Vector3 AoeCenter = new Vector3 aeCenter;
        //        Vector3 p1 = transform.position + .center;
        //        float distanceToObstacle = 0;

        //        if (Physics.SphereCast(p1, charCtrl.height / 2, transform.forward, out hit, 10))
        //        {
        //            distanceToObstacle = hit.distance;
        //        }
        //    }
        //}
    }
}


