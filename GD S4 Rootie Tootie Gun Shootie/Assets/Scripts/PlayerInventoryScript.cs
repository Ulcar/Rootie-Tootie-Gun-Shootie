using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventoryScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Primary;
    public GameObject Secondary;
    public Player player;
    void Start()
    {
        Primary = player.weapon;
        player.GetComponent<PlayerUIController>().UpdateWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchWeapon()
    {
        Debug.Log("Switched Weapons");
        if (Secondary != null)
        {
            GameObject temp = Primary;
            temp.GetComponent<Weapon>().SetHolder(null);
            temp.transform.position = Secondary.transform.position;
            Primary = Secondary;
            Primary.GetComponent<Weapon>().SetHolder(player);
            Primary.transform.localPosition = new Vector3(-0.06200001f, 0.163f, -1);
            Secondary = temp;
            player.weapon = Primary;
            player.weaponSprite = player.weapon.GetComponent<SpriteRenderer>();
            player.GetComponent<PlayerUIController>().UpdateWeapons();
        }
    }
}
