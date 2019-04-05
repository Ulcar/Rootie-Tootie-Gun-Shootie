using System;
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
    [SerializeField]
    CircleCollider2D col;
    Collider2D[] hits = new Collider2D[10];

    int currentPriority = 99999;
    private void Start()
    {
        damageable = GetComponent<IDamageable>();
        col = GetComponentInParent<CircleCollider2D>();
    }

    private void Update()
    {
      int count =   Physics2D.OverlapCircleNonAlloc(transform.position, col.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y), hits);
        for (int i = 0; i < count; i++)
        {
            OnHit(hits[i]);
        }
    }

    private void OnHit(Collider2D collision)
    {
      
        BulletBehaviour bullet = collision.GetComponent<BulletBehaviour>();       
        if (bullet != null)
        {
            if (bullet.holder != damageable && damageable.GetType() != bullet.holder.GetType())
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
