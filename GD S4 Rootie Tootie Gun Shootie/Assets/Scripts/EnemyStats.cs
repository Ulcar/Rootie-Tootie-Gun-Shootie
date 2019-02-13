using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    [SerializeField]
   public int MaxHealth;
    [SerializeField]
   public List<Attack> Attacks = new List<Attack>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Attack(int rotationOffset)
    {
        Attacks[0].DoAttack(rotationOffset);
    }
}
