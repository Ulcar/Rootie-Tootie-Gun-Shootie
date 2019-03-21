using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTriggerScript : MonoBehaviour
{
    public Room parentRoom;

    void Start()
    {

    }

    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!parentRoom.StarterRoom && col.tag == "Player")
        {
            parentRoom.CloseAllGates();
            parentRoom.CameraRoomMode();
        }
    }


}
