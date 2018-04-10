using RPG.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Character

{
    [CreateAssetMenu(menuName =("RPG/Special Ability/Power Attack"))]

    public class PowerAttackConfig : SpecialAbility
    {
        [Header("Power Attack Spesific")]
        [SerializeField]        float ExtraDamage = 10f;

        public override void AttatchComponentTo(GameObject gameObjectToAttachTo)
        {
            var behaviorComponent = gameObjectToAttachTo.AddComponent<PowerAttackBehavior>();
            behaviorComponent.SetConfig(this);
            behavior =  behaviorComponent;
        }

        public float getExtraDamage() { return ExtraDamage; }
    }
}



