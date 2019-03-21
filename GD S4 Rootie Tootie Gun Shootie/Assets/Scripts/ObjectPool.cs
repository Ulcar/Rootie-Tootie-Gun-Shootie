using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

   public class ObjectPool:MonoBehaviour      
    {
   private  Queue<BulletBehaviour> pool = new Queue<BulletBehaviour>();
    public static ObjectPool instance;
    [SerializeField]
    int amountToPool;
    [SerializeField]
  public GameObject bulletPrefab;

    public void Enqueue(BulletBehaviour bullet)
    {
        pool.Enqueue(bullet);
    }

    public BulletBehaviour Dequeue()
    {
        return pool.Dequeue();
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
