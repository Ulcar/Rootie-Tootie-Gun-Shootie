using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTriggerScript : MonoBehaviour
{
    public Room parentRoom;

    void OnTriggerEnter2D(Collider2D col)
    {
        parentRoom.CloseAllGates();   
    }


}
