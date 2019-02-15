using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponStats : ScriptableObject
{
    public List<Attack> attack = new List<Attack>();
    public Sprite weaponSprite;

}
