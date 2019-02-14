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
    protected void SpawnProjectile(float x, float y, int rotation, GameObject bullet)
    {
        var bulletposition = new Vector3(x, y, 0);
        bullet.transform.localPosition = bulletposition;
        bullet.transform.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    protected void SpawnPosition(int rotation, GameObject bullet)
    {
        float xposition = Mathf.Cos((rotation * Mathf.PI) / 180) * 3;
        float yposition = Mathf.Sin((rotation * Mathf.PI) / 180) * 3;
        SpawnProjectile(xposition, yposition, rotation, bullet);
    }

}
