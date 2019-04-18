using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
[System.Serializable]
public class Bullet
    {
   public Sprite bulletSprite;
    public Vector3 Position;
    public Quaternion Rotation;
    public float MovementSpeed;
    public float spawnTime;
    public float lifeTime = 9999999;
    public Vector3 direction = Vector3.right;
    public Attack childBullets;
    public int Damage;
    public int bulletIndex = 1;
    public float Weight = 0;
    public float Radius = 0.1f;
    public BulletType type = BulletType.Straight;
   

    public Bullet( float MovementSpeed, Vector3 direction, Sprite sprite, float lifeTime)
    {

        this.MovementSpeed = MovementSpeed;
        this.direction = direction;
        this.bulletSprite = sprite;
        if (lifeTime > 0)
        {
            this.lifeTime = lifeTime;
        }

    }

    public Bullet(float MovementSpeed, Vector3 direction, Sprite sprite, float lifeTime, Attack childBullets)
    {

        this.MovementSpeed = MovementSpeed;
        this.direction = direction;
        this.bulletSprite = sprite;
        if (lifeTime > 0)
        {
            this.lifeTime = lifeTime;
        }
        this.childBullets = childBullets;

    }

    public void Enable()
    {
        if (childBullets != null)
        {
            childBullets.DoAttack((int)Rotation.z);
            
        }
    }




    }

public enum BulletType
{
    Straight, Homing
}
