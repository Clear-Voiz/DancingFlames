using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide_EX : CharaBaseState
{

    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("WallSlide");
        machine.player.isGrounded = false;
        machine.player.isWallSliding = true;
        machine.player._rig.gravityScale = 0;
        MonoBehaviour.Instantiate(machine.player.Up_Impulse, machine.player.transform.position, Quaternion.identity);
    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (!machine.player.isBoosting) machine.SwitchState(machine.suspended);

        if (!machine.player.wallColl)
        {
            if (machine.player.isBoosting)
            {
                machine.SwitchState(machine.boost);
            }
            else
            {
                machine.SwitchState(machine.suspended);
            }
               
        }
        
        machine.transform.Translate(0f, machine.player.speed * Time.deltaTime + machine.player.accel,0f);
        
        //if (!machine.player.isWallSliding) machine.SwitchState(machine.boost);
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.player.isWallSliding = false;
        machine.player._rig.gravityScale = 1;
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        
    }

    public override void OnEnable(CharaStateManager machine)
    {
        
    }

    public override void OnDisable(CharaStateManager machine)
    {
       
    }
}
