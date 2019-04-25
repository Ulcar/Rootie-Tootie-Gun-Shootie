using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    
  public  class NewCharacterPushScript:MonoBehaviour
    {
    [SerializeField]
    Vector2 BoxSize;
    Vector2 previousPosition;


    public bool SimulateNextPosition(Vector2 velocity, out Vector2 hitpoint)
    {
        //convert to movement over one frame
        velocity *= Time.fixedDeltaTime;
        Debug.DrawLine(transform.position, transform.position + (Vector3)velocity);
        float distane = Mathf.Sqrt(Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.y, 2));
        RaycastHit2D h = Physics2D.BoxCast(transform.position, BoxSize, Vector2.Angle(Vector2.zero, velocity.normalized), velocity.normalized, distane,  1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("PlayerWall"));
        if(h)
        {
            previousPosition = transform.position;
            hitpoint = h.point - (Vector2)transform.position;
            Vector2 distanceToTravel = h.point - (Vector2)transform.position - velocity.normalized * 2;
            return true;
        }
        else
        {
            hitpoint = Vector2.zero;
            return false;
        }
    }
}
