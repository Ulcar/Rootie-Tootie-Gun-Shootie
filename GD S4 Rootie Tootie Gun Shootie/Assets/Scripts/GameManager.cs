using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public FloorInfo[] FloorInfos;
    public static GameManager instance;
    public int Floor = 0;
    public FloorHandler floorHandler;
    RoomGenerator roomGenerator;
    public Player player;
    public Camera MainCamera;
    public Image HealthImage;
    bool scan = false;
    public Image HurtOverlay;
    public Image AbilityBarFiller;
    public Image PrimaryWeapon;
    public Image SecondaryWeapon;
    public Room RoomPlayerIsIn;
    public HearthDropScript healthOrb;
    public CoinDropScript Coin;
    public Sprite BronzeCoin;
    public Sprite SilverCoin;
    public Sprite GoldCoin;
    public Sprite PlatinumCoin;
    public Text CoinsText;
    
    
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
            player.OnDeath.AddListener(OnDeath);
        }
        
    }

    void SetupPlayer()
    {
        
    }

    public void OnDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
