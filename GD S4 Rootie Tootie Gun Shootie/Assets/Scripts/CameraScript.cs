using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public enum States
    {
        Player,
        Room
    };
    public bool InRoomMode;
    public Room RoomPlayerIsIn;
    public Player player;
    public States currentState;
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
        float diff = MainCamera.transform.position.x - player.transform.position.x;
        Debug.Log("Diff for X: " + diff); 
        if (diff >= 0.1)
        {
            float result = (diff / 1) / 15;
            //Debug.Log("XDiff: " + diff + ", Result: " + result);
            float x = MainCamera.transform.position.x - result;
            MainCamera.transform.position = new Vector3(x, MainCamera.transform.position.y, MainCamera.transform.position.z);
        }
        else if (diff <= -0.1)
        {
            float result = (diff / 1) / 15;
            Debug.Log("XDiff: " + diff + ", Result: " + result);
            float x = MainCamera.transform.position.x - result;
            MainCamera.transform.position = new Vector3(x, MainCamera.transform.position.y, MainCamera.transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, MainCamera.transform.position.y, -10);
        }

        float Yone = player.transform.position.y;
        float Ytwo = MainCamera.transform.position.y;
        float diff2 = (Yone - Ytwo) ;
        Debug.Log("PlayerY: " + player.transform.position.y + "MainCameraY: " + MainCamera.transform.position.y);
        Debug.Log("Diff2 for Y: " + diff2);
        if (diff2 >= 0.1)
        {
            float result = (diff2 / 1) / 15;
            Debug.Log("YDiff2: " + diff2 + ", Result: " + result);
            float y = MainCamera.transform.position.y + result;
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, y, MainCamera.transform.position.z);
        }
        
        else if (diff2 <= -0.1)
        {
            float result = (diff2 / 1) / 15;
            //Debug.Log("XDiff: " + diff + ", Result: " + result);
            float y = MainCamera.transform.position.y + result;
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, y, MainCamera.transform.position.z);
        }
        else
        {
            transform.position = new Vector3(MainCamera.transform.position.x, player.transform.position.y, -10);
        }
        


        if (MainCamera.orthographicSize >= 3.01f)
        {
            
            MainCamera.orthographicSize = MainCamera.orthographicSize + (((14 / (MainCamera.orthographicSize/10)) - 35) / 100);
        }
        else
        {
            MainCamera.orthographicSize = 3;
        }

    }
    void roomState()
    {
        // CAMERA "DISTANCE" FROM PLAYER
        if (MainCamera.orthographicSize <= 5.99)
        {
            MainCamera.orthographicSize = MainCamera.orthographicSize + ((8.3f / MainCamera.orthographicSize) - 1.4f)/4;
        }
        else
        {
            MainCamera.orthographicSize = 6;
        }

        // ROOM SNAP X
        if (MainCamera.transform.position.x + 10 > RoomPlayerIsIn.transform.position.x + 10) 
        {
            float diff = (MainCamera.transform.position.x + 10) - (RoomPlayerIsIn.transform.position.x + 10);
            if (diff >= 0.1)
            {
                float result = (diff / 1) / 20;
                //Debug.Log("XDiff: " + diff + ", Result: " + result);
                float x = MainCamera.transform.position.x - result;
                MainCamera.transform.position = new Vector3(x, MainCamera.transform.position.y, MainCamera.transform.position.z);
            }
            else
            {
                MainCamera.transform.position = new Vector3(RoomPlayerIsIn.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
            }
        }
        if (MainCamera.transform.position.x - 10 < RoomPlayerIsIn.transform.position.x - 10)
        {

            float diff = (RoomPlayerIsIn.transform.position.x - 10) - (MainCamera.transform.position.x - 10);
            if (diff >= 0.1)
            {
                float result = (diff / 1) / 20;
               // Debug.Log("XDiff: " + diff + ", Result: " + result);
                float x = MainCamera.transform.position.x + result;
                MainCamera.transform.position = new Vector3(x, MainCamera.transform.position.y, MainCamera.transform.position.z);
            }
            else
            {
                MainCamera.transform.position = new Vector3(RoomPlayerIsIn.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z);
            }
        }

        // ROOM SNAP Y
        if (MainCamera.transform.position.y + 6 > RoomPlayerIsIn.transform.position.y + 10)
        {
            //Debug.Log("ABC1");
            float diff = (MainCamera.transform.position.y + 6) - (RoomPlayerIsIn.transform.position.y + 10);
            if (diff >= 0.1)
            {
                float result = (diff / 1) / 20;
                //Debug.Log("YDiff: " + diff + ", Result: " + result);
                float y = MainCamera.transform.position.y - result;
                //MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, RoomPlayerIsIn.transform.position.y + 4, MainCamera.transform.position.z);
                MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, y, MainCamera.transform.position.z);
            }
            else
            {
                MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, RoomPlayerIsIn.transform.position.y + 4, MainCamera.transform.position.z);
            }
        }
        else if (MainCamera.transform.position.y - 6 < RoomPlayerIsIn.transform.position.y - 10)
        {
            //Debug.Log("ABC2");
            float diff = (RoomPlayerIsIn.transform.position.y - 10) - (MainCamera.transform.position.y - 6);
            if (diff >= 0.1)
            {
                float result = (diff / 1) / 20;
                //Debug.Log("YDiff2: " + diff + ", Result2: " + result);
                float y = MainCamera.transform.position.y + result;

                MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, y, MainCamera.transform.position.z);
            }
            else
            {
                MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, RoomPlayerIsIn.transform.position.y - 4, MainCamera.transform.position.z);
            }
        }
        else
        {
            Vector3 temp = MainCamera.transform.position;
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, player.transform.position.y, MainCamera.transform.position.z);
            if (MainCamera.transform.position.y - 6 < RoomPlayerIsIn.transform.position.y - 10 || MainCamera.transform.position.y + 6 > RoomPlayerIsIn.transform.position.y + 10)
            {
                MainCamera.transform.position = temp;
            }
        }
    }
}
