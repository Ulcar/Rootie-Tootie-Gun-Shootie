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
    public int HealthDropsForFloor;
    public int HealthPerRoomLimit;
    public int CoinsForFloor;
    public int CoinsPerRoomLimit;
    public int BossCoinReward;
    public int BossHearthReward;
    public int coinBaseline;
    public int healthBaseline;
}
