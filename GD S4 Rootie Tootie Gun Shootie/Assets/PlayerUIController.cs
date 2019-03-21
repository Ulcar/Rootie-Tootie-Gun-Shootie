using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    Image health;
    [SerializeField]
    float maxHealth;

    [SerializeField]
    Player player;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.health.fillAmount = (player.healthManager.health / maxHealth);
    }
}
