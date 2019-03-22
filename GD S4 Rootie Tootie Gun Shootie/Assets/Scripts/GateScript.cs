using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    bool GateOpen = true;
    public bool Node;
    public Animator animator;
    public bool GatePermOpened = false;
    public bool GateLocked = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LockGate()
    {
        if (!GateLocked && !GatePermOpened && Node)
        {
            GateLocked = true;
            animator.SetTrigger("CloseGate");
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.layer = LayerMask.NameToLayer("Wall");
        }
    }

    public void PermOpenGate()
    {
        if (!GatePermOpened && Node)
        {
            GatePermOpened = true;
            GateLocked = false;
            animator.SetTrigger("OpenGate");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    public void NormalOpenGate()
    {
        if (!GatePermOpened && Node)
        {
            animator.SetTrigger("OpenGate");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
