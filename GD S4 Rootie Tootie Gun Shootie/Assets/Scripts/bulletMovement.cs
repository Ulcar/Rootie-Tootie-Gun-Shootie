using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    [SerializeField]
 public   float MovementSpeed;
    [SerializeField]
  public  Vector2 direction = Vector2.right;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
     body =   GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
     //   transform.Translate(direction * MovementSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Vector3 movement = (transform.rotation * direction * MovementSpeed * Time.deltaTime);
        body.MovePosition((transform.position + movement));
    }
}
