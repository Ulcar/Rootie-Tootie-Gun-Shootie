using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    WeaponStats stats;
    List<Attack> attacks = new List<Attack>();
    [SerializeField]
    GameObject bulletPrefab;

    Queue<BulletBehaviour> pool = new Queue<BulletBehaviour>();
    [SerializeField]
    int amountToPool;

    float timeSinceLastAttack = 100;
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject tmp = Instantiate(bulletPrefab);
            pool.Enqueue(tmp.GetComponent<BulletBehaviour>());
            tmp.SetActive(false);
        }

        if (stats != null)
        {
            //copy attacks so multiple weapon scripts can have the same weapon stats
            foreach (Attack attack in stats.attacks)
            {
                attacks.Add(Instantiate(attack));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

   public void Attack(int attackIndex)
    {
        StartCoroutine(ShootBullets(attackIndex));
       // List<Bullet> bullets = attacks[0].DoAttack((int)transform.rotation.eulerAngles.z);
      //  StartCoroutine(ShootBullets(bullets));
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
            BulletBehaviour bulletBehaviour = pool.Dequeue();
         //   if (!bulletBehaviour.gameObject.activeSelf)
         //   {
                bulletBehaviour.Init(bullet, transform.position);
                bulletBehaviour.gameObject.SetActive(true);
         //   }

            pool.Enqueue(bulletBehaviour);
        }
        yield return null;
        }

    IEnumerator ShootBullets(int attackIndex)
    {
        foreach (Attack attack in attacks)
        {
            attack.currentTime += timeSinceLastAttack;
        }
        timeSinceLastAttack = 0;
        if (attacks[attackIndex].cooldown < attacks[attackIndex].currentTime)
        {          
            attacks[attackIndex].currentTime = 0;
            for (int i = 0; i < attacks[attackIndex].bulletAmount; i++)
            {
                float currentTime = 0;
                Bullet bullet = attacks[attackIndex].SingleBullet((int)transform.rotation.eulerAngles.z);
                while (currentTime < bullet.spawnTime)
                {
                    currentTime += Time.deltaTime;
                    yield return null;

                }
                BulletBehaviour bulletBehaviour = pool.Dequeue();
                //   if (!bulletBehaviour.gameObject.activeSelf)
                //   {
                bulletBehaviour.Init(bullet, transform.position);
                bulletBehaviour.gameObject.SetActive(true);
                //   }

                pool.Enqueue(bulletBehaviour);
            }
            yield return null;
        }

        else
        {
            yield return null;
        }
    }
}
