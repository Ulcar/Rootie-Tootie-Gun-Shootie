using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;
using UnityEngine.Events;
public class Enemy : MonoBehaviour, IDamageable
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

    [SerializeField]
   public Transform target;

    public EnemyDeathEvent OnDeath = new EnemyDeathEvent();
   

    void Start()
    {
        manager = new HealthManager(stats.MaxHealth, 0, stats.MaxHealth, 0);
        manager.OnDeath.AddListener(OnDeathEvent);
    }

    public void Init(EnemyStats stats)
    {
        this.stats = stats;
        manager = new HealthManager(stats.MaxHealth, 0, stats.MaxHealth, 0);
        GetComponent<Weapon>().stats = stats.weapons[0];
        manager.OnDeath.AddListener(OnDeathEvent);
        //Code to generate enemy from Enemystats here
    }

    void OnDeathEvent()
    {
        OnDeath.Invoke(this);
        Destroy(gameObject.transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void FixedUpdate()
    {
        if ((impact.x > 0.001 || impact.y > 0.001))
        {
            rb.velocity = impact;
            impact = Vector3.Lerp(impact, Vector3.zero, 25 * Time.deltaTime);
        }

        else if (!AI.canMove)
        {
            rb.velocity = Vector3.zero;
            AI.SearchPath();
            AI.canMove = true;
            impact = Vector3.zero;
        }
    }


   public  void TakeDamage(int damage, Vector2 direction, float speed)
    {
        manager.TakeDamage(damage);
        impact += direction * speed * Weight;
        if (AI != null)
        {
            AI.canMove = false;
        }
    }


}

public class EnemyDeathEvent : UnityEvent<Enemy>
{

}
