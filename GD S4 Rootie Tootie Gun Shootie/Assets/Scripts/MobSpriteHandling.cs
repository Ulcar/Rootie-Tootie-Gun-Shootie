using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class MobSpriteHandling : MonoBehaviour
{
    public Rigidbody2D rb;
    Animator ani;
    public AILerp lerp;
  
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Lerp.velocityVector: " + lerp.VelocityVector);
        transform.rotation = Quaternion.Euler(0,0,-transform.parent.rotation.z);
        if (lerp.VelocityVector > 0.001 && ani.GetCurrentAnimatorStateInfo(0).IsName("Standing Still"))
        {
            
            ani.SetTrigger("Moving");
        }
        else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Imp walking") )
        {
            ani.SetTrigger("StandStill");
        }
    }
}
