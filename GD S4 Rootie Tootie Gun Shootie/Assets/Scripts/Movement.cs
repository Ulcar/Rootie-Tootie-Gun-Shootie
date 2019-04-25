using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{

    public Transform ParentTransform;
    public Rigidbody2D ParentRigidbody;
    public float movementSpeed = 2;
   public Vector2 movementVector;
    public Vector2 ColisionDirection = Vector2.zero;
    public bool DownHit, UpHit, LeftHit, RightHit;
    public Vector2 Velocity;
   
    void Start()
    {
        ParentTransform = transform;
    }
  protected virtual void Update()
    {

        //  ParentTransform.Translate(movementVector * movementSpeed * Time.deltaTime);
       
    }

    protected virtual void FixedUpdate()
    {
        Velocity = (movementVector) * movementSpeed;
        ParentRigidbody.velocity = Velocity;
    }

    public virtual void Move(Vector2 movementVector)
    {
        //ParentTransform.position = Vector3.MoveTowards(ParentTransform.position, new Vector3(x,y,0), movementSpeed * Time.deltaTime);
       if (DownHit)
        {
            movementVector = movementVector + new Vector2(0, 1);
        }
      else  if (UpHit)
        {
            movementVector = movementVector + new Vector2(0, -1);
        }

       if (LeftHit)
        {
            movementVector = movementVector + new Vector2(1, 0);
        }

     else   if (RightHit)
        {
            movementVector = movementVector + new Vector2(-1, 0);
        } 
        this.movementVector = movementVector;
       
    }

    public void MoveAdditive(Vector2 movementVector)
    {
        this.movementVector += movementVector;
        Debug.Log("Current Vector: " + this.movementVector);
    }


    //Notes
    //Beide movement voor de player en AI gaan joystick inputs gebruiken (Oftewel een richting en snelheid (Snelheid is trapgewijs voor de controller))
    //Childs van Movement kunnen dingen zoals flying en dashes aanmaken, Movement zelf neemt de input van de (mogelijk virtuele, voor de AI) joystick om de movement te bepalen.

}
