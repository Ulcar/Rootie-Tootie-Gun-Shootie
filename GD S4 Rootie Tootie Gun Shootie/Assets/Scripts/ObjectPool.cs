using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

   public class ObjectPool:MonoBehaviour      
    {
   private  Queue<BulletBehaviour> pool = new Queue<BulletBehaviour>();
    private Queue<BulletBehaviour> homingPool = new Queue<BulletBehaviour>();
    public static ObjectPool instance;
    [SerializeField]
    int amountToPool;
    [SerializeField]
  public GameObject bulletPrefab;

    public void Enqueue(BulletBehaviour bullet)
    {
        pool.Enqueue(bullet);
    }

    public void Enqueue(BulletBehaviour bullet, BulletType movement)
    {
        switch (movement)
        {
            case BulletType.Homing:
                homingPool.Enqueue(bullet);
                break;
            case BulletType.Straight:
                pool.Enqueue(bullet);
                break;
            default:
                pool.Enqueue(bullet);
                break;
        }
    }

    public BulletBehaviour Dequeue()
    {
        return pool.Dequeue();
    }

    public BulletBehaviour Dequeue(BulletType movement)
    {
        switch (movement)
        {
            case BulletType.Homing:
                return homingPool.Dequeue();
                break;
            case BulletType.Straight:
                return pool.Dequeue();
                break;
            default:
                return pool.Dequeue();
        }
    }

   public void Awake()
    {

            instance = this;

    }

    public void Start()
    {
        for (int i = 0; i < 1000; i++)
        {

            GameObject tmp = Instantiate(bulletPrefab);
            pool.Enqueue(tmp.GetComponent<BulletBehaviour>());
            tmp.SetActive(false);
        }
    }

}

public struct BulletTypeStruct
{
    BulletBehaviour behaviour;

}
