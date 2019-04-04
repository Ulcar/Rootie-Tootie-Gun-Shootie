using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropScript : MonoBehaviour
{
    public int CoinValue;
    public float gravity;
    public float yVelocity;
    public float xVelocity;
    public float FloorY;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CoinDropping");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, GameManager.instance.player.transform.position) <= 2)
        {
            float step = 0.035f / Vector2.Distance(transform.position, GameManager.instance.player.transform.position);
            Vector3 actualTarget = new Vector3(GameManager.instance.player.transform.position.x, GameManager.instance.player.transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, actualTarget, step);
            if (Vector2.Distance(transform.position, GameManager.instance.player.transform.position) < 0.25)
            {
                GameManager.instance.player.Coins += CoinValue;
                GameManager.instance.player.GetComponent<PlayerUIController>().UpdateCoins();
                Destroy(gameObject);
            }
        }
    }

    public IEnumerator CoinDropping()
    {
        for (float i = transform.position.y; i >= FloorY-0.001;)
        {

                yVelocity += gravity;
                gravity = gravity - 0.01f;
            transform.position = new Vector3(transform.position.x + xVelocity, transform.position.y + yVelocity, transform.position.z);
            i = transform.position.y;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, FloorY, transform.position.z);
    }
}
