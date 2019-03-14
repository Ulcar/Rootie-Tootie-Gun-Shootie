using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

   public class EnemyGeneration
    {
    [SerializeField]
    List<WeaponPoolSet> weaponPoolDict;
    Dictionary<EnemyType, GameObject> prefabDict;

  public  void GenerateEnemy(EnemyType type)
    {
        int weaponCount = 2;
        List<WeaponStats> weaponPool;
        if (WeaponPoolSet.TryGetWeaponPool(weaponPoolDict, type, out weaponPool))
        {
            List<WeaponStats> enemyWeaponList = new List<WeaponStats>();
            for (int i = 0; i < weaponCount; i++)
            {
                enemyWeaponList.Add(ScriptableObject.Instantiate(weaponPool[UnityEngine.Random.Range(0, weaponPool.Count - 1)]));
            }
        }

        else
        {
            Debug.LogWarning("This EnemyType has no WeaponPool: " + type);
        }
    }

   public void SpawnEnemy(EnemyStats enemyToSpawn, EnemyType type)
    {
        GameObject tmp = GameObject.Instantiate(prefabDict[type]);
        tmp.GetComponent<Enemy>().Init(enemyToSpawn);
    }
}

[Serializable]
public enum EnemyType
{
    Ranged,
    Sniper,
    Melee
}
[Serializable]
public class WeaponPoolSet
{
    [SerializeField]
    EnemyType type;
    [SerializeField]
    List<WeaponStats> weaponPool;

    public static bool TryGetWeaponPool(List<WeaponPoolSet> sets, EnemyType type, out List<WeaponStats> stats)
    {
        stats = null;
        foreach (WeaponPoolSet set in sets)
        {
            if (set.type == type)
            {
                stats = set.weaponPool;
                return true;
            }
        }
        return false;
    }
}
