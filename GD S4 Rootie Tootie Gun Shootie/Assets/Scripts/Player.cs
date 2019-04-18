using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Player : MonoBehaviour, IDamageable
{
    public Material material;
    bool InvincibilityCoRoutineStarted = false;
    public PlayerInfo playerInfo;
    public GameObject weapon;
    public Movement movement;
    public float movementSpeed;
    public HealthManager healthManager;
    public bool test = true;
    public Transform DebugLocation;
    [SerializeField]
    public SpriteRenderer renderer;
    public GameObject WeaponReadyForPickup;
    public float PickupRange;
    public float tossSpeed;
    public BoxCollider2D BoundBox;
    public Animator ani;

    public int Coins;

    [SerializeField]
    float invulTimeOnHit = 5;

    float currentTime;

    [SerializeField]
    public SpriteRenderer weaponSprite;

    [SerializeField]
    CharacterCollision collision;

    public UnityEvent OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        GetComponent<PlayerUIController>().UpdateCoins();
        movementSpeed = playerInfo.movementSpeed;
        weapon.GetComponent<Weapon>().SetHolder(this);
        healthManager = new HealthManager(playerInfo.maxHealth, playerInfo.maxShield, playerInfo.maxHealth, playerInfo.maxShield);
        healthManager.OnDeath.AddListener(OnDeathEvent);
        healthManager.UpdateHealthUI(healthManager.health);
        
    }

    void OnDeathEvent()
    {
        OnDeath.Invoke();
    }
    
    // Update is called once per frame
    void Update()
    {

        if (currentTime < invulTimeOnHit)
        {
            currentTime += Time.deltaTime;
            if (currentTime > invulTimeOnHit)
            {
                collision.SetInvincible(false, 0);
            }
        }
        CheckForWeaponPickups();
        //weapon.transform.localPosition = new Vector3(-0.06200001f, 0.163f, -1);

        if ((GetComponent<StandardMovement>().movementVector.x != 0 && ani.GetCurrentAnimatorStateInfo(0).IsName("Idle")) || (GetComponent<StandardMovement>().movementVector.y != 0 && ani.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        {
            ani.SetTrigger("Walk");
        }
        else if (GetComponent<StandardMovement>().movementVector.x == 0 && ani.GetCurrentAnimatorStateInfo(0).IsName("PlayerWalking") && (GetComponent<StandardMovement>().movementVector.y == 0))
        {
            ani.SetTrigger("StandStill");
        }
        weapon.transform.localPosition = new Vector3(0, -0.04f, -0.01f);
    }

    public void PickupWeapon()
    {
        if (WeaponReadyForPickup != null)
        {
            BuyableItem item = WeaponReadyForPickup.GetComponent<BuyableItem>();
            if (item != null)
            {
                if (!item.ForSale)
                {
                    if (GetComponent<PlayerInventoryScript>().Secondary != null)
                    {
                        if (TossWeapon())
                        {
                            Debug.Log("Tossweapon() true");
                            GameObject NewGun = Instantiate(WeaponReadyForPickup);
                            NewGun.transform.SetParent(transform);
                            NewGun.GetComponent<SpriteRenderer>().sprite = NewGun.GetComponent<Weapon>().WeaponWithHands;
                            GetComponent<PlayerInventoryScript>().Primary = NewGun;
                            weaponSprite = NewGun.GetComponent<SpriteRenderer>();
                            NewGun.GetComponent<Weapon>().SetHolder(this);
                            Destroy(WeaponReadyForPickup);
                            WeaponReadyForPickup = null;
                            weapon = NewGun;
                            weapon.transform.localPosition = new Vector3(-0.06200001f, 0.163f, -1);
                            weapon.GetComponent<Tossable>().CurrentlyBeingTossed = false;
                            weapon.GetComponent<Tossable>().tossSpeed = 0;
                            weapon.GetComponent<Tossable>().FloorY = 0;
                            GetComponent<PlayerUIController>().UpdateWeapons();
                        }
                    }
                    else
                    {
                        GetComponent<PlayerInventoryScript>().Secondary = WeaponReadyForPickup;
                        //Destroy(WeaponReadyForPickup);
                        WeaponReadyForPickup.GetComponent<SpriteRenderer>().sprite = WeaponReadyForPickup.GetComponent<Weapon>().WeaponWithHands;
                        WeaponReadyForPickup.transform.position = new Vector3(10000, 10000, 0); //Help
                        WeaponReadyForPickup.transform.SetParent(transform);
                        WeaponReadyForPickup = null;
                        weapon.GetComponent<Weapon>().SetHolder(this);

                        GetComponent<PlayerUIController>().UpdateWeapons();
                    }
                }
                else
                {
                    if (item.Price <= Coins)
                    {
                        GameManager.instance.RoomPlayerIsIn.itemForSale.Remove(WeaponReadyForPickup);
                        WeaponReadyForPickup.transform.parent.GetComponent<ShopTileScript>().SellItem();
                        WeaponReadyForPickup.GetComponent<BuyableItem>().ForSale = false;
                        Coins -= item.Price;
                        GameManager.instance.player.GetComponent<PlayerUIController>().UpdateCoins();
                        if (GetComponent<PlayerInventoryScript>().Secondary != null)
                        {
                            if (TossWeapon())
                            {
                                Debug.Log("Tossweapon() true");
                                GameObject NewGun = Instantiate(WeaponReadyForPickup);
                                NewGun.GetComponent<SpriteRenderer>().sprite = NewGun.GetComponent<Weapon>().WeaponWithHands;
                                NewGun.transform.SetParent(transform);
                                GetComponent<PlayerInventoryScript>().Primary = NewGun;
                                weaponSprite = NewGun.GetComponent<SpriteRenderer>();
                                NewGun.GetComponent<Weapon>().SetHolder(this);
                                Destroy(WeaponReadyForPickup);
                                WeaponReadyForPickup = null;
                                weapon = NewGun;
                                weapon.transform.localPosition = new Vector3(-0.06200001f, 0.163f, -1);
                                weapon.GetComponent<Tossable>().CurrentlyBeingTossed = false;
                                weapon.GetComponent<Tossable>().tossSpeed = 0;
                                weapon.GetComponent<Tossable>().FloorY = 0;
                                GetComponent<PlayerUIController>().UpdateWeapons();
                            }
                        }
                        else
                        {
                            GetComponent<PlayerInventoryScript>().Secondary = WeaponReadyForPickup;
                            WeaponReadyForPickup.GetComponent<SpriteRenderer>().sprite = WeaponReadyForPickup.GetComponent<Weapon>().WeaponWithHands;
                            //Destroy(WeaponReadyForPickup);
                            WeaponReadyForPickup.transform.position = new Vector3(10000, 10000, 0); //Help
                            WeaponReadyForPickup.transform.SetParent(transform);
                            WeaponReadyForPickup = null;
                            weapon.GetComponent<Weapon>().SetHolder(this);

                            GetComponent<PlayerUIController>().UpdateWeapons();
                        }
                    }
                    else
                    {
                        item.CashFlashing();
                    }
                }
            }
            else
            {
                if (GetComponent<PlayerInventoryScript>().Secondary != null)
                {
                    if (TossWeapon())
                    {
                        Debug.Log("Tossweapon() true");
                        GameObject NewGun = Instantiate(WeaponReadyForPickup);
                        NewGun.GetComponent<SpriteRenderer>().sprite = NewGun.GetComponent<Weapon>().WeaponWithHands;
                        NewGun.transform.SetParent(transform);
                        GetComponent<PlayerInventoryScript>().Primary = NewGun;
                        weaponSprite = NewGun.GetComponent<SpriteRenderer>();
                        NewGun.GetComponent<Weapon>().SetHolder(this);
                        Destroy(WeaponReadyForPickup);
                        WeaponReadyForPickup = null;
                        weapon = NewGun;
                        weapon.transform.localPosition = new Vector3(-0.06200001f, 0.163f, -1);
                        weapon.GetComponent<Tossable>().CurrentlyBeingTossed = false;
                        weapon.GetComponent<Tossable>().tossSpeed = 0;
                        weapon.GetComponent<Tossable>().FloorY = 0;
                        GetComponent<PlayerUIController>().UpdateWeapons();
                    }
                }
                else
                {
                    GetComponent<PlayerInventoryScript>().Secondary = WeaponReadyForPickup;
                    WeaponReadyForPickup.GetComponent<SpriteRenderer>().sprite = WeaponReadyForPickup.GetComponent<Weapon>().WeaponWithHands;
                    //Destroy(WeaponReadyForPickup);
                    WeaponReadyForPickup.transform.position = new Vector3(10000, 10000, 0); //Help
                    WeaponReadyForPickup.transform.SetParent(transform);
                    WeaponReadyForPickup = null;
                    weapon.GetComponent<Weapon>().SetHolder(this);

                    GetComponent<PlayerUIController>().UpdateWeapons();
                }
            }
        }
    }

    bool TossWeapon()
    {
        if (weapon.GetComponent<Tossable>().CurrentlyBeingTossed == false)
        {
            GameObject WeaponToToss = Instantiate(weapon);
            WeaponToToss.name = WeaponToToss.GetComponent<Weapon>().WeaponName;
            Destroy(weapon);
            WeaponToToss.GetComponent<Weapon>().SetHolder(null);
            WeaponToToss.transform.SetParent(GameManager.instance.RoomPlayerIsIn.transform);
            WeaponToToss.transform.position = transform.position;
            WeaponToToss.GetComponent<Tossable>().tossSpeed = tossSpeed;
            WeaponToToss.GetComponent<Tossable>().FloorY = transform.position.y - BoundBox.bounds.size.y / 2;
            WeaponToToss.GetComponent<SpriteRenderer>().sprite = WeaponToToss.GetComponent<Weapon>().WeaponWithoutHands;
            if (GetComponent<SpriteRenderer>().flipX)
            {
                WeaponToToss.GetComponent<Tossable>().TossItem(true);
            }
            else
            {
                WeaponToToss.GetComponent<Tossable>().TossItem(false);
            }
            return true;
        }
        return false;
    }

  public void RotatePlayer(float angle)
    {
     //   Debug.Log("Angle: " + angle);
        if (angle > 90)
        {
            angle -= 360f;
        }
        if (angle < -90)
        {
            angle += 360f;
        }
        if (angle >= 90 || angle <= -90)
        {
            // transform.rotation = Quaternion.Euler(0f, 180f, transform.rotation.y);
            renderer.flipX = true;
            weapon.transform.position = new Vector3(weapon.transform.position.x, weapon.transform.position.y, -0.01f);
        }
        else
        {
            // transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.y);
            renderer.flipX = false;
            
            weapon.transform.position = new Vector3(weapon.transform.position.x, weapon.transform.position.y, -0.01f);
            
        }
    }

  public void RotateWeapon(float Angle)
    {
      // Debug.Log("Angle voor RotateWeapon: " + Angle);
        if (Angle >= 90 || Angle <= -90)
        {
                weapon.transform.localRotation = Quaternion.Euler(0, 0, (Angle));
            weaponSprite.flipY = true;
          //  EquipedWeapon.transform.localRotation = Quaternion.Euler(0f, 0f, (Angle));
        }
        else
        {
            weaponSprite.flipY = false;
            weapon.transform.localRotation = Quaternion.Euler(0, 0, (Angle));
        }
    }


    public void TakeDamage(int damage, Vector2 direction, float speed)
    {
        Debug.Log("Damage taken");
        healthManager.TakeDamage(1);
        healthManager.UpdateHealthUI(healthManager.health);
        HurtOverlay();
        if (collision != null)
        {
            collision.SetInvincible(true, 0);
            currentTime = 0;
        }

    }

    IEnumerator HurtOverlayCoroutine()
    {
        for (float i = -5f; i <= 5; i += 0.35f)
        {
            Color c = GameManager.instance.HurtOverlay.color;
            c.a = ((-4 * (Mathf.Pow(i, 2))) + 100) / 100;
            Debug.Log("C.a: " + c.a);
            GameManager.instance.HurtOverlay.color = c;
            yield return null;
        }
    }

    public void HurtOverlay()
    {
        StartCoroutine("HurtOverlayCoroutine");
    }

    public void CheckForWeaponPickups()
    {
        GameObject temp = null;
        float distance = 0;
        if (GameManager.instance.RoomPlayerIsIn != null)
        {
            if (GameManager.instance.RoomPlayerIsIn.ShopRoom)
            {
                for (int i = 0; i < GameManager.instance.RoomPlayerIsIn.itemForSale.Count; i++)
                {
                    if (temp == null)
                    {
                        //a = √b^2+c^2
                        float b = transform.position.y - GameManager.instance.RoomPlayerIsIn.itemForSale[i].transform.position.y; //y axis
                        float c = transform.position.x - GameManager.instance.RoomPlayerIsIn.itemForSale[i].transform.position.x; //x axis
                        float result = Mathf.Sqrt(Mathf.Pow(b, 2) + Mathf.Pow(c, 2));
                        if (result <= PickupRange && result >= -PickupRange)
                        {
                            temp = GameManager.instance.RoomPlayerIsIn.itemForSale[i].gameObject;
                            distance = result;
                        }
                    }
                    else
                    {
                        float b = transform.position.y - GameManager.instance.RoomPlayerIsIn.itemForSale[i].transform.position.y; //y axis
                        float c = transform.position.x - GameManager.instance.RoomPlayerIsIn.itemForSale[i].transform.position.x; //x axis
                        float result = Mathf.Sqrt(Mathf.Pow(b, 2) + Mathf.Pow(c, 2));
                        if (result < distance)
                        {
                            distance = result;
                            temp = GameManager.instance.RoomPlayerIsIn.itemForSale[i].gameObject;
                        }
                    }
                }
            }
            for (int i = 0; i < GameManager.instance.RoomPlayerIsIn.transform.childCount; i++)
            {
                if (GameManager.instance.RoomPlayerIsIn.transform.GetChild(i).GetComponent<Weapon>() != null)
                {
                    if (temp == null)
                    {
                        //a = √b^2+c^2
                        float b = transform.position.y - GameManager.instance.RoomPlayerIsIn.transform.GetChild(i).transform.position.y; //y axis
                        float c = transform.position.x - GameManager.instance.RoomPlayerIsIn.transform.GetChild(i).transform.position.x; //x axis
                        float result = Mathf.Sqrt(Mathf.Pow(b, 2) + Mathf.Pow(c, 2));
                        if (result <= PickupRange && result >= -PickupRange)
                        {
                            temp = GameManager.instance.RoomPlayerIsIn.transform.GetChild(i).gameObject;
                            distance = result;
                        }
                    }
                    else
                    {
                        float b = transform.position.y - GameManager.instance.RoomPlayerIsIn.transform.GetChild(i).transform.position.y; //y axis
                        float c = transform.position.x - GameManager.instance.RoomPlayerIsIn.transform.GetChild(i).transform.position.x; //x axis
                        float result = Mathf.Sqrt(Mathf.Pow(b, 2) + Mathf.Pow(c, 2));
                        if (result < distance)
                        {
                            distance = result;
                            temp = GameManager.instance.RoomPlayerIsIn.transform.GetChild(i).gameObject;
                        }
                    }
                }
            }
        }
        if (temp != null)
        {
            Debug.Log("Weapon ready to pickup - debug");
            WeaponReadyForPickup = temp;
        }
    }

    public void ShootGun(int index)
    {
        weapon.GetComponent<Weapon>().Attack(index);
    }

 

    public void StartInvincibility()
    {
        if (ani.GetCurrentAnimatorStateInfo(2).IsName("Invincible") == false) { 
        ani.SetTrigger("OnInvincibility");
             }
    }
 
}
