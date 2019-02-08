using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Cone : Attack
{
    [SerializeField]
    private GameObject projectile1;
    
   

    public override List<GameObject> DoAttack()
    {
        List<GameObject> bullets =  base.DoAttack();
        bullets.Add(Instantiate(projectile1));
        bullets.Add(Instantiate(projectile1));
        bullets.Add(Instantiate(projectile1));
        SpawnPosition(0, bullets[0]);
        SpawnPosition(30, bullets[1]);
        SpawnPosition(60, bullets[2]);
        return bullets;
        
    }

    void Spawnbullet(float x, float y, int rotation, GameObject bullet)
    {
        var bulletposition = new Vector3(x, y, 0);
        bullet.transform.localPosition = bulletposition;
        bullet.transform.localRotation = Quaternion.Euler(0, 0, rotation);

    }

    void SpawnPosition(int rotation, GameObject bullet)
    {
        float xposition = Mathf.Cos((rotation * Mathf.PI) / 180) * 3;
        float yposition = Mathf.Sin((rotation * Mathf.PI) / 180) * 3;
        Spawnbullet(xposition, yposition, rotation, bullet);
    }

}
