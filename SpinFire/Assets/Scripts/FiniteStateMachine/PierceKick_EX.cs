using System.Collections;
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
    }

    public override void UpdateState(CharaStateManager machine)
    {
        machine.transform.Translate(machine.player.face * (machine.player.speed * Time.deltaTime + machine.player.accel),0f,0f);
        Chronological(machine);
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision other)
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
            machine.SwitchState(machine.player.isGrounded?(CharaBaseState) machine._forwards:machine.suspended);
            machine.player.maxSpeed = 3;
            machine.player.speed = machine.player.maxSpeed;
        }
    }
}
