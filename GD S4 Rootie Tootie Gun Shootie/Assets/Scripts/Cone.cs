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

}
