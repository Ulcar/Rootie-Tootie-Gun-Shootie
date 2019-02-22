using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;
public class Enemy : MonoBehaviour
{
    [SerializeField]
    EnemyStats stats;
    [SerializeField]
    HealthManager manager;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    Vector2 impact = Vector2.zero;
    [SerializeField]
    AILerp AI;

    [SerializeField]
    float Weight;
   

    void Start()
    {
        manager = new HealthManager(stats.MaxHealth, 0, stats.MaxHealth, 0);
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void FixedUpdate()
    {
        if ((impact.x > 0.00001 || impact.y > 0.00001))
        {
            rb.velocity = impact;
            impact = Vector3.Lerp(impact, Vector3.zero, 20 * Time.deltaTime);
        }

        else if (!AI.canMove)
        {
            rb.velocity = Vector3.zero;
            AI.SearchPath();
            //AI.ResetMovement();
            AI.canMove = true;
            impact = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletBehaviour bullet = collision.GetComponent<BulletBehaviour>();
        if(bullet != null)
        {
            if (bullet.holder != this)
            {
                TakeDamage(bullet.Bullet.Damage, bullet.transform.rotation * bullet.Bullet.direction, bullet.Bullet.MovementSpeed);
                bullet.gameObject.SetActive(false);
            }
        }
        
    }

 


    //TODO: add animations here like flashing enemy, maybe knockback / damage type stuff here?
    //TODO: put colision on different script?
    void TakeDamage(int damage, Vector2 direction, float speed)
    {
        manager.TakeDamage(damage);
        impact += direction * speed * Weight;
        if (AI != null)
        {
            AI.canMove = false;
        }
    }
}
