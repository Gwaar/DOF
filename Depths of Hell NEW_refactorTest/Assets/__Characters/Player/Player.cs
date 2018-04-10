using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
//using RPG.CameraUI; // TODO Consider rewire!

using UnityEngine.Assertions;
using RPG.Weapon;



using RPG.Characters;

namespace RPG.Character
{
    public class Player : MonoBehaviour, IDamageable
    {


        //------------------------------------------------------------------------------------------//
        //          SerializeField                                                                  //
        //------------------------------------------------------------------------------------------// 


        //TEMP for debug
        [SerializeField]
        SpecialAbility[] abilities;



        [SerializeField]
        float maxHealthpoints = 100f;
        [SerializeField]
        float baseDamage = 9;
        [SerializeField]
        AnimatorOverrideController animatorOverrideController = null;
        [SerializeField]
        Weapon.Weapon weaponInUse = null;
        [SerializeField]
        Weapon.Weapon OffHandWeaponInUse = null;


        //------------------------------------------------------------------------------------------//
        //          Public Variables                                                                //
        //------------------------------------------------------------------------------------------//    
        public bool onWeaponChange = false;
        




        //------------------------------------------------------------------------------------------//
        //         Private Variables                                                                //
        //------------------------------------------------------------------------------------------//                      
        float LastHitTime = 0f;
        GameObject _weapon = null;

        GameObject _OffHand = null;


        CameraRaycaster cameraRaycaster;
        AnimationEvent ae = new AnimationEvent();
        float currenthHealthPoints;

        Animator animator;


        bool OffHandUnsheath = false;
        bool mainHandUnSheath = true;

        //------------------------------------------------------------------------------------------//
        //        Sets Health, ResetAnimarions, Instansiate Weapon in Sheath, sets Idle Animation   //
        //------------------------------------------------------------------------------------------// 

        private void Start()
        {
           
            ResetAnimController();
            animatorOverrideController["DEFAULT_UNSHEATH_OFFHAND"]  = OffHandWeaponInUse.GetUnSeathAnimation();
            animatorOverrideController["DEFAULT_UNSHEATH"]          = weaponInUse.GetUnSeathAnimation();         
            SetCurrentMaxHealth();        
            InstantiateInSheath();
            RegisterForMouseClick();          
            animator = GetComponent<Animator>();
            animatorOverrideController["DEFAULT_IDLE"] = null;

            abilities[0].AttatchComponentTo(gameObject);


        }
        private void SetCurrentMaxHealth()
        {
            currenthHealthPoints = maxHealthpoints;
        }
        //------------------------------------------------------------------------------------------//
        //          Ovverides Animations to "in combat" Animations                                  //
        //------------------------------------------------------------------------------------------//
        private void OverrideWeaponAnimatorController()
        {
           
            var animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;

            if (weaponInUse.isOneHand == false)
            {
                animatorOverrideController["DEFAULT_ATTACK"] = weaponInUse.GetAttackAnimClip();
                animatorOverrideController["DEFAULT_IDLE"] = weaponInUse.GetIdleAnimClip();
                animatorOverrideController["DEFAULT_WALK"] = weaponInUse.GetWalkAnimClip();
                animatorOverrideController["DEFAULT_RUN"] = weaponInUse.GetRunAnimClip();
                animatorOverrideController["DEFAULT_SHEATH"] = weaponInUse.GetSeathAnimation();

            }
            else
            {
                
                if (mainHandUnSheath == true)
                {

                    print("mainHandAnims!");

                    animatorOverrideController["DEFAULT_ATTACK"] = weaponInUse.GetAttackAnimClip();
                    animatorOverrideController["DEFAULT_IDLE"] = weaponInUse.GetIdleAnimClip();
                    animatorOverrideController["DEFAULT_WALK"] = weaponInUse.GetWalkAnimClip();
                    animatorOverrideController["DEFAULT_RUN"] = weaponInUse.GetRunAnimClip();
              //      animatorOverrideController["DEFAULT_SHEATH"] = weaponInUse.GetSeathAnimation();
                    animatorOverrideController["DEFAULT_UNSHEATH_OFFHAND"] = OffHandWeaponInUse.GetUnSeathAnimation();
                    animatorOverrideController["DEFAULT_UNSHEATH"] = weaponInUse.GetUnSeathAnimation();

                    mainHandUnSheath = false;
                }
          
  
               else
                {
                    
                    if (OffHandUnsheath == false)
                    {
                        print("offhandAnims");
                    //    ResetAnimController();
                        animatorOverrideController["DEFAULT_ATTACK"] = OffHandWeaponInUse.GetAttackAnimClip();
                        animatorOverrideController["DEFAULT_IDLE"] = OffHandWeaponInUse.GetIdleAnimClip();
                        animatorOverrideController["DEFAULT_WALK"] = OffHandWeaponInUse.GetWalkAnimClip();
                        animatorOverrideController["DEFAULT_RUN"] = OffHandWeaponInUse.GetRunAnimClip();
                        animatorOverrideController["DEFAULT_UNSHEATH"] = OffHandWeaponInUse.GetUnSeathAnimation();
                        animatorOverrideController["DEFAULT_UNSHEATH_OFFHAND"] = OffHandWeaponInUse.GetUnSeathAnimation();
                        //       animatorOverrideController["DEFAULT_SHEATH"] = OffHandWeaponInUse.GetSeathAnimation();


                        OffHandUnsheath = true;
                    }
                
          
                }

            }

        
         




        }


