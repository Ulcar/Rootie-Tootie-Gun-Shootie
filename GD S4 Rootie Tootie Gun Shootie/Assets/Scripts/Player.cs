using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public Weapon weapon;
    public Movement movement;
    public bool test = true;
    public Transform DebugLocation;
    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    private SpriteRenderer weaponSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            Vector3 v_diff = (DebugLocation.position - transform.position);
            float Rad = Mathf.Atan2(v_diff.y, v_diff.x);
            float angle = Rad * Mathf.Rad2Deg;
            RotatePlayer(angle);
            RotateWeapon(angle);
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
}
