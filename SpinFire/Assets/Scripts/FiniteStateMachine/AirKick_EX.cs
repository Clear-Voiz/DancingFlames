using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirKick_EX : CharaBaseState
{
    private GameObject PFX;
    private float[] time = new float[2];
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("AirKick");
        machine.ring.alarm[0] = 0.6f;
        //time[0] = 0.6f;
        machine.ring.alarm[3] = 0.2f;
        //time[1] = 0.2f;
        machine.player.isAttacking = true;

        machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[2].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[8];
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
        if (machine.player.isGrounded) machine.SwitchState(machine.land);
        //machine.ring.alarm[0] = machine.ring.Alarm(time[0],SwitchState, machine);
        
        machine.ring.alarm[0] = machine.ring.Alarm(machine.ring.alarm[0],SwitchState, machine);
        machine.ring.alarm[3] = machine.ring.Alarm(machine.ring.alarm[3], FX, machine);
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.player.isAttacking = false;
        if (PFX != null)
        {
            MonoBehaviour.Destroy(PFX);
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

    private void SwitchState(CharaStateManager machine)
    {
        if (machine.player._rig.velocity.y > -2f)
        {
            machine.SwitchState(machine.suspended);
        }
        else if (machine.player._rig.velocity.y <= -2f)
        {
            machine.SwitchState(machine.descend);
        }
    }

    private void FX(CharaStateManager machine)
    {
        PFX = MonoBehaviour.Instantiate(machine.player.AirKickFX, machine.player.transform);
    }
}
