using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//Might make this SO later, if inheritance is needed?
[System.Serializable]
public class Bullet
    {
   public Sprite bulletSprite;
    public Vector3 Position;
    public Quaternion Rotation;
    public float MovementSpeed;
    public float spawnTime;
    public Vector3 direction = Vector3.right;
    public Attack childBullets;

    public Bullet( float MovementSpeed, Vector3 direction, Sprite sprite)
    {

        this.MovementSpeed = MovementSpeed;
        this.direction = direction;
        this.bulletSprite = sprite;

    }

    public void Enable()
    {
        if (childBullets != null)
        {
            childBullets.DoAttack((int)Rotation.z);
        }
    }
    }
