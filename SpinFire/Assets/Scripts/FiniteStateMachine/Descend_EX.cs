using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descend_EX : CharaBaseState
{
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Descend");
    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (machine.player.isGrounded) machine.SwitchState(machine.land);
        if (machine.player.isBoosting) machine.SwitchState(machine.boost);
        machine.transform.Translate(machine.player.face * (machine.player.speed * Time.deltaTime + machine.player.accel),0f,0f);
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision other)
    {
        
    }
}
