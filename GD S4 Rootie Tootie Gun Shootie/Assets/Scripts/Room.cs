using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
public class Room : MonoBehaviour
{
    // Start is called before the first frame update
    public bool permOpen = false;
    public Texture2D SpriteSheet;
    public bool BossRoom;
    public bool StarterRoom;
    public bool ShopRoom;
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
    int TotalSpawnedEnemiesInRoom = 0;
    List<Enemy> RoomEnemies = new List<Enemy>();
    List<GateScript> Gates = new List<GateScript>();
    public int CoinsForRoom;
    public int HealthForRoom;
    

    void Start()
    {
        NodeTiles = new List<RoomTile>();
        //Debug.Log("SpriteSheetName: " + SpriteSheet.name + ", Boss: " + BossRoom + ", Start: " + StarterRoom);
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
                TotalSpawnedEnemiesInRoom++;
            }
        }
    }

    void Update()
    {
        if (RoomEnemies.Count <= 0 && TotalSpawnedEnemiesInRoom > 0)
        {
            if (!permOpen)
            {
                PermOpenAllGates();
                GameManager.instance.MainCamera.GetComponent<CameraScript>().PlayerMode();
            }
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
            //Debug.Log(Gates.Count);
            foreach (GateScript gate in Gates)
            {
                gate.LockGate();
            }
            if (GameManager.instance.MainCamera.GetComponent<CameraScript>().currentState == CameraScript.States.Player && !permOpen)
            {
                CameraRoomMode();
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
            permOpen = true;
        }
    }

    public void OpenAllGates()
    {
        //Debug.Log(Gates.Count);
        foreach (GateScript gate in Gates)
        {
            gate.NormalOpenGate();
        }
    }

    public void EnableEnemies()
    {
        foreach (Enemy enemy in RoomEnemies)
        {
            enemy.transform.parent.gameObject.SetActive(true);
        }
    }

    public void CameraRoomMode()
    {
        if (!GameManager.instance.MainCamera.GetComponent<CameraScript>().InRoomMode)
        {
            GameManager.instance.MainCamera.GetComponent<CameraScript>().RoomMode(this);
            GameManager.instance.RoomPlayerIsIn = this;
        }
    }

    public void DivideResourcesOverMobs()
    {
        if (CoinsForRoom > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                
                EnemyAIController temp = transform.GetChild(i).GetComponent<EnemyAIController>();
                if (temp != null && CoinsForRoom > 0)
                {
                    int Bounty = Mathf.RoundToInt((GameManager.instance.floorHandler.floorInfo.coinBaseline * temp.enemy.CoinWeight) * Random.Range(0.80f, 1.20f));
                    if (CoinsForRoom - Bounty < 0)
                    {
                        temp.enemy.CoinBounty = CoinsForRoom;
                        CoinsForRoom = 0;
                        i = transform.childCount;
                    }
                    else
                    {
                        temp.enemy.CoinBounty = Bounty;
                        CoinsForRoom -= Bounty;
                    }
                }
                else
                {
                    Debug.Log("NULL");
                }
            }
        }
        if (HealthForRoom > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                EnemyAIController temp = transform.GetChild(i).GetComponent<EnemyAIController>();
                if (temp != null && HealthForRoom > 0)
                {
                    int Bounty = Random.Range(1,GameManager.instance.floorHandler.floorInfo.healthBaseline);
                    if (HealthForRoom - Bounty < 0)
                    {
                        temp.enemy.HealthBounty = HealthForRoom;
                        HealthForRoom = 0;
                        i = transform.childCount;
                    }
                    else
                    {
                        temp.enemy.HealthBounty = Bounty;
                        HealthForRoom -= Bounty;
                    }
                }
            }
        }
    }

}
