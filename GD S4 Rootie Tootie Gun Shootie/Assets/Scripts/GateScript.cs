using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    bool GateOpen = true;

    public SpriteRenderer renderer;
    public Animator animator;
    public bool GatePermOpened = false;
    public bool GateLocked = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveGate()
    {
        if (GateOpen && !GateLocked)
        {
            animator.SetTrigger("CloseGate");
            GateOpen = false;
            GateLocked = true;
        }
        else
        {
            animator.SetTrigger("OpenGate");
            GateOpen = true;
        }
    }
}
