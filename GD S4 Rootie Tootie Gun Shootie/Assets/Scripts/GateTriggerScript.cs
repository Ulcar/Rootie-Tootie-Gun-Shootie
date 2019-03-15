using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTriggerScript : MonoBehaviour
{
    public GateScript[] Gates;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Gate.animator.StartPlayback();
        foreach (GateScript gate in Gates)
        {
            gate.MoveGate();
        }
        
    }


}
