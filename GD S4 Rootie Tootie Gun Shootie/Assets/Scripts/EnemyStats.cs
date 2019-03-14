using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    [SerializeField]
   public int MaxHealth;

    [SerializeField]
    List<Weapon> weapons;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitEnemy(List<Weapon> weapons)
    {
        this.weapons = weapons;
    }


}
