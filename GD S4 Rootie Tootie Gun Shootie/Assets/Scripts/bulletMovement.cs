using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    [SerializeField]
 public   float MovementSpeed;
    [SerializeField]
  public  Vector2 direction = Vector2.right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * MovementSpeed * Time.deltaTime);
    }
}
