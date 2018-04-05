
using       System.Collections;
using       System.Collections.Generic;
using       UnityEngine;
namespace   RPG.Weapon

{
    [CreateAssetMenu(menuName = ("RPG/Weapon"))]
    
    public class Weapon : ScriptableObject 
    {
        public              bool                                                    isLeftHand              = false ;
        public              Transform                                               WeaponGrip              = null  ;       
        public              Transform                                               SheathTransform         = null  ;
        [SerializeField]    float                                                   minTimeBetweenHit       = 2f    ;
        [SerializeField]    float                                                   maxattackRange          = 2f    ;
        [SerializeField]    GameObject                                              weaponPrefab                    ;
        [SerializeField]    AnimationClip                                           attackAnimation                 ;
        [SerializeField]    AnimationClip                                           idleAnimation                   ;
        [SerializeField]    AnimationClip                                           WalkAnimation                   ;
        [SerializeField]    AnimationClip                                           RunAnimation                    ;
        [SerializeField]    AnimationClip                                           SeathAnimation                  ;
        [SerializeField]    AnimationClip                                           UnSheathAnimation               ;

        //------------------------------------------------------------------------------------------//
        //      Getter and Setters                                                                  //
        //------------------------------------------------------------------------------------------//
        public               float          GetMinTimeBetweenHits()     {return     minTimeBetweenHit               ;}
        public               float          GetMaxAttackRange()         {return     maxattackRange                  ;}
        public               GameObject     GetWeaponPreFab()           {return     weaponPrefab                    ;}
        public               AnimationClip  GetAttackAnimClip()         {return     attackAnimation                 ;}      
        public               AnimationClip  GetIdleAnimClip()           {return     idleAnimation                   ;}
        public               AnimationClip  GetWalkAnimClip()           {return     WalkAnimation                   ;}
        public               AnimationClip  GetRunAnimClip()            {return     RunAnimation                    ;}                
        public               AnimationClip  GetSeathAnimation()         {return     SeathAnimation                  ;}        
        public               AnimationClip  GetUnSeathAnimation()       {return     UnSheathAnimation               ;}
    }
}

    
