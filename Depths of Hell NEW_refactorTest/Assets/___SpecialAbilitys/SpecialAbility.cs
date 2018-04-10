using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;
namespace RPG.Characters
{
public  struct AbilityToUse
    {
        public IDamageable target;
        public float baseDamage;
       
        public AbilityToUse(IDamageable target, float baseDamage)
        {
            this.target     = target;
            this.baseDamage = baseDamage;
           
        }
    }
  
    public abstract class SpecialAbility : ScriptableObject
    {


        [Header("Special Ability General")]
        [SerializeField]  float energyCost;      

        protected ISpecialAbility behavior;


        abstract public void  AttatchComponentTo(GameObject gameObjectToAttachTo);

        public float GetEnergyCost()
        {
            return energyCost;
        }



        public void Use(AbilityToUse useParams)
        {
            behavior.Use(useParams);
        }
    }
    public interface ISpecialAbility
        {
            void Use(AbilityToUse useParams);
        }
   
}

