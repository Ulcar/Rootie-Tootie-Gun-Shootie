using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
  public abstract class Action:ScriptableObject
    {
  public  abstract void DoAction(EnemyAIController controller);
    public abstract void StartAction(EnemyAIController controller);
    public abstract void ExitAction(EnemyAIController controller);
}
