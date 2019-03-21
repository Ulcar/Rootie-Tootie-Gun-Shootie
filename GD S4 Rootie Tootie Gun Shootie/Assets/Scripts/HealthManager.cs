using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class HealthManager
{
    [SerializeField]
   public int health { get; private set; }
    [SerializeField]
    int shield = 0;
    [SerializeField]
    int maxHealth;
    [SerializeField]
    int maxShield;

    public UnityEvent OnDeath = new UnityEvent();
    


    public HealthManager(int CurrentHealth, int CurrentShield, int MaximumHealth, int MaximumShield)
    {
        maxShield = MaximumShield;
        maxHealth = MaximumHealth;
        health = CurrentHealth;
        shield = CurrentShield;
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
            if (health <= 0)
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
        OnDeath.Invoke();
    }
}