        //------------------------------------------------------------------------------------------//
        //       Reset Animation Controller to "out of combat" Animations                           //
        //------------------------------------------------------------------------------------------//
        private void ResetAnimController()
        {
            var animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;

            animatorOverrideController["DEFAULT_ATTACK"] = null;
            animatorOverrideController["DEFAULT_IDLE"] = null;
            animatorOverrideController["DEFAULT_WALK"] = null;
            animatorOverrideController["DEFAULT_RUN"] = null;
        }
        //------------------------------------------------------------------------------------------//
        //      Instaniate Weapon in Hand  & Set Parent to Hanb                                           //
        //------------------------------------------------------------------------------------------//
        private void InstantiateInhand()
        {
            var weaponPrefab = weaponInUse.GetWeaponPreFab();
            GameObject Hand = RequestRHandTransform();
            var weapon = Instantiate(weaponPrefab, Hand.transform);
            _weapon = weapon;




            SetParentHand(Hand, weapon);
            animator.SetBool("WeaponSheathed", false);
            animator.SetBool("WeaponDrawn", true);
        }
        //------------------------------------------------------------------------------------------//
        //Instaniate Weapon in Sheath and set parent to Spine 2                                     //
        //------------------------------------------------------------------------------------------//

        private void InstantiateInSheath()
        {

            if (weaponInUse.isOneHand == false)
            {
                var weaponPrefab = weaponInUse.GetWeaponPreFab();

                GameObject Sheath = RequestSheathTransform();

                var weapon = Instantiate(weaponPrefab, Sheath.transform);
                _weapon = weapon;


                SetParentSheath(Sheath, weapon);

                animator.SetBool("WeaponSheathed", true);
                animator.SetBool("WeaponDrawn", false);

            }
            else
            {
                var weaponPrefab = weaponInUse.GetWeaponPreFab();
                var OffhandWeaponInUse = OffHandWeaponInUse.GetWeaponPreFab();

                GameObject Sheath = RequestSheathTransform();

                var weapon = Instantiate(weaponPrefab, Sheath.transform);
                var offhand = Instantiate(OffhandWeaponInUse, Sheath.transform);
                _weapon = weapon;
                _OffHand = offhand;

                SetParentSheathOffHand(Sheath, offhand);
                SetParentSheath(Sheath, weapon);
               

                //animator.SetBool("WeaponSheathed", true);
                //animator.SetBool("WeaponDrawn", false);




            }




        }

        //------------------------------------------------------------------------------------------//
        //      Reset Animations and Destroy Weapon                                                 //
        //------------------------------------------------------------------------------------------//
        private void OnWeaponSwap()
        {

           
            if (weaponInUse.isOneHand == false)
            {
                ResetAnimController();
                Destroy(_weapon);
                InstantiateInSheath();
            }
            else
            {
                ResetAnimController();
                Destroy(_weapon);
                Destroy(_OffHand);
                InstantiateInSheath();
                InstantiateInSheath();
            }

        }
        //------------------------------------------------------------------------------------------//
        //       Sets Parent to Weapon                                                              //
        //------------------------------------------------------------------------------------------//

