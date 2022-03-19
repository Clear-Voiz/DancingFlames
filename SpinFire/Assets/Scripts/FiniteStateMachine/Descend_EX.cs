using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descend_EX : CharaBaseState
{
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Descend");
        machine.rightActions.OnPressedRight += DescendRightActs;
        machine.leftActions.OnPressedLeft += DescendLeftActs;
        machine.downActions.OnPressedDown += Dive;
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        if (!machine.player.wallColl)
        {
            machine.player._rig.velocity = new Vector2((machine.player.speed + machine.player.accel) * machine.player.face,
                    machine.player._rig.velocity.y);
        }
        else
        {
            machine.player._rig.velocity = new Vector2(0f, machine.player._rig.velocity.y);
        }
    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (machine.player.isGrounded) machine.SwitchState(machine.land);
        if (machine.player.isBoosting) machine.SwitchState(machine.boost);
        if (Input.GetKeyDown(KeyCode.DownArrow)) machine.SwitchState(machine.dive);

        if (Input.GetKeyDown(KeyCode.LeftArrow) && machine.player.face == 1f) machine.ReverseFace();
        if (Input.GetKeyDown(KeyCode.RightArrow) && machine.player.face == -1f) machine.ReverseFace();
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.rightActions.OnPressedRight -= DescendRightActs;
        machine.leftActions.OnPressedLeft -= DescendLeftActs;
        machine.downActions.OnPressedDown -= Dive;
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
       /* if (other.collider.CompareTag("Ground"))
        {
            if (other.GetContact(0).normal == Vector2.right || other.GetContact(0).normal == Vector2.left)
            {
                machine.ReverseFace();
            }
        }
        */
        if (other.collider.CompareTag("Damager"))
        {
            machine.SwitchState(machine.fall);
        }
    }

    public override void OnDisableState(CharaStateManager machine)
    {
        machine.rightActions.OnPressedRight -= DescendRightActs;
        machine.leftActions.OnPressedLeft -= DescendLeftActs;
        machine.downActions.OnPressedDown -= Dive;
    }
    
    public void DescendRightActs(CharaStateManager machine)
    {
        if (machine.player.face == 1f)
        {
            machine.SwitchState(machine.airKick);
        }
        else
        {
            machine.ReverseFace();
        }
    }
    
    public void DescendLeftActs(CharaStateManager machine)
    {
        if (machine.player.face == -1f)
        {
            machine.SwitchState(machine.airKick);
        }
        else
        {
            machine.ReverseFace();
        }
    }
    
    private void Dive(CharaStateManager machine)
    {
        machine.SwitchState(machine.dive);
    }
}
