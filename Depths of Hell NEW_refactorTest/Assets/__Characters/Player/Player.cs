using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
//using RPG.CameraUI; // TODO Consider rewire!

using UnityEngine.Assertions;
using RPG.Weapon;


namespace RPG.Character
{    
    public class Player : MonoBehaviour, IDamageable
    {

        //------------------------------------------------------------------------------------------//
        //          SerializeField                                                                  //
        //------------------------------------------------------------------------------------------// 

        [SerializeField]       float                        maxHealthpoints             = 100f                  ;                             
        [SerializeField]        float                       DMGpertHit                  = 9                     ;       
        [SerializeField]        AnimatorOverrideController  animatorOverrideController  = null                  ;
        [SerializeField]        Weapon.Weapon               weaponInUse                 = null                  ;

        //------------------------------------------------------------------------------------------//
        //          Public Variables                                                                //
        //------------------------------------------------------------------------------------------//    
        public                  bool                        onWeaponChange              = false                 ;
                       
        //------------------------------------------------------------------------------------------//
        //         Private Variables                                                                //
        //------------------------------------------------------------------------------------------//                      
                                float                       LastHitTime                 = 0f;
                                GameObject                  _weapon                     = null                  ;        
                                CameraRaycaster             cameraRaycaster                                     ;
                                AnimationEvent              ae                          = new AnimationEvent()  ;
                                float                       currenthHealthPoints                                ;

                                Animator                    animator                                            ;

        //------------------------------------------------------------------------------------------//
        //        Sets Health, ResetAnimarions, Instansiate Weapon in Sheath, sets Idle Animation   //
        //------------------------------------------------------------------------------------------// 

        private void Start()
        {         
            SetCurrentMaxHealth()   ;
            ResetAnimController()   ;
            InstantiateInSheath()   ;          
            RegisterForMouseClick() ;
            animator                                    = GetComponent<Animator>();        
            animatorOverrideController["DEFAULT_IDLE"]  = null;
        }
        private void SetCurrentMaxHealth()
        {
            currenthHealthPoints      = maxHealthpoints;
        }
        //------------------------------------------------------------------------------------------//
        //          Ovverides Animations to "in combat" Animations                                  //
        //------------------------------------------------------------------------------------------//
        private void OverrideWeaponAnimatorController()
        {

            var      animator         = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;
      
               animatorOverrideController["DEFAULT_ATTACK"] = weaponInUse.GetAttackAnimClip()   ;
               animatorOverrideController["DEFAULT_IDLE"]   = weaponInUse.GetIdleAnimClip()     ; 
               animatorOverrideController["DEFAULT_WALK"]   = weaponInUse.GetWalkAnimClip()     ;
               animatorOverrideController["DEFAULT_RUN"]    = weaponInUse.GetRunAnimClip()      ;   
               animatorOverrideController["DEFAULT_SHEATH"] = weaponInUse.GetSeathAnimation()   ;
        }

        //------------------------------------------------------------------------------------------//
        //       Reset Animation Controller to "out of combat" Animations                           //
        //------------------------------------------------------------------------------------------//
        private void ResetAnimController()
        {
            var animator                        = GetComponent<Animator>()  ;
            animator.runtimeAnimatorController  = animatorOverrideController;

            animatorOverrideController["DEFAULT_ATTACK"]    = null          ;
            animatorOverrideController["DEFAULT_IDLE"]      = null          ; 
            animatorOverrideController["DEFAULT_WALK"]      = null          ;
            animatorOverrideController["DEFAULT_RUN"]       = null          ;         
        }
        //------------------------------------------------------------------------------------------//
        //      Instaniate Weapon in Hand  & Set Parent to Hanb                                           //
        //------------------------------------------------------------------------------------------//
        private void InstantiateInhand()
        {
            var             weaponPrefab    = weaponInUse.GetWeaponPreFab()             ;
            GameObject      Hand            = RequestRHandTransform()                   ;
            var             weapon          = Instantiate(weaponPrefab, Hand.transform) ;
                            _weapon         = weapon                                    ;

            SetParentHand(Hand, weapon);
        }
        //------------------------------------------------------------------------------------------//
        //Instaniate Weapon in Sheath and set parent to Spine 2                                     //
        //------------------------------------------------------------------------------------------//

        private void  InstantiateInSheath()
        {
            var             weaponPrefab    = weaponInUse.GetWeaponPreFab()                 ;
            GameObject      Sheath          = RequestSheathTransform()                      ;
            var             weapon          = Instantiate(weaponPrefab, Sheath.transform)   ;
                            _weapon         = weapon;

            SetParentSheath(Sheath, weapon);
 
        }
        //------------------------------------------------------------------------------------------//
        //      Reset Animations and Destroy Weapon                                                 //
        //------------------------------------------------------------------------------------------//
        private void ResetWeapon()
        {
            ResetAnimController();
            Destroy     (_weapon);         
            InstantiateInSheath();
        }
        //------------------------------------------------------------------------------------------//
        //       Sets Parent to Weapon                                                              //
        //------------------------------------------------------------------------------------------//

