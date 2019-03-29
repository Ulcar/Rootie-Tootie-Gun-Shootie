using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropScript : MonoBehaviour
{
    public int CoinValue;
    public float gravity;
    public float yVelocity;
    public float xVelocity;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CoinDropping");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, GameManager.instance.player.transform.position) <= 3)
        {
            float step = 0.035f / Vector2.Distance(transform.position, GameManager.instance.player.transform.position);
            Vector3 actualTarget = new Vector3(GameManager.instance.player.transform.position.x, GameManager.instance.player.transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, actualTarget, step);
            if (Vector2.Distance(transform.position, GameManager.instance.player.transform.position) < 0.1)
            {
                GameManager.instance.player.Coins += CoinValue;
                Destroy(gameObject);
            }
        }
    }

    public IEnumerator CoinDropping(Transform StarterTrans, int FloorY)
    {
        for (float i = StarterTrans.position.y; i > FloorY; i += yVelocity)
        {
            yVelocity += gravity;
            gravity -= -0.02f;
            transform.position = new Vector3(transform.position.x + xVelocity, transform.position.y + yVelocity, transform.position.z);
            if (transform.position.y < FloorY)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
            yield return null;
        }
    }
}
