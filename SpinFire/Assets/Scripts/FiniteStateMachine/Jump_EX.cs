using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_EX : CharaBaseState
{
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Jump");
        machine.player._rig.AddForce(Vector2.up * (4f), ForceMode2D.Impulse);
        machine.player.isGrounded = false;
        machine.rightActions.OnPressedRight += JumpRightActs;
        machine.leftActions.OnPressedLeft += JumpLeftActs;
        machine.downActions.OnPressedDown += Dive;
        machine.upActions.OnPressedUp += AirDodge;
        
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
        if (machine.player._rig.velocity.y <= 0f) machine.SwitchState(machine.suspended);
        
        if (machine.player.isBoosting) machine.SwitchState(machine.boost);

        if (Input.GetKeyDown(KeyCode.DownArrow)) machine.SwitchState(machine.dive);
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) && machine.player.face == 1f) machine.ReverseFace();
        if (Input.GetKeyDown(KeyCode.RightArrow) && machine.player.face == -1f) machine.ReverseFace();

    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.rightActions.OnPressedRight -= JumpRightActs;
        machine.leftActions.OnPressedLeft -= JumpLeftActs;
        machine.downActions.OnPressedDown -= Dive;
        machine.upActions.OnPressedUp -= AirDodge;
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
       /* if (other.collider.CompareTag("Ground"))
        {
            if (other.GetContact(0).normal == Vector2.right || other.GetContact(0).normal == Vector2.left)
            {
                machine.ReverseFace();
            }
        }*/

        if (other.collider.CompareTag("Damager"))
        {
            machine.SwitchState(machine.fall);
        }
    }
    
    public override void OnDisableState(CharaStateManager machine)
    {
        machine.rightActions.OnPressedRight -= JumpRightActs;
        machine.leftActions.OnPressedLeft -= JumpLeftActs;
        machine.downActions.OnPressedDown -= Dive;
        machine.upActions.OnPressedUp -= AirDodge;
    }
    
    private void JumpRightActs(CharaStateManager machine)
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
    
    private void JumpLeftActs(CharaStateManager machine)
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

    private void AirDodge(CharaStateManager machine)
    {
        if (machine.player.hasAirdodged == false)
        {
            machine.player._rig.AddForce(new Vector2(0f,8f));
            machine.player.hasAirdodged = true;
            machine.SwitchState(machine.aerialSweep);
        }
    }
    private void Dive(CharaStateManager machine)
    {
        machine.SwitchState(machine.dive);
    }
}
