﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspended_EX : CharaBaseState
{
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Suspend");
        machine.leftActions.OnPressedLeft += SuspendedLeftActs;
        machine.rightActions.OnPressedRight += SuspendedRightActs;
        machine.downActions.OnPressedDown += Dive;
    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (machine.player._rig.velocity.y <= -2f) machine.SwitchState(machine.descend);
        if (machine.player.isGrounded) machine.SwitchState(machine.land);
        if (machine.player.isBoosting) machine.SwitchState(machine.boost);
        if (Input.GetKeyDown(KeyCode.DownArrow)) machine.SwitchState(machine.dive);
        machine.transform.Translate(machine.player.face * (machine.player.speed * Time.deltaTime + machine.player.accel),0f,0f);
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) && machine.player.face == 1f) machine.ReverseFace();
        if (Input.GetKeyDown(KeyCode.RightArrow) && machine.player.face == -1f) machine.ReverseFace();
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.leftActions.OnPressedLeft -= SuspendedLeftActs;
        machine.rightActions.OnPressedRight -= SuspendedRightActs;
        machine.downActions.OnPressedDown -= Dive;
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (other.GetContact(0).normal == Vector2.right || other.GetContact(0).normal == Vector2.left)
            {
                machine.ReverseFace();
            }
        }  
    }

    public override void OnEnable(CharaStateManager machine)
    {
        
    }

    public override void OnDisable(CharaStateManager machine)
    {
        machine.leftActions.OnPressedLeft -= SuspendedLeftActs;
        machine.rightActions.OnPressedRight -= SuspendedRightActs;
        machine.downActions.OnPressedDown -= Dive;
    }
    
    public void SuspendedRightActs(CharaStateManager machine)
    {
        if (machine.player.face == 1f)
        {
            //machine.SwitchState(machine.lenakick);
        }
        else
        {
            machine.ReverseFace();
        }
    }
    
    public void SuspendedLeftActs(CharaStateManager machine)
    {
        if (machine.player.face == -1f)
        {
           // machine.SwitchState(machine.lenakick);
        }
        else
        {
            machine.ReverseFace();
        }
    }

    private void Dive(CharaStateManager machine)
    {
        machine.SwitchState(machine.dive);
    }
}
