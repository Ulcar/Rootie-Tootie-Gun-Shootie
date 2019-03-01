using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
  public class Transition
    {
  public  List<Decision> Decisions;

    public bool DoDecisions(EnemyAIController controller, out State nextState)
    {
 
        foreach (Decision decision in Decisions)
        {
            if (decision.Decide(controller, out nextState))
            {
                return true;
            }
        }
        nextState = null;
        return false;
    }
    
    }

public abstract class Decision:ScriptableObject
{
    public abstract bool Decide(EnemyAIController controller, out State returnValue);
    [SerializeField]
   protected State stateToGoTo;
}

[CreateAssetMenu]
public class RangeDecision : Decision
{
    [SerializeField]
    float radius;
    [SerializeField]
    Vector2 direction;
    public override bool Decide(EnemyAIController controller, out State returnValue)
    {
        returnValue = null;
        RaycastHit2D[] hits;
        hits = Physics2D.CircleCastAll(controller.transform.position, radius, direction, 0, 1 << LayerMask.NameToLayer("Player"));
        foreach (RaycastHit2D hit in hits)
        {
            returnValue = stateToGoTo;
            controller.target = hit.transform;
            return true;
        }
        
        return false;
    }
}
