using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    EnemyStats stats;
    void Start()
    {
        stats.Attack((int)transform.rotation.eulerAngles.z);
        string type = "StandardMovement";
        gameObject.AddComponent(Type.GetType(type));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
