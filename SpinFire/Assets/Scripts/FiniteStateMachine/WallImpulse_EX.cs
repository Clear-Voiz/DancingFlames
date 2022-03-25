using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallImpulse_EX : CharaBaseState
{
    private CharaStateManager charaMachine;
    private float[] time = new float[2];
    public override void EnterState(CharaStateManager machine)
    {
        machine.ReverseFace();
        machine.player.anima.Play("WallImpulse");
        machine.player._rig.gravityScale = 0f;
        machine.player.speed = 5f;
        charaMachine = machine;

        time[0] = 0.3f; // Impulse
        time[1] = 0.5f; // Next

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
        time[0] = machine.ring.alarm[5] = machine.ring.Alarm(time[0], Impulse);
        time[1] = machine.ring.alarm[6] = machine.ring.Alarm(time[1], Next);
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

    private void Impulse()
    {
        charaMachine.player._rig.AddForce(Vector2.right * charaMachine.player.face * 8f,ForceMode2D.Impulse);
    }

    private void Next()
    {
        Debug.Log("has been executed");
        if (charaMachine.player.isBoosting)
        {
            charaMachine.SwitchState(charaMachine.boost);
        }
        else
        {
            charaMachine.SwitchState(charaMachine.suspended);
        }
    }
}
