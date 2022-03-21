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
        
        machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[2].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[8];
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        machine.player._rig.velocity = new Vector2(0f, machine.player.speed + machine.player.accel);
        machine.player.RegulateSpeed();
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
        
        //machine.transform.Translate(0f, machine.player.speed * Time.deltaTime + machine.player.accel,0f);
        
        //if (!machine.player.isWallSliding) machine.SwitchState(machine.boost);
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.player.isWallSliding = false;
        machine.player._rig.gravityScale = 1;
        if (machine.player.wallColl)
        {
            machine.player._rig.velocity = Vector2.zero;
            machine.player.speed = 0.2f;
        }
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        if (other.collider.CompareTag("Damager"))
        {
            machine.SwitchState(machine.fall);
        }
    }

    public override void OnDisableState(CharaStateManager machine)
    {
       
    }
}
