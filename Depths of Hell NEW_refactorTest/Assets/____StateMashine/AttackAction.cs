using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        RaycastHit hit;
        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.entityStats.attackRange.color.red);


        if (Physics.SphereCast(controller.eyes.position, controller.entityStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.entityStats.attackRange)
             && hit.collider.CompareTag("Player"))
        {

            if (controller.CheckIfCountDownElapsed(controller.entityStats.attackRate))
            {

                attack with Entity
                controller.entityAttacking.Fire(controller.entityStats.attackForce, controller.entityStats.attackRate);
            }
        }
    }
}

