using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    [SerializeField]
 public   float movementSpeed;
    [SerializeField]
  public  Vector2 direction = Vector2.right;

    [SerializeField]
    Bullet bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }
}
