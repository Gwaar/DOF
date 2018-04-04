using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using RPG.CameraUI; // TODO Consider rewire!

using UnityEngine.Assertions;
using RPG.Weapon;


namespace RPG.Character
{
    
    public class Player : MonoBehaviour, IDamageable
    {
        bool WeaponInHand = false;

        GameObject myWeapon;

      
        bool InstansiateWeapon =false;
        bool IstansiateSheath = false;


        [SerializeField]
        Weapon.Weapon weaponToInstasiate = null;
     


        [SerializeField]
        int enemyLayer = 9;
        [SerializeField]
        float DMGpertHit = 9;
        [SerializeField]
        float minTimeBetweenHit = 2f;
        [SerializeField]
        float maxattackRange = 2f;




       

        [SerializeField]
        AnimatorOverrideController animatorOverrideController = null;


        bool weaponEquipped = false;


        Animator anim;

        CameraRaycaster cameraRaycaster;
        float LastHitTime = 0f;

        AnimationEvent ae = new AnimationEvent();
        private void Start()
        {
           
            ResetAnimationController();

            InstanateWeapon(true);

            animatorOverrideController["DEFAULT_UNSHEATH"] = weaponToInstasiate.GetUnSeathAnimation();




            RegisterForMouseClick();
        
            anim = GetComponent<Animator>();

            SetCurrentMaxHealth();
            cameraRaycaster.notifyMouseClickObservers += OnMouseClicked;

        }
        private void SetCurrentMaxHealth()
        {
            currenthHealthPoints = maxHealthpoints;
        }
        private void OverrideWeaponAnimatorController()
        {

            var animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;
      
                animatorOverrideController["DEFAULT_ATTACK"] = weaponToInstasiate.GetAttackAnimClip();
               animatorOverrideController["DEFAULT_IDLE"] = weaponToInstasiate.GetIdleAnimClip(); // remove const
               animatorOverrideController["DEFAULT_WALK"] = weaponToInstasiate.GetWalkAnimClip();
               animatorOverrideController["DEFAULT_RUN"] = weaponToInstasiate.GetRunAnimClip();   
                 animatorOverrideController["DEFAULT_SHEATH"] = weaponToInstasiate.GetSeathAnimation();

          


            // TODO FIX

        }
        private void ResetAnimationController()
        {
            print("reseted");
            var animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;
       
            animatorOverrideController["DEFAULT_ATTACK"] = null;
            animatorOverrideController["DEFAULT_IDLE"] = null; // remove const
            animatorOverrideController["DEFAULT_WALK"] = null;
            animatorOverrideController["DEFAULT_RUN"] = null;
            animatorOverrideController["DEFAULT_SHEATH"] = null; // TODO FIX
            animatorOverrideController["DEFAULT_UNSHEATH"] = null; // TODO FIX


        }





        public void InstanateWeapon(bool IsSheathed = false)
        {
           
            var weaponPrefab = weaponToInstasiate.GetWeaponPreFab();

            switch (IsSheathed)
            {
                case true:
                    GameObject Sheath = RequestSheathTransform();
                    var weapon = Instantiate(weaponPrefab, Sheath.transform);
                    SetParentSeath(Sheath, weapon);
                    myWeapon = weapon;
                    break;
                case false:

                    GameObject Hand = RequestHandTransform();
                    weapon = Instantiate(weaponPrefab, Hand.transform);
                    SetParentHand(Hand, weapon);
                    print("");
                    break;
                default:
                    break;                
            }

         
        }
                
                      
        
        private void SetParentHand(GameObject Hand, GameObject weapon)
        {
            weapon.transform.localPosition = weaponToInstasiate.WeaponGrip.localPosition;
            weapon.transform.localRotation = weaponToInstasiate.WeaponGrip.localRotation;
            weapon.transform.localScale =    weaponToInstasiate.WeaponGrip.localScale;
            print(weaponToInstasiate.WeaponGrip.localPosition);
            print(weaponToInstasiate.WeaponGrip.localRotation);


            print("partentHand");

            weapon.transform.SetParent(Hand.transform);
        }

        private void SetParentSeath(GameObject Sheath, GameObject weapon)
        {
            print("partent");
            weapon.transform.localPosition = weaponToInstasiate.SheathTransform.localPosition;
            weapon.transform.localRotation = weaponToInstasiate.SheathTransform.localRotation;
            weapon.transform.localScale =    weaponToInstasiate.SheathTransform.localScale;

            weapon.transform.SetParent(Sheath.transform);


        }


        private GameObject RequestHandTransform()
        {
            var RightHand = GetComponentsInChildren<FindRightHandOfPlayer>();
            int numberOfDominantHands = RightHand.Length;
            return RightHand[0].gameObject;
        }

        private GameObject RequestSheathTransform()
        {
            var SheathLocation = GetComponentsInChildren<FindSeathTransform>();
            int numberOfDominantHands = SheathLocation.Length;
            return SheathLocation[0].gameObject;
        }

   

        private void RegisterForMouseClick()
        {
            cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        }
        [SerializeField]
        float maxHealthpoints = 100f;
        float currenthHealthPoints;
        public float healthAsPercentage { get { return currenthHealthPoints / (maxHealthpoints); } }
        public void TakeDamage(float damage)
        {
            currenthHealthPoints = Mathf.Clamp(currenthHealthPoints - damage, 0f, maxHealthpoints);
        }
        void OnMouseClicked(RaycastHit raycastHit, int layerHit)
        {
            if (layerHit == 9)
            {
                var enemy = raycastHit.collider.gameObject;

                if ((enemy.transform.position - transform.position).magnitude > maxattackRange)
                {
                    return;
                }

                var enemyComponent = enemy.GetComponent<Enemy>();
                if (Time.time - LastHitTime > minTimeBetweenHit)
                {
                    enemyComponent.TakeDamage(DMGpertHit);
                    LastHitTime = Time.time;
                }
            }
        }
      
        public void UnSheathMoment()
        {       
            SetParentHand(RequestHandTransform(), myWeapon);
        }

        public void SheathMoment()
        {

            SetParentSeath(RequestSheathTransform(), myWeapon);
        }   

        public void WeaponDrawn()
        {
          //  
        }


        public void WeaponSheathed()
        {
          //  ResetAnimationController();
        }




    }
}
    
