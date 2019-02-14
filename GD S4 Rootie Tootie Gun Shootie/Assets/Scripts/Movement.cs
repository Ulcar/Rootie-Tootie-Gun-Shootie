using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform ParentTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move(Vector2 Direction, float movementSpeed)
    {
        Vector3.MoveTowards(ParentTransform.position, Direction, movementSpeed * Time.deltaTime);
    }

    //Notes
    //Beide movement voor de player en AI gaan joystick inputs gebruiken (Oftewel een richting en snelheid (Snelheid is trapgewijs voor de controller))
    //Childs van Movement kunnen dingen zoals flying en dashes aanmaken, Movement zelf neemt de input van de (mogelijk virtuele, voor de AI) joystick om de movement te bepalen.

}
