using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewired;
using UnityEngine;
using UnityEngine.Events;

   public class PlayerInput:MonoBehaviour
    {
    Rewired.Player player;
    Player currentPlayer;

    [SerializeField]
    Movement movement;

    [SerializeField]
    FireEvent FireButtonPressedEvent;

    [SerializeField]
    UnityEvent dashButtonPressedEvent;

    [SerializeField]
    RotationEvent rotationEvent;
    [SerializeField]
    Transform MouseTargetLocation;

    public bool mouseInput;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);
        
        Debug.Log(player.GetAxis2D("Move Horizontal", "Move Vertical"));
    }

    private void Update()
    {
        movement.Move(player.GetAxis2D("Move Horizontal", "Move Vertical"));
        //Debug.Log(player.GetAxis2D("Move Horizontal", "Move Vertical"));

        if (player.GetButton("Fire"))
        {
            FireButtonPressed();
        }
        if (player.GetButton("Fire2"))
        {
            Fire2ButtonPressed();
        }

        if (mouseInput)
        {
            Vector3 v_diff = (MouseTargetLocation.position - transform.position);
            float Rad = Mathf.Atan2(v_diff.y, v_diff.x);
            float angle = Rad * Mathf.Rad2Deg;
            rotationEvent.Invoke(angle);
        }

        else
        {
          rotationEvent.Invoke(Quaternion.LookRotation(player.GetAxis2D("Rotate Horizontal", "Rotate Vertical")).eulerAngles.z);
        //    Debug.Log(Quaternion.LookRotation(player.GetAxis2D("Rotate Horizontal", "Rotate Vertical")).eulerAngles.z);
        }
    }


    void FireButtonPressed()
    {
        FireButtonPressedEvent.Invoke(0);
    }

    void Fire2ButtonPressed()
    {
        FireButtonPressedEvent.Invoke(1);
    }

    void DashButtonPressed() { }


}
[System.Serializable]
public class FireEvent : UnityEvent<int> { }

[Serializable]
public class RotationEvent : UnityEvent<float> { }
