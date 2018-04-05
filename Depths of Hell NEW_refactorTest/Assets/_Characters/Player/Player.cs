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
      
        GameObject _weapon;
 
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
            OverrideWeaponAnimatorController();
           // InstantiateInhand();
           // InstantiateInSheath();
         //   ResetAnimController();

           // InstantiateInhand();
          

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
        }

        private void ResetAnimController()
        {

            var animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;

            animatorOverrideController["DEFAULT_ATTACK"] = null;
            animatorOverrideController["DEFAULT_IDLE"] = null; // remove const
            animatorOverrideController["DEFAULT_WALK"] = null;
            animatorOverrideController["DEFAULT_RUN"] = null;
           // animatorOverrideController["DEFAULT_SHEATH"] = null;
        }





        //private void InstantiateInhand()
        //{
        //    var weaponPrefab = weaponToInstasiate.GetWeaponPreFab();
        //    GameObject Hand = RequestHandTransform();
        //    var weapon = Instantiate(weaponPrefab, Hand.transform);
        //    _weapon = weapon;
        //    SetParentHand(Hand, weapon);


            
       // }

        private void  InstantiateInSheath()
        {
            var weaponPrefab = weaponToInstasiate.GetWeaponPreFab();
            GameObject Sheath = RequestSheathTransform();
            var weapon = Instantiate(weaponPrefab, Sheath.transform);
              _weapon = weapon;
            SetParentSeath(Sheath, weapon);


            
        }
       

        private void SetParentHand(GameObject Hand, GameObject weapon)
        {

           
            weapon.transform.localPosition  = weaponToInstasiate.WeaponGrip.localPosition;
            weapon.transform.localRotation  = weaponToInstasiate.WeaponGrip.localRotation;
            weapon.transform.localScale     = weaponToInstasiate.WeaponGrip.localScale;






          //  
            weapon.transform.SetParent(Hand.transform);


          
        }

        private void SetParentSeath(GameObject Sheath, GameObject weapon)
        {

          
            weapon.transform.localPosition = weaponToInstasiate.SheathTransform.localPosition;
            weapon.transform.localRotation = weaponToInstasiate.SheathTransform.localRotation;
            weapon.transform.localScale =    weaponToInstasiate.SheathTransform.localScale;


            _weapon = weapon;
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
         //  
            SetParentHand(RequestHandTransform(), _weapon);
       
        }

        public void SheathMoment()
        {
           
            SetParentSeath(RequestSheathTransform(), _weapon);
        }   

  
    }
}
    
