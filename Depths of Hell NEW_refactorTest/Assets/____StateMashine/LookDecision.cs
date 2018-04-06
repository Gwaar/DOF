using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDecision : Decision

{
    public override bool Decide(StateController controller)
    {
        bool targetVisable = Look(controller);
        return targetVisable;
    }
    private bool Look(StateController controller)
    {
        RaycastHit hit;

        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.entityStats.lookRange, Color.blue);

        if (Physics.SphereCast(controller.eyes.position, controller.entityStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.lookRange)
            && hit.collider.CompareTag("Player"))
        {
            controller.chaseTarget = hit.transform;
            return true;
        }
        else
        {
            return false;
        }
    }
}
