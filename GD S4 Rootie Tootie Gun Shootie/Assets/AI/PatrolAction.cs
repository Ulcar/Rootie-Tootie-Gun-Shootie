using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Pathfinding;

   public class PatrolAction:Action
    {
    AILerp movementScript;

    [SerializeField]
    List<Vector3> nodes;
    int nodeIndex = 0;


    public void StartAction()
    {
        nodeIndex = 0;
        movementScript.destination = nodes[nodeIndex];
    }
    public override void DoAction()
    {
        if (movementScript.remainingDistance < 1)
        {
            nodeIndex++;
            if (nodeIndex >= nodes.Count)
            {
                nodeIndex = 0;
            }
        }
    }

    }
