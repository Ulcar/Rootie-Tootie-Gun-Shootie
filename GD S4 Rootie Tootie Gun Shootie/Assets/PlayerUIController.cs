using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    Player player;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAbilityBar();
    }

    public void UpdateWeapons()
    {
        GameManager.instance.PrimaryWeapon.sprite = player.GetComponent<PlayerInventoryScript>().Primary.GetComponent<SpriteRenderer>().sprite;
        
        if (player.GetComponent<PlayerInventoryScript>().Secondary != null)
        {
            GameManager.instance.SecondaryWeapon.enabled = true;
            GameManager.instance.SecondaryWeapon.sprite = player.GetComponent<PlayerInventoryScript>().Secondary.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            GameManager.instance.SecondaryWeapon.enabled = false;
        }
    }

    void UpdateAbilityBar()
    {
        StandardMovement Standard = player.movement as StandardMovement;
        if (Standard != null)
        {
            GameManager.instance.AbilityBarFiller.fillAmount = (Standard.currentTime / Standard.dashCooldown);
        }
    }
}
