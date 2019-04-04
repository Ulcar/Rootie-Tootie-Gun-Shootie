using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;
using UnityEngine.Events;
public class Enemy : MonoBehaviour, IDamageable
{
    public SpriteRenderer Renderer;
    [SerializeField]
    EnemyStats stats;
    [SerializeField]
    HealthManager manager;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    Vector2 impact = Vector2.zero;
    [SerializeField]
    AILerp AI;

    [SerializeField]
    float Weight;

    public float CoinWeight;

    public int CoinBounty;
    public int HealthBounty;

    [SerializeField]
   public Transform target;

    public EnemyDeathEvent OnDeath = new EnemyDeathEvent();
   

    void Start()
    {
        manager = new HealthManager(stats.MaxHealth, 0, stats.MaxHealth, 0);
        manager.OnDeath.AddListener(OnDeathEvent);
        manager.OnDeath.AddListener(SpewCoinsAndHearths);
    }

    public void Init(EnemyStats stats)
    {
        this.stats = stats;
        manager = new HealthManager(stats.MaxHealth, 0, stats.MaxHealth, 0);
        GetComponent<Weapon>().stats = stats.weapons[0];
        manager.OnDeath.AddListener(OnDeathEvent);
        //Code to generate enemy from Enemystats here
    }

    void OnDeathEvent()
    {
        OnDeath.Invoke(this);
        Destroy(gameObject.transform.parent.gameObject);
    }

    void SpewCoinsAndHearths()
    {
        if (CoinBounty > 0) {
            int numberOfCoinsToSpawn = 0;
            for (int i = CoinBounty; i > 0;)
            {
                if (i- 50 > 0)
                {
                    numberOfCoinsToSpawn++;
                    i -= 50;
                }
                else if (i - 10 > 0)
                {
                    numberOfCoinsToSpawn++;
                    i -= 10;
                }
                else if (i - 5 > 0)
                {
                    numberOfCoinsToSpawn++;
                    i -= 5;
                }
                else
                {
                    numberOfCoinsToSpawn++;
                    i -= 1;
                }
            }
            int count = 0;
            float angle = (90 / numberOfCoinsToSpawn) / 2;
            while (CoinBounty > 0)
            {
                CoinDropScript coin = Instantiate(GameManager.instance.Coin);
                coin.transform.SetParent(GameManager.instance.RoomPlayerIsIn.transform);
                coin.transform.position = transform.position;
                coin.yVelocity = UnityEngine.Random.Range(0.001f, 0.003f);
          
                    coin.xVelocity = .001f * (Mathf.Pow(( 4*(count - Mathf.RoundToInt(numberOfCoinsToSpawn/2)) ),2) + 0.001f);
  
                coin.FloorY = transform.position.y;
                //coin.transform.rotation = new Quaternion(0,(angle*count)-45,0,0);
                coin.transform.rotation = Quaternion.Euler(0, 0, (angle * count) - 45);
                count++;
                coin.gravity = 0.03f;
                if (CoinBounty - 50 > 0)
                {
                    CoinBounty -= 50;
                    coin.GetComponent<SpriteRenderer>().sprite = GameManager.instance.PlatinumCoin;
                    coin.CoinValue = 50;

                }
                else if (CoinBounty - 10 > 0)
                {
                    CoinBounty -= 10;
                    coin.GetComponent<SpriteRenderer>().sprite = GameManager.instance.GoldCoin;
                    coin.CoinValue = 10;
                }
                else if (CoinBounty - 5 > 0)
                {
                    coin.CoinValue = 5;
                    CoinBounty -= 5;
                    coin.GetComponent<SpriteRenderer>().sprite = GameManager.instance.SilverCoin;
                }
                else
                {
                    coin.CoinValue = 1;
                    CoinBounty -= 1;
                    coin.GetComponent<SpriteRenderer>().sprite = GameManager.instance.BronzeCoin;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void FixedUpdate()
    {
        if ((impact.x > 0.001 || impact.y > 0.001))
        {
            rb.velocity = impact;
            impact = Vector3.Lerp(impact, Vector3.zero, 25 * Time.deltaTime);
        }

        else if (!AI.canMove)
        {
            rb.velocity = Vector3.zero;
            AI.SearchPath();
            AI.canMove = true;
            impact = Vector3.zero;
        }
    }


   public  void TakeDamage(int damage, Vector2 direction, float speed)
    {
        

        manager.TakeDamage(damage);
        StartCoroutine("RedHue");
        impact += direction * speed * Weight;
        if (AI != null)
        {
            AI.canMove = false;
        }
    }


    public IEnumerator RedHue()
    {
        for (int i = 0; i < 3; i++)
        {
            Color c = new Color(255, 0, 0);
            Renderer.color = c;
            yield return null;
        }
        Color a = new Color(255, 255, 255);
        Renderer.color = a;
    }

}

public class EnemyDeathEvent : UnityEvent<Enemy>
{

}
