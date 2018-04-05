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

     
        [SerializeField]        int                         enemyLayer                  = 9;
        [SerializeField]        float                       DMGpertHit                  = 9;
       
        [SerializeField]        AnimatorOverrideController  animatorOverrideController  = null;
        [SerializeField]        Weapon.Weapon               weaponInUse          = null;
       
        public                  bool                        onWeaponChange              = false;       
                                float                       LastHitTime                 = 0f;
                                GameObject                  _weapon                     = null;        
                                CameraRaycaster             cameraRaycaster;
        AnimationEvent                                      ae                          = new AnimationEvent();

         Animator               animator;



        private void Start()
        {
            ResetAnimController();
            InstantiateInSheath();
            animatorOverrideController["DEFAULT_IDLE"] = null;
            RegisterForMouseClick();
            animator = GetComponent<Animator>();
            SetCurrentMaxHealth();          

        }
        private void SetCurrentMaxHealth()
        {
            currenthHealthPoints = maxHealthpoints;
        }
        private void OverrideWeaponAnimatorController()
        {

            var animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;
      
               animatorOverrideController["DEFAULT_ATTACK"] = weaponInUse.GetAttackAnimClip();
               animatorOverrideController["DEFAULT_IDLE"] = weaponInUse.GetIdleAnimClip(); // remove const
               animatorOverrideController["DEFAULT_WALK"] = weaponInUse.GetWalkAnimClip();
               animatorOverrideController["DEFAULT_RUN"] = weaponInUse.GetRunAnimClip();   
               animatorOverrideController["DEFAULT_SHEATH"] = weaponInUse.GetSeathAnimation();
        }
      

        private void ResetAnimController()
        {
            var animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;

            animatorOverrideController["DEFAULT_ATTACK"] = null;
            animatorOverrideController["DEFAULT_IDLE"] = null; 
            animatorOverrideController["DEFAULT_WALK"] = null;
            animatorOverrideController["DEFAULT_RUN"] = null;         
        }

        private void InstantiateInhand()
        {
            var weaponPrefab = weaponInUse.GetWeaponPreFab();
            GameObject Hand = RequestRHandTransform();
            var weapon = Instantiate(weaponPrefab, Hand.transform);
            _weapon = weapon;
            SetParentHand(Hand, weapon);

        }

        private void  InstantiateInSheath()
        {
            var weaponPrefab = weaponInUse.GetWeaponPreFab();
            GameObject Sheath = RequestSheathTransform();
            var weapon = Instantiate(weaponPrefab, Sheath.transform);
              _weapon = weapon;
            SetParentSheath(Sheath, weapon);
 
        }
        private void ResetWeapon()
        {
            ResetAnimController();
            Destroy(_weapon);         
            InstantiateInSheath();
        }

        private void SetParentHand(GameObject Hand, GameObject weapon)
        {

            weapon.transform.SetParent(Hand.transform);
            weapon.transform.localPosition  = weaponInUse.WeaponGrip.localPosition;
            weapon.transform.localRotation  = weaponInUse.WeaponGrip.localRotation;
            weapon.transform.localScale     = weaponInUse.WeaponGrip.localScale;

        }


        private void SetParentSheath(GameObject Sheath, GameObject weapon)
        {
            weapon.transform.SetParent(Sheath.transform);
            weapon.transform.localPosition  = weaponInUse.SheathTransform.localPosition;
            weapon.transform.localRotation  = weaponInUse.SheathTransform.localRotation;
            weapon.transform.localScale     = weaponInUse.SheathTransform.localScale;
            _weapon = weapon;

        }

        private GameObject RequestRHandTransform()
        {
            var RightHand = GetComponentsInChildren<FindRightHandOfPlayer>();
            int numberOfDominantHands = RightHand.Length;
            return RightHand[0].gameObject;
        }
        private GameObject RequestLHandTransform()
        {
            var LeftHand = GetComponentsInChildren<FindLeftHandOfPlayer>();
            int numberOfDominantHands = LeftHand.Length;
            return LeftHand[0].gameObject;
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
            cameraRaycaster.onOverPotentiallyEnemy += OnOverPotentiallyEnemy;

        }


        void OnOverPotentiallyEnemy(Enemy enemy)
        {

            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("UnSheath");
            }


            if (Input.GetMouseButtonDown(0) && IsTargetinRange(enemy.gameObject))
            {
                AttackTarget(enemy);
            }
          
                
        }

        private bool IsTargetinRange(GameObject target)
        {
            float   distanceTotarget = (target.transform.position - transform.position).magnitude;
            return  (distanceTotarget <= weaponInUse.GetMaxAttackRange());
        }
       
        [SerializeField]
        float maxHealthpoints = 100f;
        float currenthHealthPoints;


        public float healthAsPercentage { get { return currenthHealthPoints / (maxHealthpoints); } }
        public void TakeDamage(float damage)
        {
            currenthHealthPoints = Mathf.Clamp(currenthHealthPoints - damage, 0f, maxHealthpoints);
        }
        //void OnMouseClicked(RaycastHit raycastHit, int layerHit)
        //{
        //    if (layerHit == 9)
        //    {
        //        var enemy = raycastHit.collider.gameObject;
        //        if ((enemy.transform.position - transform.position).magnitude > maxattackRange)
        //        {return; }        
        //    }
        //}
      

        private void AttackTarget(Enemy enemy)
        {      
            if (Time.time - LastHitTime > weaponInUse.GetMinTimeBetweenHits())
            {
                animator.SetTrigger("Attack");
                enemy.TakeDamage(DMGpertHit);
                LastHitTime = Time.time;
            }
        }

        public void UnSheathMoment()
        {
            OverrideWeaponAnimatorController();
            SetParentHand(RequestRHandTransform(), _weapon);
            onWeaponChange = false;
        }

        public void SheathMoment()
        {         
            SetParentSheath(RequestSheathTransform(), _weapon);
            ResetAnimController();
            onWeaponChange = true;
        }
        public void OnValidate()
        {
            if (onWeaponChange == true)
            {
                ResetWeapon();
            }
          
           
        }




    }
}
    
