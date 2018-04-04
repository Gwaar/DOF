using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Weapon;

public class W_AnimationOverrideControllers : MonoBehaviour {


    public AnimationClip[] weaponAnimationClip;

    protected Animator animator;
    AnimatorOverrideController animatorOverrideController;

    protected int weaponIndex;

    public void Start()
    {
        animator = GetComponent<Animator>();
        weaponIndex = 0;

        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
    }

    public void Update()
    {
        if (Input.GetButtonDown("NextWeapon"))
        {
            weaponIndex = (weaponIndex + 1) % weaponAnimationClip.Length;
            animatorOverrideController["shot"] = weaponAnimationClip[weaponIndex];
        }
    }
}
