using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

  public  class CharacterPushScript:MonoBehaviour
    {

    Movement movement;
    private void Start()
    {
        movement = GetComponentInParent<Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ContactPoint2D[] contacts = { };
            collision.GetContacts(contacts);
            Vector3 dir = contacts[0].point;
            dir = -dir.normalized;
            movement.MoveAdditive(dir);
        }
       



    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        movement.Move(Vector2.zero);
    }
}
