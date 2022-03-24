using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallImpulse_EX : CharaBaseState
{
    public override void EnterState(CharaStateManager machine)
    {
        machine.ReverseFace();
        machine.player.anima.Play("WallImpulse");
        machine.player._rig.gravityScale = 0f;
        machine.player.speed = 5f;

        machine.StartCoroutine(machine.ring.alarm[5] = machine.ring.Alarm(0.3f,Impulse, machine));
        machine.StartCoroutine(machine.ring.alarm[6] = machine.ring.Alarm(0.5f, Next, machine));
        
        machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[2].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[8];
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        
    }

    public override void UpdateState(CharaStateManager machine)
    {
        
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.player._rig.gravityScale = 1f;
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

    private void Impulse(CharaStateManager machine)
    {
        machine.player._rig.AddForce(Vector2.right * machine.player.face * 8f,ForceMode2D.Impulse);
    }

    private void Next(CharaStateManager machine)
    {
        Debug.Log("has been executed");
        if (machine.player.isBoosting)
        {
            machine.SwitchState(machine.boost);
        }
        else
        {
            machine.SwitchState(machine.suspended);
        }
    }
}
