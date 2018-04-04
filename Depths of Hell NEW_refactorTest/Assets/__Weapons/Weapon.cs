using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Weapon

{
    [CreateAssetMenu(menuName = ("RPG/Weapon"))]
    
    public class Weapon : ScriptableObject 

    {


        public bool isWeaponSeathed;

        public              Transform       WeaponGrip          =   null;       
        public              Transform       SheathTransform     = null;

        [SerializeField]    GameObject    weaponPrefab;
        [SerializeField]    AnimationClip attackAnimation;
        [SerializeField]    AnimationClip idleAnimation;
        [SerializeField]    AnimationClip WalkAnimation;
        [SerializeField]    AnimationClip RunAnimation;

        [SerializeField]       AnimationClip SeathAnimation;
        [SerializeField]       AnimationClip UnSeathAnimation;

        







        public GameObject GetWeaponPreFab()
        {
            return weaponPrefab;
        }
        public  AnimationClip GetAttackAnimClip()
        {
            //  removeAnimationEvents();  
            return attackAnimation;
            
        }

       // private void removeAnimationEvents()
      //  {
          //  // so that Assetbugs dont cause Crashes
            //attackAnimation.events = new AnimationEvent[0];
    //    }

        public AnimationClip GetIdleAnimClip()
        {
            return idleAnimation;
        }
        public AnimationClip GetWalkAnimClip()
        {
            return WalkAnimation;
        }

        public AnimationClip GetRunAnimClip()
        {
            return RunAnimation;
        }

        
       
       

        
        public AnimationClip GetSeathAnimation()
        {
            return SeathAnimation;
        }
        
        public AnimationClip GetUnSeathAnimation()
        {
            return UnSeathAnimation;
        }
        




}
}

    
