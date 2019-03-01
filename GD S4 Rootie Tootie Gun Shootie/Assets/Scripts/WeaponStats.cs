using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponStats : ScriptableObject
{
    public List<Attack> attacks = new List<Attack>();
    public Sprite weaponSprite;
    //Make copy of attacks so multiple enemies can use the same attack


}
