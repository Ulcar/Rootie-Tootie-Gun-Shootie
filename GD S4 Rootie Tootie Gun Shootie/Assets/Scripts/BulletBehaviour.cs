using System;
using System.Collections.Generic;
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
    private SpriteRenderer sprite;
    //bullets that are lighter than itself are destroyed. bullets heavier than this bullet destroy the bullet
    public float Weight;

    public IDamageable holder;
    public Weapon parent;
    public void Init(Bullet bullet, Vector3 position)
    {
        this.Bullet = bullet;
        movement.direction = bullet.direction;
        movement.MovementSpeed = bullet.MovementSpeed;
        this.transform.position = bullet.Position + position;
        this.transform.rotation = bullet.Rotation;
        sprite.sprite = bullet.bulletSprite;
        Weight = bullet.Weight;
    }
    private void Update()
    {
        
    }

    private void OnDisable()
    {
        
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
}
