using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


  public  class BulletBehaviour:MonoBehaviour
    {
    [SerializeField]
    public Bullet Bullet { get; private set; }
    [SerializeField]
    private bulletMovement movement;

    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    LineRenderer line;
    //bullets that are lighter than itself are destroyed. bullets heavier than this bullet destroy the bullet
    public float Weight;
    public float currentTime = 0;
    public float currentLifeTime = 0;
    public float currentCooldownTime = 0;
    public IDamageable holder;
    public Weapon parent;
    private CircleCollider2D collider2D;

    private void Start()
    {
    }
    public void Init(Bullet bullet, Vector3 position)
    {
        this.Bullet = bullet;
        movement.direction = bullet.direction;
        movement.MovementSpeed = bullet.MovementSpeed;
        this.transform.position = bullet.Position + position;
        this.transform.rotation = bullet.Rotation;
        if (sprite != null)
        {
            sprite.sprite = bullet.bulletSprite;
        }
        else if (line != null)
        {
            line.material.mainTexture = bullet.bulletSprite.texture;
        }
        Weight = bullet.Weight;
        this.currentTime = 0;
        this.currentLifeTime = 0;
        collider2D = GetComponent<CircleCollider2D>();
        if (collider2D != null)
        {
            collider2D.radius = bullet.Radius;
        }

       
    }

    private void OnEnable()
    {
        if (Bullet != null)
        {
            if (Bullet.childBullets != null)
            {
                StartCoroutine(Spawn(Bullet.childBullets, transform.rotation.eulerAngles.z));
            }
        }
    }
    private void Update()
    {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime > Bullet.lifeTime)
        {
            gameObject.SetActive(false);
        }
        if (Bullet.childBullets != null)
        {
            currentCooldownTime += Time.deltaTime;
            if (currentCooldownTime > Bullet.childBullets.cooldown)
            {
                currentCooldownTime = 0;
                StartCoroutine(Spawn(Bullet.childBullets, transform.rotation.z));
            }
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletBehaviour tmp = collision.GetComponent<BulletBehaviour>();
        if (collision.gameObject.layer == 9)
        {
            gameObject.SetActive(false);
        }
        else if (tmp != null)
        {
            //TODO: Bullets should collide based on enemy who shot it, not weapon maybe?
            if (tmp.parent != parent)
            {

                if (Weight <= tmp.Weight)
                {
                    gameObject.SetActive(false);
                }
                if (Weight >= tmp.Weight)
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    IEnumerator Spawn(Attack attack, float rotation)
    {


        float currentFrameTime = 0;
        for (int i = 0; i < attack.bulletAmount; i++)
        {
            currentTime = 0;
            Bullet bullet = attack.SingleBullet((int)transform.rotation.eulerAngles.z);
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
            bulletBehaviour.parent = this.parent;
            //setting layer for now, so Enemies don't collide with each other at all
            bulletBehaviour.gameObject.layer = gameObject.layer;
            bulletBehaviour.gameObject.SetActive(true);
            //   }
            ObjectPool.instance.Enqueue(bulletBehaviour, bullet.type);
        }


        yield return null;
    }
}
