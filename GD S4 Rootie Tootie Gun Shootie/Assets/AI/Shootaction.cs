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
            Vector2 C = controller.transform.position - controller.target.position;
            float Angle = Mathf.Atan2(C.y, C.x);
            float AngleInDegrees = Angle * Mathf.Rad2Deg;
            weapon.Attack(1, AngleInDegrees);
        }

        else
        {

            Vector2 C = controller.transform.position - controller.target.position;
            float Angle = Mathf.Atan2(C.y, C.x);
            float AngleInDegrees = Angle * Mathf.Rad2Deg;
            weapon.Attack(0, AngleInDegrees);
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
