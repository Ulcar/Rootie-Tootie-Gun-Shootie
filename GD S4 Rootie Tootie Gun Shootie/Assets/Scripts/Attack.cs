using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
   public virtual List<GameObject> DoAttack()
    {
        List<GameObject> bullets = new List<GameObject>();

        return bullets;
    }


}
