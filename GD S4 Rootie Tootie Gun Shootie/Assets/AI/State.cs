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
    

    private void DoActions()
    {
        for (int i = 0; i < actions.Length; i++)
        {
           // if(actions[i].GetType().GetGenericArguments()[0] == typeof())
        }
    }
}

