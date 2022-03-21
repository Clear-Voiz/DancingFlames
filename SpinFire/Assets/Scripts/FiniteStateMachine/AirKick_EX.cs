using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirKick_EX : CharaBaseState
{
    private GameObject PFX;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("AirKick");
        var time = machine.player.anima.GetCurrentAnimatorStateInfo(0).length *2f;
        machine.StartCoroutine(machine.ring.alarm[0] = machine.ring.Alarm(time, SwitchState,machine));
        machine.player.isAttacking = true;
        machine.StartCoroutine(machine.ring.alarm[3] = machine.ring.Alarm(0.2f, FX, machine));
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
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.player.isAttacking = false;
        machine.StopCoroutine(nameof(FX));
        if (PFX)
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
