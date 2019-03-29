using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthDropScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, GameManager.instance.player.transform.position) <= 3 && GameManager.instance.player.healthManager.health < GameManager.instance.player.healthManager.maxHealth)
        {
            float step = 0.035f / Vector2.Distance(transform.position, GameManager.instance.player.transform.position);
            Vector3 actualTarget = new Vector3(GameManager.instance.player.transform.position.x, GameManager.instance.player.transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, actualTarget, step);
            if (Vector2.Distance(transform.position, GameManager.instance.player.transform.position) < 0.1)
            {
                GameManager.instance.player.healthManager.GainHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
