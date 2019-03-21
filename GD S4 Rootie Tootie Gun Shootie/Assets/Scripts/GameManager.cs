using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FloorInfo[] FloorInfos;
    public static GameManager instance;
    public int Floor = 0;
    public FloorHandler floorHandler;
    RoomGenerator roomGenerator;
    public Player player;
    public Camera MainCamera;

    public Sprite[] updownIdleOpenMovingWallSprites;
    public Sprite[] updownIdleClosedMovingWallSprites;
    public Sprite[] leftrightIdleOpenMovingWallSprites;
    public Sprite[] leftrightIdleClosedMovingWallSprites;

    bool scan = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (floorHandler.floorDone && !scan)
        {
            AstarPath.active.Scan();
            scan = true;
        }
    }

    void SetupPlayer()
    {
        
    }


}
