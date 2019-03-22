using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

  public  class CharacterPushScript:MonoBehaviour
    {

    Movement movement;
    Collider2D col;
    private void Start()
    {
        movement = GetComponentInParent<Movement>();
        col = GetComponent<Collider2D>();
    }


    private void FixedUpdate()
    {



           RaycastHit2D bottomHit = Physics2D.Linecast(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.down + col.bounds.extents.x * Vector3.right / 2, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("PlayerWall"));
           Debug.DrawLine(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.down + col.bounds.extents.x * Vector3.right / 2);

           RaycastHit2D bottomHit2 = Physics2D.Linecast(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.down + col.bounds.extents.x * Vector3.left / 2, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("PlayerWall"));
           Debug.DrawLine(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.down + col.bounds.extents.x * Vector3.left / 2);

           RaycastHit2D LeftHit = Physics2D.Linecast(col.bounds.center, col.bounds.center + col.bounds.extents.x * Vector3.left , 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("PlayerWall"));
           Debug.DrawLine(col.bounds.center, col.bounds.center + col.bounds.extents.x * Vector3.left);

           RaycastHit2D RightHit = Physics2D.Linecast(col.bounds.center, col.bounds.center + col.bounds.extents.x * Vector3.right, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("PlayerWall"));
           Debug.DrawLine(col.bounds.center, col.bounds.center + col.bounds.extents.x * Vector3.right);

           RaycastHit2D TopHit = Physics2D.Linecast(col.bounds.center, (col.bounds.center + col.bounds.extents.y * Vector3.up) + col.bounds.extents.x * Vector3.left / 2, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("PlayerWall"));
           Debug.DrawLine(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.up + col.bounds.extents.x * Vector3.left / 2);

           RaycastHit2D TopHit2 = Physics2D.Linecast(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.up + col.bounds.extents.x * Vector3.right / 2, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("PlayerWall"));
           Debug.DrawLine(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.up + col.bounds.extents.x * Vector3.right / 2);


           if (LeftHit)
           {
               movement.LeftHit = true;
           }

           else
           {
               movement.LeftHit = false;
           }

           if (bottomHit)
           {
               movement.DownHit = true;
           }

           else
           {
               movement.DownHit = false;
           }



           if (RightHit)
           {
               movement.RightHit = true;
           }

           else
           {
               movement.RightHit = false;

           }

           if (TopHit || TopHit2)
           {
               movement.UpHit = true;
           }

           else
           {
               movement.UpHit = false;
           }

           //hit a blind spot
        if(!bottomHit && !bottomHit2 && !TopHit && !TopHit2 && !LeftHit && !RightHit)
        {
           
            RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center, col.bounds.extents, 0, Vector2.zero, 1, 1 << LayerMask.NameToLayer("Wall"));
            if (hit)
            {
                Debug.Log("Blind spot");
                movement.ColisionDirection = hit.point - (Vector2)col.bounds.center;
            }
            
        }

    
       
    }

}
