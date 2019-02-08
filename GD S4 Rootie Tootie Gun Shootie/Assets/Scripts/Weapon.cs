using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    WeaponStats stats;
    List<Attack> attacks;
    void Start()
    {
        if (stats != null)
        {
            attacks = stats.attack;
        }
    List<GameObject> bullets =    attacks[0].DoAttack();
        foreach (GameObject bullet in bullets)
        {
            Vector3 tetten = (bullet.transform.rotation.eulerAngles + transform.rotation.eulerAngles);
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = tetten;
            bullet.transform.rotation = rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<GameObject> bullets = attacks[0].DoAttack();
            foreach (GameObject bullet in bullets)
            {
                bullet.transform.transform.position = gameObject.transform.position + bullet.transform.TransformDirection(bullet.transform.position);
            }
        }
    }
}
