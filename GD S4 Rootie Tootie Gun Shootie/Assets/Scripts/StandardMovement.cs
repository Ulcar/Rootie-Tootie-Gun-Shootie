using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMovement : Movement
{
    CharacterCollision collision;
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
        collision = GetComponent<CharacterCollision>();
        baseSpeed = movementSpeed;
    }

    // Update is called once per frame
   override protected void Update()
    {
        if (movementSpeed > baseSpeed)
        {
            if (currentTime > invulTime)
            {
                collision.SetInvincible(false, 1);
            }
            movementSpeed -= 16 * Time.deltaTime;
            if (movementSpeed < baseSpeed)
            {
                movementSpeed = baseSpeed;
            }
        }
        currentTime += Time.deltaTime;

     
        base.Update();
      
     
    }

    public void Dash()
    {
        if (currentTime > dashCooldown)
        {
            movementSpeed = dashSpeed;
            currentTime = 0;
            collision.SetInvincible(true, 1);
        }
    }
}
