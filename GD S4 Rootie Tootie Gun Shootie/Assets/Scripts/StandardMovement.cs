using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMovement : Movement
{
    Collider2D col;
    [SerializeField]
    float dashCooldown;
    float currentTime;
    float baseSpeed;
    [SerializeField]
    float dashSpeed;

    [SerializeField]
    float invulTime = 0.167f;

    [SerializeField]
    private Player player;
    void Start()
    {
        col = GetComponent<Collider2D>();
        baseSpeed = movementSpeed;
    }

    // Update is called once per frame
   override protected void Update()
    {
        if (movementSpeed > baseSpeed)
        {
            movementSpeed -= 16 * Time.deltaTime;
            if (movementSpeed < baseSpeed)
            {
                movementSpeed = baseSpeed;
            }
        }
        currentTime += Time.deltaTime;

        if (currentTime > invulTime)
        {
            player.SetInvincible(false);
        }
        base.Update();
      
     
    }

    public void Dash()
    {
        if (currentTime > dashCooldown)
        {
            movementSpeed = dashSpeed;
            currentTime = 0;
            ///uhhhh fuck
            player.SetInvincible(true);
        }
    }
}
