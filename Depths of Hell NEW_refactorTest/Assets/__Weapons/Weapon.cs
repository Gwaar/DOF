
using       System.Collections;
using       System.Collections.Generic;
using       UnityEngine;
using RPG.Character;
using RPG.Characters;

namespace   RPG.Weapon


{
    [CreateAssetMenu(menuName = ("RPG/Weapon"))]
    
    public class Weapon : ScriptableObject 
    {




        public              bool                                                    isLeftHand              = false ;
 
        public              bool                                                    isOneHand               = false;



        public              Transform                                               WeaponGrip              = null  ;       
        public              Transform                                               SheathTransform         = null  ;
        [SerializeField]    float                                                   minTimeBetweenHit       = 2f    ;
        [SerializeField]    float                                                   maxattackRange          = 2f    ;
        [SerializeField]    GameObject                                              weaponPrefab                    ;
        [SerializeField]    AnimationClip[]                                         attackAnimations                ;
        [SerializeField]    AnimationClip                                          attackAnimation                ;
        [SerializeField]    AnimationClip                                           idleAnimation                   ;
        [SerializeField]    AnimationClip                                           WalkAnimation                   ;
        [SerializeField]    AnimationClip                                           RunAnimation                    ;
        [SerializeField]    AnimationClip                                           SheathAnimation                  ;
        [SerializeField]    AnimationClip                                           UnSheathAnimation               ;

        //------------------------------------------------------------------------------------------//
        //      Getter and Setters                                                                  //
        //------------------------------------------------------------------------------------------//


       



        public               float          GetMinTimeBetweenHits()     {return     minTimeBetweenHit               ;}
        public               float          GetMaxAttackRange()         {return     maxattackRange                  ;}
        public               GameObject     GetWeaponPreFab()           {return     weaponPrefab                    ;}
        public               AnimationClip[]  GetAttackAnimClips()         {return     attackAnimations               ;}   

         public               AnimationClip  GetAttackAnimClip()         {   return SetAttackAnimationToUse()          ;} 

        public               AnimationClip  GetIdleAnimClip()           {return     idleAnimation                   ;}
        public               AnimationClip  GetWalkAnimClip()           {return     WalkAnimation                   ;}
        public               AnimationClip  GetRunAnimClip()            {return     RunAnimation                    ;}                
        public               AnimationClip  GetSheathAnimation()         {return     SheathAnimation                  ;}        
        public               AnimationClip  GetUnSeathAnimation()       {return     UnSheathAnimation               ;}

        

        AnimationClip SetAttackAnimationToUse(){

                     attackAnimation = attackAnimations[UnityEngine.Random.Range(0, attackAnimations.Length)];
                    return attackAnimation;
        }


    }

    
}

    
