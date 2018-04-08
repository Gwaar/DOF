using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "PluggableAI/Decisions/ActivateState")]

public class ActiveStateDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool chaseTargetisActive = controller.chaseTarget.gameObject.activeSelf;
        return chaseTargetisActive;
    }

}
