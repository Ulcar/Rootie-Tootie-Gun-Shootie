using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu]
public class DistanceDecision : Decision
{
    [SerializeField]
    float maxDistance;
    [SerializeField]
    float minDistance;
    [SerializeField]
    State minDistanceState;

    public override bool Decide(EnemyAIController controller, out State returnValue)
    {
        returnValue = stateToGoTo;
        if (Vector2.Distance(controller.transform.position, controller.target.transform.position) > maxDistance)
        {
            return true;
        }
        else if (Vector2.Distance(controller.transform.position, controller.target.transform.position) < minDistance)
        {
            if (minDistanceState != null)
            {
                returnValue = minDistanceState;
                return true;
            }
            else
            {
                return false;
            }
        }

        else
        {
            return false;
        }
    }
}
