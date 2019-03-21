using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    enum States
    {
        Player,
        Room
    };
    public bool InRoomMode;
    public Room RoomPlayerIsIn;
    public Player player;
    States currentState;
    Camera MainCamera;
    public float debugSize;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = gameObject.GetComponent<Camera>();
        InRoomMode = false;
        currentState = States.Player;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        switch (currentState)
        {
            case States.Player:
                PlayerState();
                break;
            case States.Room:
                roomState();
                break;
        }
    }

    public void RoomMode(Room room)
    {
        if (!InRoomMode)
        {
            InRoomMode = true;
            RoomPlayerIsIn = room;
            currentState = States.Room;
        }
    }

    public void PlayerMode()
    {
        InRoomMode = false;
        RoomPlayerIsIn = null;
        currentState = States.Player;
    }

    void PlayerState()
    {
        if (MainCamera.orthographicSize >= 3.01f)
        {
            
            MainCamera.orthographicSize = MainCamera.orthographicSize + (((14 / (MainCamera.orthographicSize/10)) - 35) / 75);
        }
        else
        {
            MainCamera.orthographicSize = 3;
        }
    }
    void roomState()
    {
        if (MainCamera.orthographicSize <= 5.99)
        {
            MainCamera.orthographicSize = MainCamera.orthographicSize + ((8.3f / MainCamera.orthographicSize) - 1.4f)/4;
        }
        else
        {
            MainCamera.orthographicSize = 6;
        }

        if (MainCamera.transform.position.x + MainCamera.orthographicSize >= RoomPlayerIsIn.transform.position.x + RoomPlayerIsIn.tilemap.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x * 20) 
        {
            MainCamera.transform.position = new Vector3(RoomPlayerIsIn.transform.position.x + RoomPlayerIsIn.tilemap.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x * 20, MainCamera.transform.position.y, MainCamera.transform.position.z);
        }
    }
}
