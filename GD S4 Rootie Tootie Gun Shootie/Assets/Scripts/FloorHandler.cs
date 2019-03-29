using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorHandler : MonoBehaviour
{
    public Room CurrentFloorStartRoom;
    public Room CurrentFloorBossRoom;
    public bool floorDone = false;
    public int AmountOfTilesHorizontal;
    public FloorInfo floorInfo;
    //public Room TestRoom;
    public Room[] Rooms;
    public Room[] BossRooms;
    public List<ExitDirections>[] RoomDirections;

    public GameObject GridForAStar;
    bool BossRoomDone = false;
    int DirectionValue = -1;
    int x;
    int y;
    public static GenerationRoom[,] generationRooms;
    List<GenerationRoom> Path;
    int AmountOfRoomsGenerated = 0;
    int ActuallyGenerated = 0;

    int FloorCoinAmountLeft;
    int FloorHearthAmountLeft;
    [SerializeField]
    EnemyGeneration generation;
 
    // Start is called before the first frame update

    public FloorHandler(FloorInfo info)
    {
        floorInfo = info;
    }
    void Start()
    {
        FloorCoinAmountLeft = floorInfo.CoinsForFloor;
        FloorHearthAmountLeft = floorInfo.HealthDropsForFloor;
        Path = new List<GenerationRoom>();
        generationRooms = new GenerationRoom[floorInfo.FloorWidthHeight, floorInfo.FloorWidthHeight];

        for (int i = 0; i < floorInfo.FloorWidthHeight; i++)
        {
            for (int j = 0; j < floorInfo.FloorWidthHeight; j++)
            {
                generationRooms[i, j] = null;
            }
        }

        x = Random.Range(0, floorInfo.FloorWidthHeight);
        y = Random.Range(0, floorInfo.FloorWidthHeight);
        SetupNodeSystem();
        GeneratePath();
        /*
        for (int i = 0; i < 5; i++)
        {
            string idk = "i: " + i.ToString() + ", ";
            for (int j = 0; j < 5; j++)
            {
                if (generationRooms[i, j] == null)
                {
                    idk = idk + ", NULL";
                }
                else
                {
                    if (generationRooms[i, j].starterRoom)
                    {
                        idk = idk + ", START" + generationRooms[i, j].ID;
                    }
                    else if (generationRooms[i, j].bossRoom)
                    {
                        idk = idk + ", BOSS" + generationRooms[i, j].ID;
                    }
                    else
                    {
                        idk = idk + ", ROOM" + generationRooms[i, j].ID;
                    }
                        
                    bool up = false;
                    bool right = false;
                    bool down = false;
                    bool left = false;
                    foreach (GenerationNode node in generationRooms[i, j].Nodes)
                    {
                        if (node.exitDirection == ExitDirections.Up && !up)
                        {
                            idk = idk + ">";
                            up = true;
                        }
                        if (node.exitDirection == ExitDirections.Right && !right)
                        {
                            idk = idk + "V";
                            right = true;
                        }
                        if (node.exitDirection == ExitDirections.Down && !down)
                        {
                            idk = idk + "<";
                            down = true;
                        }
                        if (node.exitDirection == ExitDirections.Left && !left)
                        {
                            idk = idk + "^";
                            left = true;
                        }
                    }
                }
                
            }
            Debug.Log(idk);
            
        }
        */
        floorDone = true;
    }

    public void GeneratePath()
    {  
        ExitDirections[] exitDirections = { ExitDirections.Up, ExitDirections.Right, ExitDirections.Down, ExitDirections.Left };

        //Debug.Log("X at start: " + x.ToString());
        //Debug.Log("Y at start: " + y.ToString());

        
        GenerateRoom(exitDirections);

        GenerateFloor(floorInfo.Floor1SpriteSheet);
        
    }

    public void GenerateFloor(Texture2D Spritesheet)
    {
        float offset = (Rooms[0].tilemap.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x) * AmountOfTilesHorizontal;
        //AstarPath Astar = GridForAStar.GetComponent<AstarPath>();
        
        for (int i = 0; i < floorInfo.FloorWidthHeight; i++)
        {
            for (int j = 0; j < floorInfo.FloorWidthHeight; j++)
            {
                if (generationRooms[i, j] != null)
                {
                    Room temp;
                    List<int> PossibleRooms = new List<int>();
                    if (generationRooms[i, j].starterRoom)
                    {
                        temp = floorInfo.StarterRoom;
                        //temp.representedRoomIndex = 0;
                    }
                    else
                    {
                        for (int k = 0; k < Rooms.Length; k++)
                        {
                            bool AddRoom = true;

                            //if (generationRooms[i, j].Nodes.Count == RoomDirections[k].Count)
                            //{

                            if (Rooms[k].Exclusive)
                            {
                                if (generationRooms[i, j].Nodes.Count == RoomDirections[k].Count)
                                {
                                    foreach (GenerationNode node in generationRooms[i, j].Nodes)
                                    {
                                        bool NodeInRightDirection = false;
                                        foreach (ExitDirections direction in RoomDirections[k])
                                        {
                                            if (direction == node.exitDirection)
                                            {
                                                NodeInRightDirection = true;
                                            }
                                        }
                                        if (!NodeInRightDirection)
                                        {
                                            AddRoom = false;
                                        }
                                    }
                                }
                                else
                                {
                                    AddRoom = false;
                                }
                            }
                            else
                            {
                                foreach (GenerationNode node in generationRooms[i, j].Nodes)
                                {
                                    bool NodeInRightDirection = false;
                                    foreach (ExitDirections direction in RoomDirections[k])
                                    {
                                        if (direction == node.exitDirection)
                                        {
                                            NodeInRightDirection = true;
                                        }
                                    }
                                    if (!NodeInRightDirection)
                                    {
                                        AddRoom = false;
                                    }
                                }
                            }
                            /*}
                           else
                           {
                               AddRoom = false;
                           }*/

                            if (AddRoom)
                            {
                                PossibleRooms.Add(k);
                            }
                        }
                        int index = PossibleRooms[Random.Range(0, PossibleRooms.Count)];
                        temp = Rooms[index];
                        temp.representedRoomIndex = index;
                    }
                        
                    temp.RepresentedRoom = generationRooms[i, j];
                    temp.SpriteSheet = Spritesheet;
                    temp.respresentedX = i;
                    temp.respresentedY = j;
                    Room RoomToInstantiate = Instantiate(temp);
                    //Debug.Log("RTI: " + RoomToInstantiate.respresentedX);
                    //Debug.Log("TempName: " + temp.SpriteSheet.name);

                    RoomToInstantiate.transform.position = new Vector3(i * offset, j * offset, 0);

                    RoomToInstantiate.transform.SetParent(this.transform);
                    if (generationRooms[i, j].bossRoom)
                    {
                        RoomToInstantiate.BossRoom = true;
                        
                        CurrentFloorBossRoom = RoomToInstantiate;
                    }
                    else if (generationRooms[i, j].starterRoom)
                    {
                        RoomToInstantiate.StarterRoom = true;
                        CurrentFloorStartRoom = RoomToInstantiate;
                        List<RoomTile> Spawnpoints = new List<RoomTile>();
                        //Debug.Log("RoomChildCount: " + RoomToInstantiate.tilemap.transform.childCount);
                        for (int l = 0; l < RoomToInstantiate.tilemap.transform.childCount; l++)
                        {
                            RoomTile tile = RoomToInstantiate.tilemap.transform.GetChild(l).GetComponent<RoomTile>();
                            if (tile == null)
                            {
                                Debug.Log("Tile is null");
                            }
                            //Debug.Log(tile.playerSpawnPoint);
                            if (tile.playerSpawnPoint)
                            {
                                Spawnpoints.Add(tile);
                            }
                        }
                        int RandomRange = Random.Range(0, Spawnpoints.Count);
                        //Debug.Log("RandomRange: " + RandomRange);
                        //Debug.Log("Spawnpoints: " + Spawnpoints.Count);
                        Transform chosenSpawnPoint = Spawnpoints[RandomRange].transform;

                        GameManager.instance.player.transform.position = new Vector3(chosenSpawnPoint.position.x + Rooms[0].tilemap.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x / 2, chosenSpawnPoint.position.y + Rooms[0].tilemap.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y / 2, 0);
                        GameManager.instance.MainCamera.transform.position = new Vector3(GameManager.instance.player.transform.position.x, GameManager.instance.player.transform.position.y, -10);

                        GameManager.instance.RoomPlayerIsIn = RoomToInstantiate;
                    }
                    else
                    {
                        List<RoomTile> MobSpawnPoints = new List<RoomTile>();
                        for (int l = 0; l < RoomToInstantiate.tilemap.transform.childCount; l++)
                        {
                            RoomTile tile = RoomToInstantiate.tilemap.transform.GetChild(l).GetComponent<RoomTile>();
                            if (tile.monsterSpawnPoint)
                            {
                                MobSpawnPoints.Add(tile);
                            }
                        }
                        for (int MobCount = 0; MobCount < RoomToInstantiate.AmountOfMobsInRoom; MobCount++)
                        {
                            // Spawn enemy here
                            //should randomise enemy type later
                            int SpawnIndex = Random.Range(0, MobSpawnPoints.Count);
                         GameObject enemy =   SpawnEnemy(EnemyType.Ranged, RoomToInstantiate.transform, new Vector3(MobSpawnPoints[SpawnIndex].transform.position.x, MobSpawnPoints[SpawnIndex].transform.position.y, -2));
                            RoomToInstantiate.enemiesToSpawn.Add(enemy);
                            enemy.SetActive(false);


                            //old code commented out
                           /* GameObject EnemyToInstantiate = Instantiate(floorInfo.Enemies[Random.Range(0,floorInfo.Enemies.Length)]);
                            EnemyToInstantiate.GetComponent<EnemyAIController>().target = GameManager.instance.player.transform;
                            int SpawnIndex = Random.Range(0, MobSpawnPoints.Count);
                            EnemyToInstantiate.transform.position =;
                            EnemyToInstantiate.transform.parent = RoomToInstantiate.transform;
                            MobSpawnPoints.Remove(MobSpawnPoints[SpawnIndex]); */

                        }
                    }
                }
            }
        }

        int Temp = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Room>().BossRoom == false && transform.GetChild(i).GetComponent<Room>().ShopRoom == false && transform.GetChild(i).GetComponent<Room>().StarterRoom == false)
            {
                Temp += 1;
            }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Room>().BossRoom == false && transform.GetChild(i).GetComponent<Room>().ShopRoom == false && transform.GetChild(i).GetComponent<Room>().StarterRoom == false)
            {
                transform.GetChild(i).GetComponent<Room>().CoinsForRoom = Mathf.RoundToInt((floorInfo.CoinsForFloor / Temp) * Random.Range(0.70f, 1.30f));
            }
        }
        for (int i = 0; i < 30; i++)
        {
            if (FloorHearthAmountLeft > 0)
            {
                int RandomNumber = Random.Range(0, transform.childCount);
                if (transform.GetChild(RandomNumber).GetComponent<Room>().BossRoom == false && transform.GetChild(RandomNumber).GetComponent<Room>().ShopRoom == false && transform.GetChild(RandomNumber).GetComponent<Room>().StarterRoom == false)
                {
                    transform.GetChild(RandomNumber).GetComponent<Room>().HealthForRoom += 1;
                    FloorHearthAmountLeft -= 1;
                }
            }
            else
            {
                break;
            }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Room>().DivideResourcesOverMobs();
        }
    }
    //enemy Spawning code, using EnemyGeneration Class
    GameObject SpawnEnemy(EnemyType type, Transform parent, Vector3 spawnPosition)
    {
        GameObject tmp = generation.GenerateEnemyPrefab(type);
        GameObject enemyToSpawn = Instantiate(tmp);
        EnemyStats stats = generation.GenerateEnemyStats(type);
        enemyToSpawn.GetComponentInChildren<Enemy>().Init(stats);
        enemyToSpawn.transform.position = spawnPosition;
        enemyToSpawn.transform.parent = parent;
        return enemyToSpawn;
    }

    public void SetupNodeSystem()
    {
        RoomDirections = new List<ExitDirections>[Rooms.Length];
        for (int i = 0; i < RoomDirections.Length; i++)
        {
            RoomDirections[i] = new List<ExitDirections>();
        }
        for (int i = 0; i < Rooms.Length; i++)
        {
            bool Up = false;
            bool Right = false;
            bool Left = false;
            bool Down = false;
            for (int k = 0; k < Rooms[i].tilemap.transform.childCount; k++)
            {
                if (Rooms[i].tilemap.transform.GetChild(k).GetComponent<RoomTile>().Node)
                {
                    if (Rooms[i].tilemap.transform.GetChild(k).GetComponent<RoomTile>().Direction == ExitDirections.Up)
                    {
                        Up = true;
                    }
                    if (Rooms[i].tilemap.transform.GetChild(k).GetComponent<RoomTile>().Direction == ExitDirections.Right)
                    {
                        Right = true;
                    }
                    if (Rooms[i].tilemap.transform.GetChild(k).GetComponent<RoomTile>().Direction == ExitDirections.Down)
                    {
                        Down = true;
                    }
                    if (Rooms[i].tilemap.transform.GetChild(k).GetComponent<RoomTile>().Direction == ExitDirections.Left)
                    {
                        Left = true;
                    }
                }
            }
            if (Up)
            {
                RoomDirections[i].Add(ExitDirections.Up);
            }
            if (Right)
            {
                RoomDirections[i].Add(ExitDirections.Right);
            }
            if (Down)
            {
                RoomDirections[i].Add(ExitDirections.Down);
            }
            if (Left)
            {
                RoomDirections[i].Add(ExitDirections.Left);
            }
        }
    }

    public void GenerateRoom(ExitDirections[] DirectionsArray)
    {
       // Debug.Log("Amount of rooms generated: " + AmountOfRoomsGenerated);


        {
            if (AmountOfRoomsGenerated == 0)
            {
                //StarterRoom
                ActuallyGenerated++;
                GenerationRoom StarterRoom = new GenerationRoom(x, y, true, false, ActuallyGenerated);
                //Debug.Log("StarterRoomX: " + StarterRoom.x + ", StarterRoomY: " + StarterRoom.y);
                if (CheckAvailability())
                {
                    StarterRoom.Nodes.Add(new GenerationNode(DirectionsArray[DirectionValue]));
                    generationRooms[x, y] = StarterRoom;
                    IncrementXY(DirectionValue);
                    AmountOfRoomsGenerated++;
                    GenerateRoom(DirectionsArray);
                }
                else
                {
                    Debug.Log("Unable to add node because direction is not available, exiting.");
                    return;
                }
                

            }
            else
            {
                if (AmountOfRoomsGenerated >= floorInfo.RoomsInFloor1 && BossRoomDone == false)
                {
                   
                    BossRoomDone = true;
                    ActuallyGenerated++;
                    GenerationRoom randomRoom = new GenerationRoom(x, y, false, true, ActuallyGenerated);
                    if (randomRoom.y + 1 < floorInfo.FloorWidthHeight)
                    {
                        
                        if (generationRooms[randomRoom.x, randomRoom.y + 1] != null)
                        {
                            if (generationRooms[randomRoom.x, randomRoom.y + 1].BossRoomEntryRoom)
                            {
                                GenerationNode node = new GenerationNode(ExitDirections.Up);
                                foreach (GenerationNode otherNode in generationRooms[randomRoom.x, randomRoom.y + 1].Nodes)
                                {
                                    if (otherNode.exitDirection == ExitDirections.Down)
                                    {
                                        node.ConnectNodes(otherNode);
                                        otherNode.ConnectNodes(node);
                                    }
                                }
                                randomRoom.Nodes.Add(node);
                            }
                        }
                    }
                    if (randomRoom.x + 1 < floorInfo.FloorWidthHeight)
                    {
                        if (generationRooms[randomRoom.x + 1, randomRoom.y] != null)
                        {
                            if (generationRooms[randomRoom.x + 1, randomRoom.y].BossRoomEntryRoom)
                            {
                                GenerationNode node = new GenerationNode(ExitDirections.Right);
                                foreach (GenerationNode otherNode in generationRooms[randomRoom.x + 1, randomRoom.y].Nodes)
                                {
                                    if (otherNode.exitDirection == ExitDirections.Left)
                                    {
                                        node.ConnectNodes(otherNode);
                                        otherNode.ConnectNodes(node);
                                    }
                                }
                                randomRoom.Nodes.Add(node);
                            }
                        }
                    }
                    if (randomRoom.y - 1 >= 0)
                    {
                        if (generationRooms[randomRoom.x, randomRoom.y - 1] != null)
                        {
                            if (generationRooms[randomRoom.x, randomRoom.y - 1].BossRoomEntryRoom)
                            {
                                GenerationNode node = new GenerationNode(ExitDirections.Down);
                                foreach (GenerationNode otherNode in generationRooms[randomRoom.x, randomRoom.y - 1].Nodes)
                                {
                                    if (otherNode.exitDirection == ExitDirections.Up)
                                    {
                                        node.ConnectNodes(otherNode);
                                        otherNode.ConnectNodes(node);
                                    }
                                }
                                randomRoom.Nodes.Add(node);
                            }
                        }
                    }
                    if (randomRoom.x - 1 >= 0)
                    {
                        if (generationRooms[randomRoom.x - 1, randomRoom.y] != null)
                        {
                            if (generationRooms[randomRoom.x - 1, randomRoom.y].BossRoomEntryRoom)
                            {
                                GenerationNode node = new GenerationNode(ExitDirections.Left);
                                foreach (GenerationNode otherNode in generationRooms[randomRoom.x - 1, randomRoom.y].Nodes)
                                {
                                    if (otherNode.exitDirection == ExitDirections.Right)
                                    {
                                        node.ConnectNodes(otherNode);
                                        otherNode.ConnectNodes(node);
                                    }
                                }
                                randomRoom.Nodes.Add(node);
                            }
                        }
                    }

                    if (randomRoom.y + 1 < floorInfo.FloorWidthHeight)
                    {
                        if (generationRooms[randomRoom.x, randomRoom.y + 1] != null)
                        {
                            for (int i = 0; i < generationRooms[randomRoom.x, randomRoom.y + 1].Nodes.Count; i++)
                            {
                                if (generationRooms[randomRoom.x, randomRoom.y + 1].Nodes[i].exitDirection == ExitDirections.Down)
                                {
                                    if (randomRoom.Nodes[0].ConnectedNode != generationRooms[randomRoom.x, randomRoom.y + 1].Nodes[i])
                                    {
                                        generationRooms[randomRoom.x, randomRoom.y + 1].Nodes.Remove(generationRooms[randomRoom.x, randomRoom.y + 1].Nodes[i]);
                                    }
                                }
                            }
                        }
                    }
                    if (randomRoom.x + 1 < floorInfo.FloorWidthHeight)
                    {
                        if (generationRooms[randomRoom.x + 1, randomRoom.y] != null)
                        {
                            for (int i = 0; i < generationRooms[randomRoom.x + 1, randomRoom.y].Nodes.Count; i++)
                            {
                                if (generationRooms[randomRoom.x + 1, randomRoom.y].Nodes[i].exitDirection == ExitDirections.Left)
                                {
                                    if (randomRoom.Nodes[0].ConnectedNode != generationRooms[randomRoom.x + 1, randomRoom.y].Nodes[i])
                                    {
                                        generationRooms[randomRoom.x + 1, randomRoom.y].Nodes.Remove(generationRooms[randomRoom.x + 1, randomRoom.y].Nodes[i]);
                                    }
                                }
                            }
                        }
                    }
                    if (randomRoom.y - 1 >= 0)
                    {
                        if (generationRooms[randomRoom.x, randomRoom.y - 1] != null)
                        {
                            for (int i = 0; i <  generationRooms[randomRoom.x, randomRoom.y - 1].Nodes.Count; i++)
                            {
                                if (generationRooms[randomRoom.x, randomRoom.y - 1].Nodes[i].exitDirection == ExitDirections.Up)
                                {
                                    if (randomRoom.Nodes[0].ConnectedNode != generationRooms[randomRoom.x, randomRoom.y - 1].Nodes[i])
                                    {
                                        generationRooms[randomRoom.x, randomRoom.y - 1].Nodes.Remove(generationRooms[randomRoom.x, randomRoom.y - 1].Nodes[i]);
                                    }
                                }
                            }
                        }
                    }

                    if (randomRoom.x - 1 >= 0)
                    {
                        if (generationRooms[randomRoom.x - 1, randomRoom.y] != null)
                        {
                            for (int i = 0; i < generationRooms[randomRoom.x - 1, randomRoom.y].Nodes.Count; i++)
                            {
                                if (generationRooms[randomRoom.x - 1, randomRoom.y].Nodes[i].exitDirection == ExitDirections.Right)
                                {
                                    if (randomRoom.Nodes[0].ConnectedNode != generationRooms[randomRoom.x - 1, randomRoom.y].Nodes[i])
                                    {
                                        generationRooms[randomRoom.x - 1, randomRoom.y].Nodes.Remove(generationRooms[randomRoom.x - 1, randomRoom.y].Nodes[i]);
                                    }
                                }
                            }
                        }
                    }
                    generationRooms[randomRoom.x, randomRoom.y] = randomRoom;
                }
                else if (ActuallyGenerated <= floorInfo.RoomsInFloor1 && ActuallyGenerated != 0)
                {
                    //Regular room
                    ActuallyGenerated++;
                    GenerationRoom randomRoom = new GenerationRoom(x, y, false, false, ActuallyGenerated);
                    
                   // Debug.Log("randomRoomNr: " + AmountOfRoomsGenerated + ", X: " + randomRoom.x + ", Y: " + randomRoom.y);
                    //Debug.Log("randomRoomNr: " + AmountOfRoomsGenerated + ", CurrentX: " + x + ", CurrentY: " + y);
                    bool NorthAvailable = true;
                    bool EastAvailable = true;
                    bool SouthAvailable = true;
                    bool WestAvailable = true;
                    if (x + 1 >= floorInfo.FloorWidthHeight)
                    {
                        //Debug.Log("DebugXEast: " + x + ", DebugYEast: " + y + ", FloorWidthHeight: " + floorInfo.FloorWidthHeight);
                        EastAvailable = false;
                    }
                    else
                    {
                        //Debug.Log("DebugXEast: " + x + ", DebugYEast: " + y);
                        if (generationRooms[x + 1, y] != null)
                        {
                            if (generationRooms[x + 1, y].bossRoom == false)
                            {
                                //Debug.Log("ABC0");
                                EastAvailable = false;
                                GenerationNode node = new GenerationNode(ExitDirections.Right);
                                foreach (GenerationNode OtherNode in generationRooms[x + 1, y].Nodes)
                                {
                                    if (OtherNode.exitDirection == ExitDirections.Left)
                                    {
                                        node.ConnectNodes(OtherNode);
                                        OtherNode.ConnectNodes(node);
                                        randomRoom.Nodes.Add(node);
                                       // Debug.Log("CBA0");
                                    }
                                }
                            }
                        }
                    }
                    if (x - 1 <= -1)
                    {
                        //Debug.Log("DebugXWest: " + x + ", DebugYWest: " + y + ", FloorWidthHeight: " + floorInfo.FloorWidthHeight);
                        WestAvailable = false;
                    }
                    else
                    {
                       // Debug.Log("DebugXWest: " + x + ", DebugYWest: " + y);
                        if (generationRooms[x - 1, y] != null)
                        {
                            if (generationRooms[x - 1, y].bossRoom == false)
                            {
                                //Debug.Log("ABC1");
                                WestAvailable = false;
                                GenerationNode node = new GenerationNode(ExitDirections.Left);
                                foreach (GenerationNode OtherNode in generationRooms[x - 1, y].Nodes)
                                {
                                    if (OtherNode.exitDirection == ExitDirections.Right)
                                    {
                                        node.ConnectNodes(OtherNode);
                                        OtherNode.ConnectNodes(node);
                                        randomRoom.Nodes.Add(node);
                                        //Debug.Log("CBA1");
                                    }
                                }
                            }
                        }
                    }
                    if (y + 1 >= floorInfo.FloorWidthHeight)
                    {
                        //Debug.Log("DebugXNorth: " + x + ", DebugYNorth: " + y + ", FloorWidthHeight: " + floorInfo.FloorWidthHeight);
                        NorthAvailable = false;
                    }
                    else
                    {
                       // Debug.Log("DebugXNorth: " + x + ", DebugYNorth: " + y);
                        if (generationRooms[x, y + 1] != null)
                        {
                            if (generationRooms[x, y + 1].bossRoom == false)
                            {
                               // Debug.Log("ABC2");
                                NorthAvailable = false;
                                GenerationNode node = new GenerationNode(ExitDirections.Up);
                                foreach (GenerationNode OtherNode in generationRooms[x, y + 1].Nodes)
                                {
                                    if (OtherNode.exitDirection == ExitDirections.Down)
                                    {
                                        node.ConnectNodes(OtherNode);
                                        OtherNode.ConnectNodes(node);
                                        randomRoom.Nodes.Add(node);
                                        //Debug.Log("CBA2");
                                    }
                                }
                            }
                        }
                    }
                    if (y - 1 <= -1)
                    {
                        //Debug.Log("DebugXSouth: " + x + ", DebugYSouth: " + y + ", FloorWidthHeight: " + floorInfo.FloorWidthHeight);
                        SouthAvailable = false;
                    }
                    else
                    {
                       // Debug.Log("DebugXSouth: " + x + ", DebugYSouth: " + y);
                        if (generationRooms[x, y - 1] != null)
                        {
                            if (generationRooms[x, y - 1].bossRoom == false)
                            {
                                //Debug.Log("ABC3");
                                SouthAvailable = false;
                                GenerationNode node = new GenerationNode(ExitDirections.Down);
                                foreach (GenerationNode OtherNode in generationRooms[x, y - 1].Nodes)
                                {
                                    if (OtherNode.exitDirection == ExitDirections.Up)
                                    {
                                        node.ConnectNodes(OtherNode);
                                        OtherNode.ConnectNodes(node);
                                        randomRoom.Nodes.Add(node);
                                        //Debug.Log("CBA3");
                                    }
                                }
                            }
                            
                        }
                    }
                    //Debug.Log("North: " + NorthAvailable + ", East: " + EastAvailable + ", South: " + SouthAvailable + ", West: " + WestAvailable);
                    int available = (NorthAvailable ? 1 : 0) + (EastAvailable ? 1 : 0) + (SouthAvailable ? 1 : 0) + (WestAvailable ? 1 : 0);
                    //Debug.Log("AmountGenerated: " + AmountOfRoomsGenerated + ", AvailableNodes: " + available);
                    for (int i = 0; i < available; i++)
                    {
                        if (AmountOfRoomsGenerated >= Mathf.CeilToInt(floorInfo.RoomsInFloor1 / 1.5f))
                        {
                            if (Random.Range(0, 2) == 1)
                            {
                                for (int k = 0; k < 30; k++)
                                {
                                    int randNumber = Random.Range(0, 4);
                                    if (randNumber == 0 && NorthAvailable == true && AmountOfRoomsGenerated + 1 <= floorInfo.RoomsInFloor1)
                                    {
                                        randomRoom.Nodes.Add(new GenerationNode(ExitDirections.Up));
                                        AmountOfRoomsGenerated++;
                                        if (AmountOfRoomsGenerated == floorInfo.RoomsInFloor1)
                                        {
                                            randomRoom.BossRoomEntryRoom = true;
                                        }
                                        NorthAvailable = false;
                                        break;
                                    }
                                    if (randNumber == 1 && EastAvailable == true && AmountOfRoomsGenerated + 1 <= floorInfo.RoomsInFloor1)
                                    {
                                        randomRoom.Nodes.Add(new GenerationNode(ExitDirections.Right));
                                        AmountOfRoomsGenerated++;
                                        if (AmountOfRoomsGenerated == floorInfo.RoomsInFloor1)
                                        {
                                            randomRoom.BossRoomEntryRoom = true;
                                        }
                                        EastAvailable = false;
                                        break;
                                    }
                                    if (randNumber == 2 && SouthAvailable == true && AmountOfRoomsGenerated + 1 <= floorInfo.RoomsInFloor1)
                                    {
                                        randomRoom.Nodes.Add(new GenerationNode(ExitDirections.Down));
                                        AmountOfRoomsGenerated++;
                                        if (AmountOfRoomsGenerated == floorInfo.RoomsInFloor1)
                                        {
                                            randomRoom.BossRoomEntryRoom = true;
                                        }
                                        SouthAvailable = false;
                                        break;
                                    }
                                    if (randNumber == 3 && WestAvailable == true && AmountOfRoomsGenerated + 1 <= floorInfo.RoomsInFloor1)
                                    {
                                        randomRoom.Nodes.Add(new GenerationNode(ExitDirections.Left));
                                        AmountOfRoomsGenerated++;
                                        if (AmountOfRoomsGenerated == floorInfo.RoomsInFloor1)
                                        {
                                            randomRoom.BossRoomEntryRoom = true;
                                        }
                                        WestAvailable = false;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int k = 0; k < 30; k++)
                            {
                                int randNumber = Random.Range(0, 4);
                                if (randNumber == 0 && NorthAvailable == true && AmountOfRoomsGenerated + 1 <= floorInfo.RoomsInFloor1)
                                {
                                    randomRoom.Nodes.Add(new GenerationNode(ExitDirections.Up));
                                    AmountOfRoomsGenerated++;
                                    if (AmountOfRoomsGenerated == floorInfo.RoomsInFloor1)
                                    {
                                        randomRoom.BossRoomEntryRoom = true;
                                    }
                                    NorthAvailable = false;
                                    break;
                                }
                                if (randNumber == 1 && EastAvailable == true && AmountOfRoomsGenerated + 1 <= floorInfo.RoomsInFloor1)
                                {
                                    randomRoom.Nodes.Add(new GenerationNode(ExitDirections.Right));
                                    AmountOfRoomsGenerated++;
                                    if (AmountOfRoomsGenerated == floorInfo.RoomsInFloor1)
                                    {
                                        randomRoom.BossRoomEntryRoom = true;
                                    }
                                    EastAvailable = false;
                                    break;
                                }
                                if (randNumber == 2 && SouthAvailable == true && AmountOfRoomsGenerated + 1 <= floorInfo.RoomsInFloor1)
                                {
                                    randomRoom.Nodes.Add(new GenerationNode(ExitDirections.Down));
                                    AmountOfRoomsGenerated++;
                                    if (AmountOfRoomsGenerated == floorInfo.RoomsInFloor1)
                                    {
                                        randomRoom.BossRoomEntryRoom = true;
                                    }
                                    SouthAvailable = false;
                                    break;
                                }
                                if (randNumber == 3 && WestAvailable == true && AmountOfRoomsGenerated + 1 <= floorInfo.RoomsInFloor1)
                                {
                                    randomRoom.Nodes.Add(new GenerationNode(ExitDirections.Left));
                                    AmountOfRoomsGenerated++;
                                    if (AmountOfRoomsGenerated == floorInfo.RoomsInFloor1)
                                    {
                                        randomRoom.BossRoomEntryRoom = true;
                                    }
                                    WestAvailable = false;
                                    break;
                                }
                            }
                        }
                    }
                    generationRooms[randomRoom.x, randomRoom.y] = randomRoom;
                    
                    //Debug.Log("Actually Generated: " + ActuallyGenerated);
                    bool up = true;
                    bool right = true;
                    bool down = true;
                    bool left = true;
                    //Debug.Log("Before Node shit, roomID: " + randomRoom.ID + ", NodeCount: " + randomRoom.Nodes.Count);
                    for (int i = 0; i < randomRoom.Nodes.Count; i++) {  
                        //Debug.Log("NodeDingOfzo, roomID: " + randomRoom.ID);
                        x = randomRoom.x;
                        y = randomRoom.y;
                        if (randomRoom.Nodes[i].exitDirection == ExitDirections.Up && up && generationRooms[x,y+1] == null)
                        {
                            up = false;
                            y++;
                            GenerateRoom(DirectionsArray);
                        }
                        if (randomRoom.Nodes[i].exitDirection == ExitDirections.Right && right && generationRooms[x + 1, y] == null)
                        {
                            right = false;
                            x++;
                            GenerateRoom(DirectionsArray);
                        }
                        if (randomRoom.Nodes[i].exitDirection == ExitDirections.Down && down && generationRooms[x, y - 1] == null)
                        {
                            down = false;
                            y--;
                            GenerateRoom(DirectionsArray);
                        }
                        if (randomRoom.Nodes[i].exitDirection == ExitDirections.Left && left && generationRooms[x - 1, y] == null)
                        {
                            left = false;
                            x--;
                            GenerateRoom(DirectionsArray);
                        }

                    }
                }
            }
        }
    }

    public void IncrementXY(int value)
    {   
        if (value == 0)
        {
            y += 1;
            return;
        }
        if (value == 1)
        {
            x += 1;
            return;
        }
        if (value == 2)
        {
            y -= 1;
            return;
        }
        if (value == 3)
        {
            x -= 1;
            return;
        }

        Debug.Log("Fatal error with directions, value is <0 or >3. Value: " + value.ToString());
        return;
    }

    public bool CheckAvailability()
    {
        for (int i = 0; i < 30; i++)
        {
            DirectionValue = Random.Range(0, 4);
            if (DirectionValue == 0)
            {
                if (y + 1 < floorInfo.FloorWidthHeight)
                {
                    if (generationRooms[x, y + 1] == null)
                    {
                        return true;
                    }
                }

            }
            if (DirectionValue == 1)
            {
                if (x + 1 < floorInfo.FloorWidthHeight)
                {
                    if (generationRooms[x + 1, y] == null)
                    {
                        return true;
                    }
                }
            }
            if (DirectionValue == 2)
            {
                if (y - 1 > -1)
                {
                    if (generationRooms[x, y - 1] == null)
                    {
                        return true; 
                    }
                }
            }
            if (DirectionValue == 3)
            {
                if (x - 1 > -1)
                {
                    if (generationRooms[x - 1, y] == null)
                    {
                        return true;
                    }
                }
            }
        }
        Debug.Log("No available direction after 30 tries.");
        return false;
    }
}
