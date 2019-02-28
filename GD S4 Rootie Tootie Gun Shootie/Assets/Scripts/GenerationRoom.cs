using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationRoom
{
    public List<GenerationNode> Nodes;
    public int ID;
    public int x;
    public int y;
    public bool starterRoom = false;
    public bool bossRoom = false;
    public bool BossRoomEntryRoom = false;

    public GenerationRoom(int X, int Y, bool StarterRoom, bool BossRoom, int id)
    {
        ID = id;
        if (X > -1)
        {
            x = X;
        }
        else
        {
            Debug.Log("Room added with X lesser than 0");
        }
        if (Y > -1)
        {
            y = Y;
        }
        else
        {
            Debug.Log("Room added with Y lesser than 0");
        }
        starterRoom = StarterRoom;
        bossRoom = BossRoom;
        Nodes = new List<GenerationNode>();
    }
}
