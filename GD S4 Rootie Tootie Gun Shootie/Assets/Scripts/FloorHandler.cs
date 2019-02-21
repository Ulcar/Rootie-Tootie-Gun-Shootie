using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHandler : MonoBehaviour
{
    public Room[] Rooms;
    // Start is called before the first frame update
    void Start()
    {

            Instantiate(Rooms[0]);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
