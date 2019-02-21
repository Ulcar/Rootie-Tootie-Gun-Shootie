using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    EnemyStats stats;
    [SerializeField]
    HealthManager manager;

    void Start()
    {
        manager = new HealthManager(stats.MaxHealth, 0, stats.MaxHealth, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletBehaviour bullet = collision.GetComponent<BulletBehaviour>();
        if(bullet != null)
        {
            TakeDamage(bullet.Bullet.Damage);
            bullet.gameObject.SetActive(false);
        }
        
    }


    //TODO: add animations here like flashing enemy, maybe knockback / damage type stuff here?
    //TODO: put colision on different script?
    void TakeDamage(int damage)
    {
        manager.TakeDamage(damage);
    }
}
