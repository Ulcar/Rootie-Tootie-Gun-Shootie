using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopTileScript : MonoBehaviour
{
    public TextMeshPro coinText;
    public SpriteRenderer coinSprite;
    public bool IsShopTile = false;
    public float debug;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<RoomTile>().ShopTile == false)
        {
            coinText.enabled = false;
            coinSprite.enabled = false;
        }
        else
        {
            IsShopTile = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Weapon>() != null)
                {
                    coinText.text = transform.GetChild(i).GetComponent<BuyableItem>().Price.ToString();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsShopTile)
        {
            debug = Vector2.Distance(transform.position, GameManager.instance.player.transform.position);
            if (debug <= 1.6f)
            {
                coinText.enabled = true;
                coinSprite.enabled = true;
            }
            else
            {
                coinText.enabled = false;
                coinSprite.enabled = false;
            }
        }
    }

    public void SellItem()
    {
        coinText.enabled = false;
        coinSprite.enabled = false;
        IsShopTile = false;
    }
}
