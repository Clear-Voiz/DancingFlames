using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpKick_EX : CharaBaseState
{
    private float secs;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("UpKick");
        secs = machine.player.anima.GetCurrentAnimatorStateInfo(0).length;
        
        machine.player.isGrounded = false;
        machine.player._rig.AddForce(Vector2.up * (6f), ForceMode2D.Impulse);
    }

    public override void UpdateState(CharaStateManager machine)
    {
        machine.transform.Translate(machine.player.face * (machine.player.speed * Time.deltaTime + machine.player.accel),0f,0f);
        //Chronological(machine);
        if (machine.player.isGrounded) machine.SwitchState(machine.land);
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision other)
    {
        
    }

    void Chronological(CharaStateManager machine)
    {
        if (secs > 0f)
        {
            secs -= Time.deltaTime;
            Debug.Log(secs);
        }
        else
        {
            if (machine.player.isBoosting)
            {
                machine.SwitchState(machine.boost);
            }
            else
            {
                machine.SwitchState(machine.player.isGrounded?(CharaBaseState) machine.land:machine.suspended);
            }
        }
    }
}
