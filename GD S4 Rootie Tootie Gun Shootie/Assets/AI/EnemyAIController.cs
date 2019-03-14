using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Pathfinding;
  public  class EnemyAIController:MonoBehaviour
    {
   public IAstarAI pathfindingAI { get; private set; }
    [SerializeField]
    State currentState;

    public Transform target;

    public Weapon weapon;

    public Enemy enemy;

    private void Start()
    {
        pathfindingAI = GetComponent<IAstarAI>();
        weapon = GetComponentInChildren<Weapon>();
        enemy = GetComponentInChildren<Enemy>();
        //still pretty ugly and implementation specific
        weapon.SetHolder(GetComponentInChildren<IDamageable>());
        currentState.StartActions(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void TransitionState(State newState)
    {
        currentState = newState;
        currentState.StartActions(this);
    }
}
