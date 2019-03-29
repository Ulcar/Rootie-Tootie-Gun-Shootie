using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTriggerScript : MonoBehaviour
{
    public Room parentRoom;
    public bool permOpen = false;

    void Start()
    {

    }

    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!parentRoom.StarterRoom && col.tag == "Player" && !permOpen)
        {
            parentRoom.CloseAllGates();
            parentRoom.EnableEnemies();
        }
    }


}
