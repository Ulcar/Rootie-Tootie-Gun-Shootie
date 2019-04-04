using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RoomTile : MonoBehaviour
{
    public int ID;
    public SpriteRenderer spriteRenderer;
    public bool Node;
    public ExitDirections Direction;
    public int NodeID;
    public bool ColliderIfNotNode = false;
    public bool playerSpawnPoint;
    public bool monsterSpawnPoint;
    public bool DefaultLayer;
    public bool ignore;
    public bool ShopTile;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

}
