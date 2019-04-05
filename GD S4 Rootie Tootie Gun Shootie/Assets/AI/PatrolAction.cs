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
    bool waitFrame = false;

    [SerializeField]
    float switchTime = 2;
    float currentTime = 0;
    Vector3 initialPos;
    [SerializeField]
    bool flying = false;


    public override void StartAction(EnemyAIController controller)
    {
        initialPos = controller.transform.position;
        nodeIndex = 1;
        movementScript = controller.pathfindingAI;
        nodes[0] = Vector3.zero;
        movementScript.destination =  nodes[nodeIndex] + initialPos;
        movementScript.SearchPath();
        //wait a frame, because properties like reachedDestination only update the next frame
        waitFrame = true;



    }
    public override void DoAction(EnemyAIController controller)
    {
        if (movementScript.reachedEndOfPath && !movementScript.pathPending && !waitFrame)
        {

            currentTime += Time.deltaTime;
            if(currentTime > switchTime)
            {
                nodeIndex++;
                if (nodeIndex >= nodes.Count)
                {
                    nodeIndex = 0;
                }
                movementScript.destination = nodes[nodeIndex] + initialPos;
                movementScript.SearchPath();
                waitFrame = true;
                currentTime = 0;
            }
          

        }
        else
        {
            waitFrame = false;
        }

        
    }

    public override void ExitAction(EnemyAIController controller)
    {
    }

}
