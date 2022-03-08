using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide_EX : CharaBaseState
{

    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("WallSlide");
        machine.player.isGrounded = false;
    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (!machine.player.isBoosting) machine.SwitchState(machine.suspended);
        
        machine.transform.Translate(0f, machine.player.speed * Time.deltaTime + machine.player.accel,0f);
        
        //if (!machine.player.isWallSliding) machine.SwitchState(machine.boost);
    }

    public override void ExitState(CharaStateManager machine)
    {
        
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        
    }
    
}
