using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu]
public class Shootaction : Action
{
    Weapon weapon;
    [SerializeField]
    float currentTime = 0;
    [SerializeField]
    float targetTime = 5f;

    [SerializeField]
    float BaseAttackCooldown = 1f;

    public override void DoAction(EnemyAIController controller)
    {
        //TODO: make getter function to check if attack is off cooldown
        currentTime += Time.deltaTime;
        if (currentTime > targetTime)
        {
            currentTime = 0;
            if (weapon != null)
            {
               weapon.Attack(1,  (Mathf.Atan2(controller.target.position.y - controller.transform.position.y, controller.target.position.x - controller.transform.position.x) * 180 / Mathf.PI));
                
            }
        }

        else
        {
            if (weapon != null)
            {
                weapon.Attack(0, (Mathf.Atan2(controller.target.position.y - controller.transform.position.y, controller.target.position.x - controller.transform.position.x) * 180 / Mathf.PI));
            }
        }
    }

    public override void ExitAction(EnemyAIController controller)
    {
    }

    public override void StartAction(EnemyAIController controller)
    {
        weapon = controller.weapon;
    }
}
