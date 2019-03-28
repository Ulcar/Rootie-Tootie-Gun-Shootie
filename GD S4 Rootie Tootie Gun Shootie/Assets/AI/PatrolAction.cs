using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Pathfinding;
[CreateAssetMenu]
   public class PatrolAction:Action
    {
    IAstarAI movementScript;

    [SerializeField]
    List<Vector3> nodes;
    int nodeIndex = 0;


    public override void StartAction(EnemyAIController controller)
    {
        nodeIndex = 0;
        movementScript = controller.pathfindingAI;
        movementScript.destination =  nodes[nodeIndex] + controller.transform.position;



    }
    public override void DoAction(EnemyAIController controller)
    {
        if (movementScript.reachedDestination)
        {
            nodeIndex++;
            
            if (nodeIndex >= nodes.Count)
            {   
                nodeIndex = 0;
            }
            movementScript.destination = nodes[nodeIndex] + controller.transform.position;
            movementScript.SearchPath();
            
        }
    }

    public override void ExitAction(EnemyAIController controller)
    {
    }

}
