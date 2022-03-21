using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspended_EX : CharaBaseState
{
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Suspend");
        machine.leftActions.OnPressedLeft += SuspendedLeftActs;
        machine.rightActions.OnPressedRight += SuspendedRightActs;
        machine.downActions.OnPressedDown += Dive;
        
        if (machine.player.face == 1)
        {
            machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[4];
            machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[1];
        }
        else
        {
            machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[0];
            machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[5];
        }
        
        machine.player.centerActions.arrowRenderers[2].sprite = machine.player.centerActions.options[2];
        machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[3];
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
        if (machine.player._rig.velocity.y <= -2f) machine.SwitchState(machine.descend);
        if (machine.player.isGrounded) machine.SwitchState(machine.land);
        if (machine.player.isBoosting) machine.SwitchState(machine.boost);
        if (Input.GetKeyDown(KeyCode.DownArrow)) machine.SwitchState(machine.dive);

        if (Input.GetKeyDown(KeyCode.LeftArrow) && machine.player.face == 1f) machine.ReverseFace();
        if (Input.GetKeyDown(KeyCode.RightArrow) && machine.player.face == -1f) machine.ReverseFace();
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.leftActions.OnPressedLeft -= SuspendedLeftActs;
        machine.rightActions.OnPressedRight -= SuspendedRightActs;
        machine.downActions.OnPressedDown -= Dive;
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            if (other.GetContact(0).normal == Vector2.right || other.GetContact(0).normal == Vector2.left)
            {
                machine.ReverseFace();
            }
        }
        
        if (other.collider.CompareTag("Damager"))
        {
            machine.SwitchState(machine.fall);
        }
    }
    public override void OnDisableState(CharaStateManager machine)
    {
        machine.leftActions.OnPressedLeft -= SuspendedLeftActs;
        machine.rightActions.OnPressedRight -= SuspendedRightActs;
        machine.downActions.OnPressedDown -= Dive;
    }
    
    public void SuspendedRightActs(CharaStateManager machine)
    {
        if (machine.player.face == 1f)
        {
            machine.SwitchState(machine.airKick);
        }
        else
        {
            machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[4];
            machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[1];
            machine.ReverseFace();
        }
    }
    
    public void SuspendedLeftActs(CharaStateManager machine)
    {
        if (machine.player.face == -1f)
        { 
            machine.SwitchState(machine.airKick);
        }
        else
        {
            machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[0];
            machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[5];
            machine.ReverseFace();
        }
    }

    private void Dive(CharaStateManager machine)
    {
        machine.SwitchState(machine.dive);
    }
}
