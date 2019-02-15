using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
   public virtual List<Bullet> DoAttack(int rotationOffset)
    {
        List<Bullet> bullets = new List<Bullet>();

        return bullets;
    }

    public int bulletAmount;
    public float cooldown;
    public float currentTime;

    public abstract Bullet SingleBullet(int rotationOffset);

}
