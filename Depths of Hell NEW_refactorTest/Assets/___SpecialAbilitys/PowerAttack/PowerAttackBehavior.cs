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

        public void Use()
        {
            print("powerAttackUsed");
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


