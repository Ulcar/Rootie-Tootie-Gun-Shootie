using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerationBehaviourTest : MonoBehaviour
{
    [SerializeField]
    EnemyGeneration generation;
    [SerializeField]
    EnemyType type;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SpawnEnemy()
    {
     GameObject tmp =    generation.GenerateEnemyPrefab(type);
      GameObject enemyToSpawn = Instantiate(tmp);
        EnemyStats stats = generation.GenerateEnemyStats(type);
        enemyToSpawn.GetComponentInChildren<Enemy>().Init(stats);
    }
}
