using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    WeaponStats stats;
    List<Attack> attacks;
    [SerializeField]
    GameObject bulletPrefab;

    Queue<BulletBehaviour> pool = new Queue<BulletBehaviour>();
    [SerializeField]
    int amountToPool;
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
            attacks = stats.attack;
        }
        Attack();
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        List<Bullet> bullets = attacks[0].DoAttack((int)transform.rotation.eulerAngles.z);
        StartCoroutine(ShootBullets(bullets));
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
}
