using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField]
    Weapon weapon;

    float currentTime = 0;
    float targetTime = 5f;
    
    //NOTES
    /*AI heeft Alert en Behaviour. Een enemy wordt Alert als een speler gespot is (door middel van een zichtcirkel), of als een enemy in range is die in alert mode is.
     *Behaviour wordt geactiveerd zodra de enemy in Alert is. Behaviour verschilt voor elke enemy. Denk hierbij aan Flying mobs die afwachten met aanvallen als de speler afgeleid is,
     *Of tanks die mobs beschermen. Ranged mobs houden afstand, en melee mobs gaan in de spelers face zitten.
     *
     * 
     * 
     * 
     */
     

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: make getter function to check if attack is off cooldown
        targetTime += Time.deltaTime;
        if (currentTime > targetTime)
        {
            currentTime = 0;
            weapon.Attack(1);
        }

        else
        {
            weapon.Attack(0);
        }
    }

}
