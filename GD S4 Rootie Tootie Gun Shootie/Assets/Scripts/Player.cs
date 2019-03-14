using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public PlayerInfo playerInfo;
    public Weapon weapon;
    public Movement movement;
    public float movementSpeed;
    public HealthManager healthManager;
    public bool test = true;
    public Transform DebugLocation;
    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    float invulTimeOnHit = 5;

    float currentTime;

    [SerializeField]
    private SpriteRenderer weaponSprite;

    [SerializeField]
    CharacterCollision collision;

    // Start is called before the first frame update
    void Start()
    {

        movementSpeed = playerInfo.movementSpeed;
        weapon.SetHolder(this);
        healthManager = new HealthManager(playerInfo.maxHealth, playerInfo.maxShield, playerInfo.maxHealth, playerInfo.maxShield);

    }

    // Update is called once per frame
    void Update()
    {

        if (currentTime < invulTimeOnHit)
        {
            currentTime += Time.deltaTime;
            if (currentTime > invulTimeOnHit)
            {
                collision.SetInvincible(false, 0);
            }
        }

    }

  public  void RotatePlayer(float angle)
    {
     //   Debug.Log("Angle: " + angle);
        if (angle > 90)
        {
            angle -= 360f;
        }
        if (angle < -90)
        {
            angle += 360f;
        }
        if (angle >= 90 || angle <= -90)
        {
            // transform.rotation = Quaternion.Euler(0f, 180f, transform.rotation.y);
            renderer.flipX = true;
            weapon.transform.position = new Vector3(weapon.transform.position.x, weapon.transform.position.y, -0.01f);
        }
        else
        {
            // transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.y);
            renderer.flipX = false;
            
            weapon.transform.position = new Vector3(weapon.transform.position.x, weapon.transform.position.y, -0.01f);
            
        }
    }

  public  void RotateWeapon(float Angle)
    {
      //  Debug.Log("Angle voor RotateWeapon: " + Angle);
        if (Angle >= 90 || Angle <= -90)
        {
                weapon.transform.localRotation = Quaternion.Euler(0, 0, (Angle));
            weaponSprite.flipY = true;
          //  EquipedWeapon.transform.localRotation = Quaternion.Euler(0f, 0f, (Angle));
        }
        else
        {
            weaponSprite.flipY = false;
            weapon.transform.localRotation = Quaternion.Euler(0, 0, (Angle));
        }
    }


    public void TakeDamage(int damage, Vector2 direction, float speed)
    {
        
        healthManager.TakeDamage(1);
        if (collision != null)
        {
            collision.SetInvincible(true, 0);
            currentTime = 0;
        }

    }
}
