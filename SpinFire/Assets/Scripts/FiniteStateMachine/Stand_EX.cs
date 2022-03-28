using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Stand_EX : CharaBaseState
{
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Lenalee_stand");
        machine.rightActions.OnPressedRight += StandRightActs;
        machine.leftActions.OnPressedLeft += StandLeftActs;
        machine.upActions.OnPressedUp += Jump;
        machine.downActions.OnPressedDown += Engarde;
        machine.player.speed = 0f;
        if (!machine.player.wallColl) machine.player._rig.velocity = Vector2.right * machine.player.face;
        else
        {
            machine.player._rig.velocity = Vector2.zero;
        }

        machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[0];
        machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[1];
        machine.player.centerActions.arrowRenderers[2].sprite = machine.player.centerActions.options[2];
        machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[8];
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        
    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (machine.player.isBoosting) machine.SwitchState(machine.boost);
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.rightActions.OnPressedRight -= StandRightActs;
        machine.leftActions.OnPressedLeft -= StandLeftActs;
        machine.upActions.OnPressedUp -= Jump;
        machine.downActions.OnPressedDown -= Engarde;
        machine.player.speed = 3f;
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        if (other.collider.CompareTag("Damager"))
        {
            machine.SwitchState(machine.collapse);
        }
    }

    public override void OnDisableState(CharaStateManager machine)
    {
        machine.rightActions.OnPressedRight -= StandRightActs;
        machine.leftActions.OnPressedLeft -= StandLeftActs;
        machine.upActions.OnPressedUp -= Jump;
        machine.downActions.OnPressedDown -= Engarde;
    }
    
    private void StandRightActs(CharaStateManager machine)
    {
        if (!machine.player.wait)
        {
            if (machine.player.face == 1f)
            {
                if (!machine.player.wallColl) machine.SwitchState(machine._forwards);
            }
            else
            {
                machine.ReverseFace();
                machine.player.wallColl = false;
                machine.SwitchState(machine._forwards);
            }
        }
    }
    
    private void StandLeftActs(CharaStateManager machine)
    {
        if (!machine.player.wait)
        {
            if (machine.player.face == -1f)
            {
                if (!machine.player.wallColl) machine.SwitchState(machine._forwards);
            }
            else
            {
                machine.ReverseFace();
                machine.player.wallColl = false;
                machine.SwitchState(machine._forwards);
            }
        
        }
    }

    private void Jump(CharaStateManager machine)
    {
        machine.SwitchState(machine.jump);
    }

    private void Engarde(CharaStateManager machine)
    {
        machine.SwitchState(machine.guard);
    }
    
}
