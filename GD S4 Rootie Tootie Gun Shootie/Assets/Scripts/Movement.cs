﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{

    public Transform ParentTransform;
    public float movementSpeed = 2;
   protected Vector2 movementVector;
   
    void Start()
    {
        ParentTransform = transform;
    }
  protected virtual void Update()
    {

            ParentTransform.Translate(movementVector * movementSpeed * Time.deltaTime);
    }

    public void Move(Vector2 movementVector)
    {
        //ParentTransform.position = Vector3.MoveTowards(ParentTransform.position, new Vector3(x,y,0), movementSpeed * Time.deltaTime);
        this.movementVector = movementVector;
    }

    public void MoveAdditive(Vector2 movementVector)
    {
        this.movementVector += movementVector;
    }


    //Notes
    //Beide movement voor de player en AI gaan joystick inputs gebruiken (Oftewel een richting en snelheid (Snelheid is trapgewijs voor de controller))
    //Childs van Movement kunnen dingen zoals flying en dashes aanmaken, Movement zelf neemt de input van de (mogelijk virtuele, voor de AI) joystick om de movement te bepalen.

}
