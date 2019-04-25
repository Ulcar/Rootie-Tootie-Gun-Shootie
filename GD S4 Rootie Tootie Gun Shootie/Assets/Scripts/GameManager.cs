using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum TransitionPhase
{
    LightToDark,
    DarkToLight
}
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
    public Image TransitionImage;

    public Button RetryButton;
    public Text RetryText;
    bool Death;

    
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        
    }
    void Start()
    {
        StartCoroutine("TransitionScreen", TransitionPhase.DarkToLight);

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
        StartCoroutine("TransitionScreen", TransitionPhase.LightToDark);
        Death = true;
    }

    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public IEnumerator TransitionScreen(TransitionPhase phase)
    {
        if (!Death)
        {
            switch (phase)
            {
                case TransitionPhase.DarkToLight:
                    {
                        for (float i = 1; i >= 0; i -= 0.04f)
                        {

                            Color c = new Color(1, 1, 1, i);
                            TransitionImage.color = c;

                            yield return new WaitForSeconds(0.002f);
                        }
                        break;
                    }
                case TransitionPhase.LightToDark:
                    {

                        for (float i = 0; i <= 1; i += 0.04f)
                        {
                            Color c = new Color(1, 1, 1, i);
                            TransitionImage.color = c;
                            RetryText.color = c;
                            if (i > 0.8)
                            {
                                RetryButton.interactable = true;
                                //Debug.Log("Enabled");
                            }
                            yield return new WaitForSeconds(0.002f);
                            
                        }
                        
                        break;
                    }
            }
        }
    }


}
