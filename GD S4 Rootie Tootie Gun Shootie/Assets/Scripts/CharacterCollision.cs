﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
    public class CharacterCollision:MonoBehaviour
    {
    IDamageable damageable;
    [SerializeField]
    bool invincible = false;

    public UnityEvent OnInvincible;

    int currentPriority = 99999;
    private void Start()
    {
        damageable = GetComponent<IDamageable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        BulletBehaviour bullet = collision.GetComponent<BulletBehaviour>();       
        if (bullet != null)
        {
            if (bullet.holder != damageable)
            {
                bullet.gameObject.SetActive(false);
                if (invincible)
                {
                    return;
                }
                damageable.TakeDamage(bullet.Bullet.Damage, bullet.transform.rotation * bullet.Bullet.direction, bullet.Bullet.MovementSpeed);               
            }
        }
    }

    public void SetInvincible(bool value, int priority)
    {
        if (priority <= currentPriority)
        {
            invincible = value;
            if (!value)
            {
                currentPriority = 99999;
            }
            OnInvincible.Invoke();
        }


    }
}
