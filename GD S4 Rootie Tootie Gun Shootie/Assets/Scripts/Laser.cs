using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Laser : Attack
{
    [SerializeField]
    private GameObject LaserProjectile;
    public override List<GameObject> DoAttack()
    {
        List<GameObject> bullets = base.DoAttack();
        bullets.Add(Instantiate(LaserProjectile));
        SpawnPosition(0, bullets[0]);
        return bullets;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
