using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse_EX : CharaBaseState
{
    private CharaStateManager charaMachine;
    private float time;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Collapse");
        machine.player.invenci = true;
        charaMachine = machine;
        time = machine.player.anima.GetCurrentAnimatorStateInfo(0).length;
        
        machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[2].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[8];
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        if (machine.player.wallColl)
        {
            machine.player.speed = 0f;
            machine.player._rig.velocity = Vector2.zero;
        }
    }

    public override void UpdateState(CharaStateManager machine)
    {
        time = machine.ring.alarm[1] = machine.ring.Alarm(time, SwitchState);
    }

    public override void ExitState(CharaStateManager machine)
    {
        
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        
    }

    public override void OnDisableState(CharaStateManager machine)
    {
        
    }

    private void SwitchState()
    {
        charaMachine.SwitchState(charaMachine.standUp); //change this to stand up
    }
}
