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
    public Vector3 direction = Vector3.right;
    public Attack childBullets;
    public int Damage;
    public float Weight = 0;
   

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

    IEnumerator Spawn()
    {


        if (childBullets != null)
        {
            for (int i = 0; i < childBullets.bulletAmount; i++)
            {

            }
        }


        yield return null;
    }


    }
