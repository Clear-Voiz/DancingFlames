using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Aerial_Sweep_EX : CharaBaseState
{
    private float secs;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("AerialSweep");
        var aniEnd = machine.player.anima.GetCurrentAnimatorStateInfo(0);
        secs = 0.6f;//aniEnd.length * 3f;
        machine.player.isAttacking = true;
        
        machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[2].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[8];
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        if (machine.player.wallColl)
        {
            machine.player._rig.velocity = new Vector2(0f, machine.player._rig.velocity.y);
        }
    }

    public override void UpdateState(CharaStateManager machine)
    {
       if (machine.player.isGrounded) machine.SwitchState(machine.land);
       if (machine.player.wallColl && machine.player.isBoosting) machine.SwitchState(machine.wallSlide);
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
            machine.SwitchState(machine.fall);
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
            if (machine.player.isBoosting) machine.SwitchState(machine.boost);
            else
            {
                machine.SwitchState(machine.player._rig.velocity.y>=-1f?(CharaBaseState) machine.suspended:machine.descend);
            }
        }
    }
}
