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
    public int maxHealth;
    [SerializeField]
    int maxShield;

    public UnityEvent OnDeath = new UnityEvent();


    void Update()
    {
        
    }
    public HealthManager(int CurrentHealth, int CurrentShield, int MaximumHealth, int MaximumShield)
    {
        Debug.Log("Ik wordt aangeroepen 2.0");
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
        Debug.Log("Ik wordt aangeroepen");
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

    public void UpdateHealthUI(int PlayerHealth)
    {
        Debug.Log("CurentHealth: " + PlayerHealth);
        float one = 10f;
        //Debug.Log("One: " + one);
        float temp = (PlayerHealth * one);
        //Debug.Log("FIll%: "+temp);
        //Debug.Log("Fill2%: " + temp / 100);
        GameManager.instance.HealthImage.fillAmount = temp / 100;
    }
}
