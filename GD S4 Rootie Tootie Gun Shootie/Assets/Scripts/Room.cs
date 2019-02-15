using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    // Start is called before the first frame update
    public RoomInfo roomInfo;
    public Sprite[] TileSprites;
    public RoomTile[] roomTiles;

    void Start()
    {
        TileSprites = Resources.LoadAll<Sprite>(roomInfo.SpriteSheetName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitialiseBrush()
    {
        
    }
}
