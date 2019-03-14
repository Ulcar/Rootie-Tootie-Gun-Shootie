using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    [CreateAssetMenu]
   public class EnemyGeneration:ScriptableObject
    {
    [SerializeField]
    List<WeaponPoolSet> weaponPoolDict;
    [SerializeField]
    List<PrefabPoolSet> prefabPoolSets;

  public  EnemyStats GenerateEnemyStats(EnemyType type)
    {
        EnemyStats stats = ScriptableObject.CreateInstance<EnemyStats>();
        int weaponCount = 1;
        List<WeaponStats> weaponPool;
        if (WeaponPoolSet.TryGetWeaponPool(weaponPoolDict, type, out weaponPool))
        {
            List<WeaponStats> enemyWeaponList = new List<WeaponStats>();
            for (int i = 0; i < weaponCount; i++)
            {
                //copy scriptableobject in dict so the original doesn't change at runtime
                enemyWeaponList.Add(ScriptableObject.Instantiate(weaponPool[UnityEngine.Random.Range(0, weaponPool.Count)]));
            }
            stats.weapons = enemyWeaponList;
        }


        return stats;

    }

    public GameObject GenerateEnemyPrefab(EnemyType type)
    {
        List<GameObject> prefabs;
        GameObject prefab = null;
        if (PrefabPoolSet.TryGetPrefabPool(prefabPoolSets, type, out prefabs))
        {
            prefab = prefabs[UnityEngine.Random.Range(0, prefabs.Count - 1)];
        }

        return prefab;
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

[Serializable]
public class PrefabPoolSet
{
    [SerializeField]
    EnemyType type;

    [SerializeField]
    List<GameObject> prefabs;

    public static bool TryGetPrefabPool(List<PrefabPoolSet> sets, EnemyType type, out List<GameObject> prefabs)
    {
        prefabs = null;
        foreach (PrefabPoolSet set in sets)
        {
            if (set.type == type)
            {
                prefabs = set.prefabs;
                return true;
            }
        }
        return false;
    }
}
