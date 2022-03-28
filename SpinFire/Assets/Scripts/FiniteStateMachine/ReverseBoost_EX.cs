using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseBoost_EX : CharaBaseState
{
    private bool _isHitten;
    private float secs;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("ReverseBoost");
        secs = 0.2f;
        machine.player._rig.gravityScale = 0f;
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        
    }

    public override void UpdateState(CharaStateManager machine)
    {
        secs = machine.ring.alarm[7] = machine.ring.Alarm(secs, Next, machine);
    }

    public override void ExitState(CharaStateManager machine)
    {
        if (_isHitten)
        {
            machine.player._rig.gravityScale = 1f;
        }
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        if (other.collider.CompareTag("Damager"))
        {
            _isHitten = true;
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
    
    private void Next(CharaStateManager machine)
    {
        machine.player.isReverseBoosting = true;
        machine.SwitchState(machine.wallImpulse);
    }
}
