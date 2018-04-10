using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;

namespace RPG.Characters
{
    public class PowerAttackBehavior : MonoBehaviour, ISpecialAbility
    {


        PowerAttackConfig config;


        public void SetConfig(PowerAttackConfig conficToSet)
        {
            this.config = conficToSet;
        }

        public void Use(AbilityToUse useParams)
        {
            print("powerAttackUsed by "+ gameObject.name);
            float damageToDeal = useParams.baseDamage + config.getExtraDamage();
            useParams.target.TakeDamage(damageToDeal);

        }

        // Use this for initialization
        void Start()
        {
            print("powerAttack Behavior Attatched to " + gameObject.name);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


