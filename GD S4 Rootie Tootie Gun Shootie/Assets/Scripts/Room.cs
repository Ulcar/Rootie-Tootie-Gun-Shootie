using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class Room : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D SpriteSheet;
    public bool BossRoom;
    public bool StarterRoom;
    public Sprite[] TileSprites;
    public List<RoomTile> NodeTiles;
    public Tilemap tilemap;
    public GenerationRoom RepresentedRoom;
    public List<GameObject> enemiesToSpawn;
    [Range(0, 2)]
    public int index;
    public int respresentedX;
    public int respresentedY;
    public int representedRoomIndex;
    public bool Exclusive;
    public int AmountOfMobsInRoom;

    void Start()
    {
        NodeTiles = new List<RoomTile>();
        Debug.Log("SpriteSheetName: " + SpriteSheet.name + ", Boss: " + BossRoom + ", Start: " + StarterRoom);
        TileSprites = Resources.LoadAll<Sprite>(SpriteSheet.name);

        for (int i = 0; i < tilemap.transform.childCount; i++)
        {
            RoomTile tile = tilemap.transform.GetChild(i).GetComponent<RoomTile>();
            //Debug.Log("TileID: " + tile.ID + ", TileNodeID: " + tile.NodeID);
            if (tile.Node)
            {
                bool Node = false;
                foreach (GenerationNode GenNode in FloorHandler.generationRooms[respresentedX,respresentedY].Nodes)
                {
                    if (GenNode.exitDirection == tile.Direction)
                    {
                        Node = true;
                    }
                }
                if (Node)
                {
                    tile.spriteRenderer.sprite = TileSprites[tile.NodeID];
                }
                else
                {
                    if (tile.ColliderIfNotNode)
                    {
                        tilemap.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
                        if (tile.DefaultLayer == false)
                        {
                            tilemap.transform.GetChild(i).gameObject.layer = 9;
                        }
                    }
                    tile.spriteRenderer.sprite = TileSprites[tile.ID];
                }
            }
            else if (!tile.Node)
            {
                if (tile.ColliderIfNotNode)
                {
                    tilemap.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
                    if (tile.DefaultLayer == false)
                    {
                        tilemap.transform.GetChild(i).gameObject.layer = 9;
                    }
                }
                tile.spriteRenderer.sprite = TileSprites[tile.ID];
            }
            
            
        }
    }

}