        private void SetParentHand(GameObject Hand, GameObject weapon)
        {
            weapon.transform.SetParent(Hand.transform);
            weapon.transform.localPosition = weaponInUse.WeaponGrip.localPosition;
            weapon.transform.localRotation = weaponInUse.WeaponGrip.localRotation;
            weapon.transform.localScale = weaponInUse.WeaponGrip.localScale;
        }
        private void SetParentSheath(GameObject Sheath, GameObject weapon)
        {

            weapon.transform.SetParent(Sheath.transform);
            weapon.transform.localPosition = weaponInUse.SheathTransform.localPosition;
            weapon.transform.localRotation = weaponInUse.SheathTransform.localRotation;
            weapon.transform.localScale = weaponInUse.SheathTransform.localScale;
            _weapon = weapon;

        }


        private void SetParentSheathOffHand(GameObject Sheath, GameObject Offhand)
        {

            Offhand.transform.SetParent(Sheath.transform);
            Offhand.transform.localPosition =   OffHandWeaponInUse.SheathTransform.localPosition;
            Offhand.transform.localRotation =   OffHandWeaponInUse.SheathTransform.localRotation;
            Offhand.transform.localScale    =   OffHandWeaponInUse.SheathTransform.localScale;
            _OffHand = Offhand;

        }
        private void SetParentHandOffHand(GameObject Sheath, GameObject Offhand)
        {

            Offhand.transform.SetParent(Sheath.transform);
            Offhand.transform.localPosition = OffHandWeaponInUse.WeaponGrip.localPosition;
            Offhand.transform.localRotation = OffHandWeaponInUse.WeaponGrip.localRotation;
            Offhand.transform.localScale = OffHandWeaponInUse.WeaponGrip.localScale;
          //  print("gotHere!");

        }


        //------------------------------------------------------------------------------------------//
        //      Finds Transform to set Weapon Parent                                                //
        //------------------------------------------------------------------------------------------//

        private GameObject RequestRHandTransform()
        {
            var         RightHand               = GetComponentsInChildren<FindRightHandOfPlayer>();
            int         numberOfDominantHands   = RightHand.Length;
            print("RightHandSet");
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
            if (Input.GetMouseButton(0) && IsTargetinRange(enemy.gameObject))
            {

                animator.SetTrigger("UnSheath");
                AttackTarget(enemy);
            }else if
                (Input.GetMouseButtonDown(1))
                {
                AttemptSpecialAbility(0, enemy);
                }
            }

        private void AttemptSpecialAbility(int abilityIndex , Enemy enemy)
        {
            var EnergyComponent = GetComponent<Energy>();
            var EnergyCost = abilities[abilityIndex].GetEnergyCost();

            if (EnergyComponent.IsEnergyAvailable(EnergyCost)) // TODO REMOVE HARDCODED NUMBER
            {
                EnergyComponent.ConsumeEnergy(EnergyCost); // TODO USE ABILITY!

                var abilityPerams = new AbilityToUse(enemy, baseDamage);
                abilities[abilityIndex].Use(abilityPerams);
            }
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
                enemy.TakeDamage    (baseDamage);

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

            if(weaponInUse.isLeftHand == true)
            {
                    
                    SetParentHand(RequestLHandTransform(), _weapon);
                    onWeaponChange = false;
            }
            else
            {
                print("RighHandSet");
                SetParentHand(RequestRHandTransform(), _weapon);
                onWeaponChange = false;
            }       
        }

        public void UnSheathOffHandMoment()
        {
            SetParentHandOffHand(RequestLHandTransform(), _OffHand);

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
                OnWeaponSwap();
            }                 
        }

        private void Update()
        {
            if (Input.GetKey("1"))
                
                animator.SetTrigger("Attack");
                
            if (Input.GetKey("2"))
                animator.SetTrigger("UnSeath");

           

        }
    }
    
  

}

