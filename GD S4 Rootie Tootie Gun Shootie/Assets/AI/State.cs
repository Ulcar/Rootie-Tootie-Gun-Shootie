using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Pathfinding;
[CreateAssetMenu]
   public class State:ScriptableObject
    {
    public Action[] actions;
    public Transition[] transitions;
    

    private void DoActions(EnemyAIController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].DoAction(controller);
        }
    }

    private void CheckTransitions(EnemyAIController controller)
    {
        State nextState = null;
        foreach (Transition transition in transitions)
        {
            
            if(transition.DoDecisions(controller, out nextState))
            {
                controller.TransitionState(nextState);
                return;
            }
        }
    }

    public void UpdateState(EnemyAIController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    

    public void StartActions(EnemyAIController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].StartAction(controller);
        }
    }
}

