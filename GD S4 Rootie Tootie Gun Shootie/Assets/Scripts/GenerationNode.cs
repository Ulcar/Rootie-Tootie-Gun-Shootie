using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ExitDirections
    {
        Up, Right, Down, Left
    }
public class GenerationNode
{
    public ExitDirections exitDirection;
    public GenerationNode ConnectedNode;

    public GenerationNode(ExitDirections Direction)
    {
        exitDirection = Direction;
    }
    public void ConnectNodes(GenerationNode NodeToConnectTo)
    {

            ConnectedNode = NodeToConnectTo;
            //NodeToConnectTo.ConnectedNode = this;

    }
}
