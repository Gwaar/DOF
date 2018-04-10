using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{

  
    public abstract class SpecialAbility : ScriptableObject
    {


        [Header("Special Ability General")]
        [SerializeField]        float energyCost = 10f;

        protected ISpecialAbility behavior;


        abstract public void  AttatchComponentTo(GameObject gameObjectToAttachTo);

        public void Use()
        {
            behavior.Use();
        }
    }
    public interface ISpecialAbility
        {
            void Use();
        }
   
}

