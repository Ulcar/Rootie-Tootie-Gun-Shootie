using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloorInfo : ScriptableObject
{
    public int RoomsInFloor1;
    public Texture2D Floor1SpriteSheet;
    public int FloorWidthHeight;
    public Room StarterRoom;
    public GameObject[] Enemies;
}
