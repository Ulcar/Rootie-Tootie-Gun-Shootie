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
    RaycastHit2D[] TopHits;
    private void Start()
    {
        movement = GetComponentInParent<Movement>();
        col = GetComponent<Collider2D>();
    }


    private void FixedUpdate()
    {



           RaycastHit2D bottomHit = Physics2D.Linecast(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.down + col.bounds.extents.x * Vector3.right / 2, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("PlayerWall") | 1 << LayerMask.NameToLayer("Hole"));
           Debug.DrawLine(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.down + col.bounds.extents.x * Vector3.right / 2);

           RaycastHit2D bottomHit2 = Physics2D.Linecast(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.down + col.bounds.extents.x * Vector3.left / 2, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("PlayerWall") | 1 << LayerMask.NameToLayer("Hole"));
           Debug.DrawLine(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.down + col.bounds.extents.x * Vector3.left / 2);

           RaycastHit2D[] LeftHit = Physics2D.LinecastAll(col.bounds.center, col.bounds.center + col.bounds.extents.x * Vector3.left , 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("PlayerWall") | 1 << LayerMask.NameToLayer("Hole"));
           Debug.DrawLine(col.bounds.center, col.bounds.center + col.bounds.extents.x * Vector3.left);

           RaycastHit2D[] RightHit = Physics2D.LinecastAll(col.bounds.center, col.bounds.center + col.bounds.extents.x * Vector3.right, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("PlayerWall") | 1 << LayerMask.NameToLayer("Hole"));
           Debug.DrawLine(col.bounds.center, col.bounds.center + col.bounds.extents.x * Vector3.right);

        List<RaycastHit2D> TopHit = new List<RaycastHit2D>();
      TopHit.AddRange(Physics2D.LinecastAll(col.bounds.center, (col.bounds.center + col.bounds.extents.y * Vector3.up) + col.bounds.extents.x * Vector3.left / 2, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("PlayerWall") | 1 << LayerMask.NameToLayer("Hole")));
           Debug.DrawLine(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.up + col.bounds.extents.x * Vector3.left / 2);

           TopHit.AddRange(Physics2D.LinecastAll(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.up + col.bounds.extents.x * Vector3.right / 2, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("PlayerWall") | 1 << LayerMask.NameToLayer("Hole")));
           Debug.DrawLine(col.bounds.center, col.bounds.center + col.bounds.extents.y * Vector3.up + col.bounds.extents.x * Vector3.right / 2);
        

        if (LeftHit.Length > 0)
        {
            movement.LeftHit = true;
            foreach (RaycastHit2D hit in LeftHit)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("PlayerWall"))
                {
                    if (!hit.collider.GetComponent<RoomTile>().ignore)
                    {
                        movement.LeftHit = true;
                    }
                    else
                    {
                        movement.LeftHit = false;
                    }
                    break;
                }
            }
            
        }

        else
        {
            movement.LeftHit = false;
        }

           if (bottomHit)
           {
               movement.DownHit = true;
                if (bottomHit.collider.gameObject.layer == LayerMask.NameToLayer("PlayerWall"))
                {
                    if (!bottomHit.collider.GetComponent<RoomTile>().ignore)
                    {
                        movement.DownHit= true;
                    }
                    else
                    {
                        movement.DownHit = false;
                    }
                }
        }

           else
           {
               movement.DownHit = false;
           }



           if (RightHit.Length > 0)
           {
               movement.RightHit = true;
            foreach (RaycastHit2D hit in LeftHit)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("PlayerWall"))
                {
                    if (!hit.collider.GetComponent<RoomTile>().ignore)
                    {
                        movement.RightHit = true;
                    }
                    else
                    {
                        movement.RightHit = false;
                    }
                    break;
                }
            }
        }

           else
           {
               movement.RightHit = false;

           }

           if (TopHit.Count > 0)
           {
            movement.UpHit = true;
            foreach (RaycastHit2D hit in TopHit)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("PlayerWall"))
                {
                    if (!hit.collider.GetComponent<RoomTile>().ignore)
                    {
                        movement.UpHit = true;
                    }
                    else
                    {
                        movement.UpHit = false;
                    }
                    break;
                }
            }
        }

           else
           {
               movement.UpHit = false;
           }

           //hit a blind spot


    
       
    }

}
