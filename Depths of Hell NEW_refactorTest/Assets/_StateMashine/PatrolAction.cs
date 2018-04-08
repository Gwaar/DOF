using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;


[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{


    public override void Act(StateController controller)
    {
        Patrol(controller);


    }

    public void Patrol(StateController controller)
    {


        controller.StateWorking("bajs");
      
        //controller.navMeshAgent.destination = controller.wayPointList[controller.nextWayPoint].position;
        //controller.navMeshAgent.isStopped = false;

        //if (controller.navMeshAgent.remainingDistance <=
        //    controller.navMeshAgent.stoppingDistance &&
        //    !controller.navMeshAgent.pathPending)
        //{

            //Vector3 _nextWaypoint = controller.wayPointList[controller.nextWayPoint].position;

            //controller.aiCharacterControl.SetTarget(controller.wayPointList[controller.nextWayPoint].transform);


            

            //controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count; // what does % mean?


    //    }

    }
}
