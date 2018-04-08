
using       System.Collections;
using       System.Collections.Generic;
using       UnityEngine;
namespace   RPG.Weapon

{
    [CreateAssetMenu(menuName = ("RPG/EnemyWeapon"))]
    
    public class EnemyWeapon : ScriptableObject 
    {
      
        public              Transform                                               TransformToInstansiate              = null  ;       
      
        [SerializeField]    float                                                   minTimeBetweenHit       = 2f    ;
        [SerializeField]    float                                                   maxattackRange          = 2f    ;

   
        [SerializeField]    GameObject                                              EnemyPrefabtoInstansiate                    ;

        //------------------------------------------------------------------------------------------//
        //     Idle State                                                                           //
        //------------------------------------------------------------------------------------------//


        [SerializeField]    AnimationClip                                           Idlestate_IdleAnimation                   ;
        [SerializeField]    AnimationClip                                           IdleState_WalkAnimation                   ;
        [SerializeField]    AnimationClip                                           IdleState_SocialAnimation                  ;

        //------------------------------------------------------------------------------------------//
        //      Combat State                                                                       //
        //------------------------------------------------------------------------------------------//

        [SerializeField]        AnimationClip                                        CombatState_AttackAnimation             ;
        [SerializeField]        AnimationClip                                        CombatStrate_IdleAnimation               ;
        [SerializeField]        AnimationClip                                        CombatState_WalkAnimation               ;
        [SerializeField]        AnimationClip                                        CombatState_RunAnimation                ;
        [SerializeField]        AnimationClip                                        CombatState_AggroAnimation;

        //------------------------------------------------------------------------------------------//
        //      Getter and Setters                                                                  //
        //------------------------------------------------------------------------------------------//

        public               float          GetMinTimeBetweenHits()     {return     minTimeBetweenHit               ;}
        public               float          GetMaxAttackRange()         {return     maxattackRange                  ;}
       

        public GameObject     GetWeaponPreFab()                         {return     EnemyPrefabtoInstansiate                    ;}
        //------------------------------------------------------------------------------------------//
        //     IdleState                                                                    //
        //------------------------------------------------------------------------------------------//


        public AnimationClip GetIdleState_IdleAnimClip() { return Idlestate_IdleAnimation       ; }
        public AnimationClip GetIdleState_WalkAnimClip() { return IdleState_WalkAnimation       ; }

        //------------------------------------------------------------------------------------------//
        //     CombatState                                                                          //
        //------------------------------------------------------------------------------------------//
        public AnimationClip GetCombatState_AggroAnimation() { return CombatState_AggroAnimation; }
        public AnimationClip GetCombatState_AttackAnimation() { return CombatState_AttackAnimation; }
        public AnimationClip GetCombatStrate_IdleAnimation() { return CombatStrate_IdleAnimation; }
        public AnimationClip GetCombatState_WalkAnimation() { return CombatState_WalkAnimation; }
        public AnimationClip GetCombatState_RunAnimation() { return CombatState_RunAnimation; }

        //------------------------------------------------------------------------------------------//
        //     GetHit                                                                          //
        //------------------------------------------------------------------------------------------//




        //------------------------------------------------------------------------------------------//
        //     Alerted State                                                                          //
        //------------------------------------------------------------------------------------------//






        //   public AnimationClip GetIdleState_SocialAnimClip() { return IdleState_SocialAnimation   ; }
        //[SerializeField]    AnimationClip                                           RunAnimation                    ;
        //[SerializeField]    AnimationClip                                           SeathAnimation                  ;
        //[SerializeField]    AnimationClip                                           UnSheathAnimation               ;
        //[SerializeField]        AnimationClip                                        SeathAnimation              ;
        //[SerializeField]        AnimationClip                                        UnSheathAnimation           ;

    }
}

    
