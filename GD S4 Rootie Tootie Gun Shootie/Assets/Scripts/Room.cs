using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
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
    List<Enemy> RoomEnemies = new List<Enemy>();
    List<GateScript> Gates = new List<GateScript>();

    void Start()
    {
        NodeTiles = new List<RoomTile>();
        Debug.Log("SpriteSheetName: " + SpriteSheet.name + ", Boss: " + BossRoom + ", Start: " + StarterRoom);
        TileSprites = Resources.LoadAll<Sprite>(SpriteSheet.name);

        for (int i = 0; i < tilemap.transform.childCount; i++)
        {
            GateScript gate = tilemap.transform.GetChild(i).GetComponent<GateScript>();
            if (gate != null)
            {
                Gates.Add(gate);
            }
            GateTriggerScript trigger = tilemap.transform.GetChild(i).GetComponent<GateTriggerScript>();
            if (trigger != null)
            {
                trigger.parentRoom = this;
            }
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
                    if (gate != null)
                    {
                        gate.Node = true;
                    }
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
                    if (gate != null)
                    {
                        gate.Node = false;
                        gate.GetComponent<Animator>().enabled = false;
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

        for (int i = 0; i < transform.childCount; i++)
        {
            Enemy TempEnemy = transform.GetChild(i).GetComponentInChildren<Enemy>();
            if (TempEnemy != null)
            {
                TempEnemy.OnDeath.AddListener(OnDeath);
                RoomEnemies.Add(TempEnemy);
                
            }
        }
    }

    void Update()
    {
        if (RoomEnemies.Count <= 0)
        {
            PermOpenAllGates();
            GameManager.instance.MainCamera.GetComponent<CameraScript>().PlayerMode();
        }
    }

    void OnDeath(Enemy enemy)
    {
        RoomEnemies.Remove(enemy);
    }

    void Awake()
    {
        OpenAllGates();
    }

    public void CloseAllGates()
    {
        if (!StarterRoom)
        {
            Debug.Log(Gates.Count);
            foreach (GateScript gate in Gates)
            {
                gate.LockGate();
            }
        }
    }
    public void PermOpenAllGates()
    {
        if (!StarterRoom)
        {
            foreach (GateScript gate in Gates)
            {
                gate.PermOpenGate();
            }
        }
    }

    public void OpenAllGates()
    {
        Debug.Log(Gates.Count);
        foreach (GateScript gate in Gates)
        {
            gate.NormalOpenGate();
        }
    }

    public void CameraRoomMode()
    {
        if (!GameManager.instance.MainCamera.GetComponent<CameraScript>().InRoomMode)
        {
            GameManager.instance.MainCamera.GetComponent<CameraScript>().RoomMode(this);
        }
    }

}
