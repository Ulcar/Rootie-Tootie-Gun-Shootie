using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class Room : MonoBehaviour
{
    // Start is called before the first frame update
    public RoomInfo roomInfo;
    public Sprite[] TileSprites;
    public List<RoomTile> Tiles;
    public Tilemap tilemap; 
    [Range(0, 2)]
    public int index;

    void Start()
    {
        Tiles = new List<RoomTile>();
        
        for (int i = 0; i < tilemap.transform.childCount; i++) 
        {
            Transform child = tilemap.transform.GetChild(i);
            Tiles.Add(child.GetComponent<RoomTile>());
            Tiles[i].spriteRenderer = child.GetComponent<SpriteRenderer>();
        }
        TileSprites = Resources.LoadAll<Sprite>(roomInfo.SpriteSheetName.name);
        //vul grid met tilemap
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Tiles.Count; i ++) { 
            Tiles[i].spriteRenderer.sprite = TileSprites[Tiles[i].ID];
        }

    }
}
