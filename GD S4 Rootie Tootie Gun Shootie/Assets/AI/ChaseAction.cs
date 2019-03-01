using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Pathfinding;
[CreateAssetMenu]
   public class ChaseAction:Action
    {
    IAstarAI movementScript;

    int nodeIndex = 0;
    public Transform target;

    public override void StartAction(EnemyAIController controller)
    {
        target = controller.target;
        
    }
    public override void DoAction(EnemyAIController controller)
    {
        controller.pathfindingAI.destination = target.position;
    }

    public override void ExitAction(EnemyAIController controller)
    {
    }

}
