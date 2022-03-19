﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceKick_EX : CharaBaseState
{
    private float secs;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("PierceKick");
        var time = machine.player.anima.GetCurrentAnimatorStateInfo(0);
        secs = time.length * 4f;
        
        machine.player.isAttacking = true;
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        if (!machine.player.wallColl)
        {
            machine.player._rig.velocity = new Vector2((machine.player.speed + machine.player.accel) * machine.player.face, machine.player._rig.velocity.y);
        }
        else
        {
            machine.player._rig.velocity = new Vector2(0f, machine.player._rig.velocity.y);
        }
    }

    public override void UpdateState(CharaStateManager machine)
    {
        Chronological(machine);
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.player.isAttacking = false;
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        if (other.collider.CompareTag("Damager"))
        {
            if (machine.player.isGrounded)
            {
                machine.SwitchState(machine.collapse);
            }
            else
            {
                machine.SwitchState(machine.fall);
            }
           
        }
    }

    public override void OnDisableState(CharaStateManager machine)
    {
       
    }

    void Chronological(CharaStateManager machine)
    {
        if (secs > 0f)
        {
            secs -= Time.deltaTime;
        }
        else
        {
            if (machine.player.isGrounded)
            {
                if (!machine.player.wallColl)
                {
                    machine.SwitchState(machine._forwards);
                }
                else
                {
                    if (machine.player.isBoosting)
                    {
                        machine.SwitchState(machine.wallSlide);
                    }
                    else
                    {
                        machine.SwitchState(machine.stand);
                    }
                }
            }
            else
            {
                machine.SwitchState(machine.suspended);
            }
            machine.player.maxSpeed = 3;
            machine.player.speed = machine.player.maxSpeed;
        }
    }
}
