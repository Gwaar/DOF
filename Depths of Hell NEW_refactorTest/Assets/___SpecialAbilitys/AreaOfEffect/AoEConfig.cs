using RPG.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Character

{
    [CreateAssetMenu(menuName = ("RPG/Special Ability/AreaOfEffect"))]

    public class AoEConfig : SpecialAbility
    {
        [Header("AoE Attack Spesific")]
        [SerializeField]
        float DamageToEachTarget = 10f;
        float AoeRadius = 5f;

        public override void AttatchComponentTo(GameObject gameObjectToAttachTo)
        {
            var behaviorComponent = gameObjectToAttachTo.AddComponent<AoEBehavior>();
            behaviorComponent.SetConfig(this);
            behavior = behaviorComponent;
        }

        public float getDamageToEachTarget() { return DamageToEachTarget; }

        public float getAoeRadius() { return AoeRadius; }
    }
}



