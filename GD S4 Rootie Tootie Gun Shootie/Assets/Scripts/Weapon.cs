using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    WeaponStats stats;
    List<Attack> attacks;

    List<GameObject> pool;
    void Start()
    {
        if (stats != null)
        {
            attacks = stats.attack;
        }
<<<<<<< HEAD
        List<GameObject> bullets = attacks[0].DoAttack();
        foreach (GameObject bullet in bullets)
        {
            Vector3 tetten = (bullet.transform.rotation.eulerAngles + transform.rotation.eulerAngles);
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = tetten;
            bullet.transform.rotation = rotation;
        }
=======
        Attack();
    
>>>>>>> master
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
        
        foreach (Bullet bullet in bullets)
        {
            float currentTime = 0;
            
            while (currentTime < bullet.spawnTime)
            {
                currentTime += Time.deltaTime;
                yield return null;

            }
            GameObject tmp = new GameObject();
            SpriteRenderer render = tmp.AddComponent<SpriteRenderer>();
            render.sprite = bullet.bulletSprite;
            tmp.transform.position = bullet.Position + transform.position;
            tmp.transform.rotation = bullet.Rotation;
            bulletMovement mov = tmp.AddComponent<bulletMovement>();
         //   Debug.Log(bullet.direction);
            mov.direction = bullet.direction;
            mov.movementSpeed = bullet.MovementSpeed;
            tmp.SetActive(true);
        }
        yield return null;
        }
}
