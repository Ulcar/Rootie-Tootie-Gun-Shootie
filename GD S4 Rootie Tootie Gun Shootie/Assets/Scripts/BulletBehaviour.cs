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

    public void Init(Bullet bullet, Vector3 position)
    {
        this.Bullet = bullet;
        movement.direction = bullet.direction;
        movement.MovementSpeed = bullet.MovementSpeed;
        this.transform.position = bullet.Position + position;
        this.transform.rotation = bullet.Rotation;
        sprite.sprite = bullet.bulletSprite;
    }
    private void Update()
    {
        
    }
}
