using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public Weapon weapon;
    public Movement movement;
    public float movementSpeed;
    public HealthManager healthManager;
    public bool test = true;
    public Transform DebugLocation;

    [SerializeField]
    private SpriteRenderer weaponSprite;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = playerInfo.movementSpeed;
        healthManager = new HealthManager(playerInfo.maxHealth, playerInfo.maxShield, playerInfo.maxHealth, playerInfo.maxShield);
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            Vector3 v_diff = (DebugLocation.position - transform.position);
            float Rad = Mathf.Atan2(v_diff.y, v_diff.x);
            float angle = Rad * Mathf.Rad2Deg;
            RotatePlayer(angle, weapon);
            RotateWeapon(weapon, angle);
        }
        movement.Move(DebugLocation.position.x, DebugLocation.position.y, Vector3.Distance(transform.position, DebugLocation.transform.position) * movementSpeed);
    }

    void RotatePlayer(float angle, Weapon EquipedWeapon)
    {
        Debug.Log("Angle: " + angle);
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
            transform.rotation = Quaternion.Euler(0f, 180f, transform.rotation.y);
            EquipedWeapon.transform.position = new Vector3(weapon.transform.position.x, weapon.transform.position.y, -0.01f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.y);

            EquipedWeapon.transform.position = new Vector3(weapon.transform.position.x, weapon.transform.position.y, -0.01f);

        }

    }

    void RotateWeapon(Weapon EquipedWeapon, float Angle)
    {
        Debug.Log("Angle voor RotateWeapon: " + Angle);
        if (Angle >= 90 || Angle <= -90)
        {
            EquipedWeapon.transform.localRotation = Quaternion.Euler(-180f, -180f, (Angle * -1));
        }
        else
        {
            EquipedWeapon.transform.localRotation = Quaternion.Euler(0f, 0f, (Angle));
        }
    }

}
