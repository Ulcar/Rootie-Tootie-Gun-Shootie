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
    [Range(0, 2)]
    public int index;

    void Start()
    {
        NodeTiles = new List<RoomTile>();
        TileSprites = Resources.LoadAll<Sprite>(SpriteSheet.name);
        
        for (int i = 0; i < tilemap.transform.childCount; i++) 
        {
            RoomTile tile = tilemap.transform.GetChild(i).GetComponent<RoomTile>();
            if (tile.Node)
            {
                NodeTiles.Add(tile);
            }
            tile.spriteRenderer.sprite = TileSprites[tile.ID];
            
        }
        
        foreach (GenerationNode GenNode in RepresentedRoom.Nodes)
        {
            for (int i = 0; i < NodeTiles.Count; i++)
            {
                if (NodeTiles[i].Direction == GenNode.exitDirection)
                {
                    NodeTiles[i].spriteRenderer.sprite = TileSprites[NodeTiles[i].NodeID];
                }
            }
        }
        

    }
}
