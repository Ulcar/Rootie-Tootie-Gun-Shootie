using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HealthManager
{
    [SerializeField]
    int health = 0;
    [SerializeField]
    int shield = 0;
    [SerializeField]
    int maxHealth;
    [SerializeField]
    int maxShield;


    public HealthManager(int CurrentHealth, int CurrentShield, int MaximumHealth, int MaximumShield)
    {
        maxShield = MaximumShield;
        maxHealth = MaximumHealth;
        health = CurrentHealth;
        shield = CurrentShield;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Dead af niBBa");
        }
    }

    public void TakeDamage(int amount)
    {

        if (shield > 0)
        {
            if (shield < amount)
            {
                health -= (amount - shield);
                shield = 0;
            }
            else
            {
                shield -= amount;
            }
        }
        else
        {
            health -= amount;
            if (health < 0)
            {
                health = 0;
                Death();
            }
        }
    }

    public bool GainHealth(int amount)
    {
        //True geeft aan dat de health pickup verwijderd moet worden, false geeft aan dat de health pickup niet gebruikt moet worden
        if (health < maxHealth)
        {
            if (health + amount > maxHealth)
            {
                health = maxHealth;
                return true;
            }
            else
            {
                health += amount;
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public bool GainShield(int amount)
    {
        if (shield < maxShield)
        {
            if (shield + amount > maxShield)
            {
                shield = maxShield;
                return true;
            }
            else
            {
                shield += maxShield;
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    private void Death()
    {
        Debug.Log("Dead AF niBBA");
    }
}
