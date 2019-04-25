using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMovement : Movement
{
    CharacterCollision collision;
    [SerializeField]
    public float dashCooldown;
    public float currentTime;
    float baseSpeed;
    [SerializeField]
    float dashSpeed;

    [SerializeField]
    float invulTime = 0.167f;

    [SerializeField]
    private Player player;

    [SerializeField]
    NewCharacterPushScript aa;
    void Start()
    {
        collision = GetComponent<CharacterCollision>();
        baseSpeed = movementSpeed;
    }

    // Update is called once per frame
   override protected void Update()
    {
     
     
        base.Update();
      
     
    }

    public override void Move(Vector2 movementVector)
    {

        if (aa != null)
        {
            Vector2 hitpoint = Vector2.zero;
            if (!aa.SimulateNextPosition(movementVector * movementSpeed, out hitpoint))
            {
                base.Move(movementVector);
            }
            else
            {
                base.Move(-hitpoint);
            }
        }
        else
        {
            base.Move(movementVector);
        }
    }

    override protected void FixedUpdate()
    {
        if (movementSpeed > baseSpeed)
        {
            if (currentTime > invulTime)
            {
                collision.SetInvincible(false, 1);
            }
            movementSpeed -= 10 * Time.deltaTime;
            if (movementSpeed < baseSpeed)
            {
                movementSpeed = baseSpeed;
            }
        }
        currentTime += Time.deltaTime;
        base.FixedUpdate();
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
