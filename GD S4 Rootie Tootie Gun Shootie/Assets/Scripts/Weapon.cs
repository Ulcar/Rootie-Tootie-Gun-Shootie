﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IPickupable
{
    // Start is called before the first frame update
    [SerializeField]
   public WeaponStats stats;
    List<Attack> attacks = new List<Attack>();
    public string WeaponName;
    public Sprite WeaponWithoutHands;
    public Sprite WeaponWithHands;
    IDamageable holder;
    float timeSinceLastAttack = 100;
    void Start()
    {
        if (stats != null)
        {
            //copy attacks so multiple weapon scripts can have the same weapon stats
            foreach (Attack attack in stats.attacks)
            {
                attacks.Add(Instantiate(attack));
            }
            WeaponName = stats.WeaponName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

   public void Attack(int attackIndex, float rotation)
    {
        StartCoroutine(ShootBullets(attackIndex));
       // List<Bullet> bullets = attacks[0].DoAttack((int)transform.rotation.eulerAngles.z);
      //  StartCoroutine(ShootBullets(bullets));
    }

    public void Attack(int attackIndex)
    {
        StartCoroutine(ShootBullets(attackIndex));
        // List<Bullet> bullets = attacks[0].DoAttack((int)transform.rotation.eulerAngles.z);
        //  StartCoroutine(ShootBullets(bullets));
    }

    public bool GetCooldown(int attackIndex)
    {
        return attacks[attackIndex].cooldown < attacks[attackIndex].currentTime;
    }

    IEnumerator ShootBullets(List<Bullet> bullets)
    {
        //TODO: move timing code from weapon to bullet itself?
        foreach (Bullet bullet in bullets)
        {
            float currentTime = 0;
            
            while (currentTime < bullet.spawnTime)
            {
                currentTime += Time.deltaTime;
                yield return null;

            }
            BulletBehaviour bulletBehaviour = ObjectPool.instance.Dequeue();
                bulletBehaviour.Init(bullet, transform.position);  
                bulletBehaviour.gameObject.SetActive(true);
         //   }

            ObjectPool.instance.Enqueue(bulletBehaviour);
        }
        yield return null;
        }

    IEnumerator ShootBullets(int attackIndex)
    {
        if (attacks[attackIndex] != null)
        {
            foreach (Attack attack in attacks)
            {
                attack.currentTime += timeSinceLastAttack;
            }
            timeSinceLastAttack = 0;

            if (attacks[attackIndex].cooldown < attacks[attackIndex].currentTime)
            {
                attacks[attackIndex].currentTime = 0;
                float currentFrameTime = 0;
                for (int i = 0; i < attacks[attackIndex].bulletAmount; i++)
                {
                    float currentTime = 0;              
                    Bullet bullet = attacks[attackIndex].SingleBullet((int)transform.rotation.eulerAngles.z);
                    while (currentTime < bullet.spawnTime)
                    {
                        currentTime += Time.deltaTime;
                        currentFrameTime += bullet.spawnTime;
                        if (currentTime < bullet.spawnTime)
                        {
                            if (currentFrameTime >= Time.deltaTime)
                            {
                                currentFrameTime = 0;
                                yield return null;
                            }
                            //time is less than a frame, go to next bullet
                            else
                            {
                                currentTime = 0;
                                break;
                            }
                        }
                        if (currentFrameTime >= Time.deltaTime)
                        {
                            currentFrameTime = 0;
                            yield return null;
                        }
                    }
                    
                    BulletBehaviour bulletBehaviour = null;
                    bulletBehaviour = ObjectPool.instance.Dequeue(bullet.type);
                   
                 
                    bulletBehaviour.Init(bullet, transform.position);
                    bulletBehaviour.holder = holder;
                    bulletBehaviour.parent = this;
                    //setting layer for now, so Enemies don't collide with each other at all
                    bulletBehaviour.gameObject.layer = gameObject.layer;
                    bulletBehaviour.gameObject.SetActive(true);
                    //   }
                    ObjectPool.instance.Enqueue(bulletBehaviour, bullet.type);
                }
                yield return null;
            }

            else
            {
                yield return null;
            }
        }

        else
        {
            Debug.LogWarning("Attack Does not exist");
        }
    }

    public void SetHolder(IDamageable holder)
    {
        this.holder = holder;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void PickUp()
    {

    }
}
