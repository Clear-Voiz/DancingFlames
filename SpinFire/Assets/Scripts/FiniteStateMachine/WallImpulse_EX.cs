using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallImpulse_EX : CharaBaseState
{ private float[] time = new float[3];
    public GameObject EruptionFX;
    public override void EnterState(CharaStateManager machine)
    {
        machine.ReverseFace();
        machine.player.anima.Play("WallImpulse");
        machine.player._rig.gravityScale = 0f;
        machine.player.speed = 5f;

        time[0] = 0.3f; // Impulse
        time[1] = 0.5f; // Next
        time[2] = 0.1f; // Eruption
        //time[3] = 0.2f;

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
        time[0] = machine.ring.alarm[5] = machine.ring.Alarm(time[0], Impulse,machine);
        time[1] = machine.ring.alarm[6] = machine.ring.Alarm(time[1], Next,machine);
        if (machine.player.isReverseBoosting)
        {
            time[2] = machine.ring.alarm[8] = machine.ring.Alarm(time[2], Eruption, machine);
        }
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.player._rig.gravityScale = 1f;
        machine.player.isReverseBoosting = false;
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
        if (machine.player.isBoosting)
        {
            machine.SwitchState(machine.boost);
        }
        else
        {
            machine.SwitchState(machine.suspended);
        }
    }

    private void Eruption(CharaStateManager machine)
    {
        MonoBehaviour.Instantiate(machine.player.EruptionFX, machine.transform);
        machine.player._rig.velocity = Vector2.zero;
    }
}