        private void SetParentHand      (GameObject     Hand, GameObject    weapon)
        {
            weapon.transform.SetParent(Hand.transform);
            weapon.transform.localPosition  = weaponInUse.WeaponGrip.localPosition  ;
            weapon.transform.localRotation  = weaponInUse.WeaponGrip.localRotation  ;
            weapon.transform.localScale     = weaponInUse.WeaponGrip.localScale     ;
        }
        private void SetParentSheath    (GameObject     Sheath, GameObject      weapon)
        {
            weapon.transform.SetParent(Sheath.transform);
            weapon.transform.localPosition  = weaponInUse.SheathTransform.localPosition ;
            weapon.transform.localRotation  = weaponInUse.SheathTransform.localRotation ;
            weapon.transform.localScale     = weaponInUse.SheathTransform.localScale    ;
                                    _weapon = weapon;

        }
        //------------------------------------------------------------------------------------------//
        //      Finds Transform to set Weapon Parent                                                //
        //------------------------------------------------------------------------------------------//

        private GameObject RequestRHandTransform()
        {
            var         RightHand               = GetComponentsInChildren<FindRightHandOfPlayer>();
            int         numberOfDominantHands   = RightHand.Length;
          
            return      RightHand[0].gameObject;
            
        }
        private GameObject RequestLHandTransform()
        {
            var         LeftHand                = GetComponentsInChildren<FindLeftHandOfPlayer>();
            int         numberOfDominantHands   = LeftHand.Length;

            return          LeftHand[0].gameObject;
        }
      
        private GameObject RequestSheathTransform()
        {
            var                 SheathLocation  = GetComponentsInChildren<FindSeathTransform>() ;
            int         numberOfDominantHands   = SheathLocation.Length                         ;

            return          SheathLocation[0].gameObject                                        ;
        }

        //------------------------------------------------------------------------------------------//
        //       Sets Deligates                                                                     //
        //------------------------------------------------------------------------------------------//

        private void RegisterForMouseClick()
        {
            cameraRaycaster                         = FindObjectOfType<CameraRaycaster>()   ;
            cameraRaycaster.onOverPotentiallyEnemy += OnOverPotentiallyEnemy                ;
        }
        //------------------------------------------------------------------------------------------//
        //        Unsheath and Attack Enemy on click                                                //
        //------------------------------------------------------------------------------------------//
        void OnOverPotentiallyEnemy(Enemy enemy)
        {

            if (Input.GetMouseButtonDown(0))                                        {
                animator.SetTrigger            ("UnSheath");
                                                                                    }

            if (Input.GetMouseButtonDown(0) && IsTargetinRange(enemy.gameObject))   {
                AttackTarget                   (enemy);                             }                       
        }

        //------------------------------------------------------------------------------------------//
        //       Checks if target is in Range                                                       //
        //------------------------------------------------------------------------------------------//

        private bool IsTargetinRange(   GameObject  target   )
        {
            float               distanceTotarget         = (target.transform.position - transform.position).magnitude;
            return              (distanceTotarget       <= weaponInUse.GetMaxAttackRange());
        }

        //------------------------------------------------------------------------------------------//
        //         Player Take Damage                                                               //
        //------------------------------------------------------------------------------------------// 
        public float       healthAsPercentage  {get {return currenthHealthPoints/(maxHealthpoints);}}
        public      void        TakeDamage          (float damage)
        {
            currenthHealthPoints        = Mathf.Clamp(currenthHealthPoints - damage, 0f, maxHealthpoints);
        }

        //------------------------------------------------------------------------------------------//
        //      Player Attack Enemy                                                                 //
        //------------------------------------------------------------------------------------------// 
        private void AttackTarget(Enemy enemy)
        {      
            if (Time.time - LastHitTime > weaponInUse.GetMinTimeBetweenHits())
            {
                animator.SetTrigger ("Attack");
                enemy.TakeDamage    (DMGpertHit);

                LastHitTime = Time.time;
            }
        }
        //------------------------------------------------------------------------------------------//
        //      The exact moment the hand reaches the weapon while Un sheathing
        //      Sets New Parent to weapon reset ANimations to "out of combat"//
        //------------------------------------------------------------------------------------------//
        public void UnSheathMoment()
        {
            OverrideWeaponAnimatorController();
            SetParentHand(RequestRHandTransform(), _weapon);
            onWeaponChange = false;
        }
        //------------------------------------------------------------------------------------------//
        //      The exact moment the hand reaches the weapon while Sheathing                        //
        //      Sets New Parent to weapon reset ANimations to "out of combat"                       //
        //------------------------------------------------------------------------------------------//

        public void SheathMoment()
        {         
            SetParentSheath(RequestSheathTransform(), _weapon);
            ResetAnimController();
            onWeaponChange = true;
        }
        //------------------------------------------------------------------------------------------//
        //      Changes Weapon on runtime, REset animation Instaniate new weapon                    //      
        //------------------------------------------------------------------------------------------//

        public void OnValidate()
        {
            if (onWeaponChange == true)
            {
                ResetWeapon();
            }                 
        }


    }
}
    
