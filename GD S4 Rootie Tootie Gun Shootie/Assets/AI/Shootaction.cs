﻿using System;
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
            weapon.Attack(1, Vector2.Angle(controller.transform.position, controller.target.position) - 180);
        }

        else
        {

            weapon.Attack(0, Vector2.Angle(controller.transform.position, controller.target.position) - 180);
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
