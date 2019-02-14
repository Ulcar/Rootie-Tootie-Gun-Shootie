using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    EnemyStats stats;

    [SerializeField]
    int currentHealth;
    void Start()
    {
        currentHealth = stats.MaxHealth;
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
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
