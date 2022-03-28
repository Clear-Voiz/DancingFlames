using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost_Ex : CharaBaseState
{
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Boost");
        machine.player._rig.velocity = Vector2.zero;
        machine.player._rig.gravityScale = 0;
        machine.player.maxSpeed = 6f;
        machine.player.increment = 0.1f;
        machine.upActions.OnPressedUp += UpKick;
        machine.downActions.OnPressedDown += DownActs;
        machine.rightActions.OnPressedRight += RightPierce;
        machine.leftActions.OnPressedLeft += LeftPierce;

        if (machine.player.isGrounded)
        {
            machine.player.centerActions.arrowRenderers[2].sprite = machine.player.centerActions.options[6];
            machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[3];
        }
        else
        {
            machine.player.centerActions.arrowRenderers[2].sprite = (machine.player.hasAirdodged) ? machine.player.centerActions.options[8] : machine.player.centerActions.options[2];
            machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[7];
        }

        if ((int)machine.player.face == 1)
        {
            machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[4];
            machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[8];
        }
        else
        {
            machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[8];
            machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[5];
        }
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        if (!machine.player.wallColl)
        {
            machine.player._rig.velocity = new Vector2((machine.player.speed + machine.player.accel) * machine.player.face, machine.player._rig.velocity.y);
        }
        
        if (machine.player.isGrounded)
        {
            machine.player.centerActions.arrowRenderers[2].sprite = machine.player.centerActions.options[6];
            machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[3];
        }
        else
        {
            machine.player.centerActions.arrowRenderers[2].sprite = (machine.player.hasAirdodged) ? machine.player.centerActions.options[8] : machine.player.centerActions.options[2];
            machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[7];
        }
    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (!machine.player.isBoosting) machine.SwitchState(machine.player.isGrounded?(CharaBaseState) machine._forwards:machine.suspended);
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) machine.SwitchState(machine.pierceKick);
        if (Input.GetKeyDown(KeyCode.DownArrow)) machine.SwitchState(machine.player.isGrounded?(CharaBaseState) machine.dash:machine.aerialSweep);
        if (Input.GetKeyDown(KeyCode.UpArrow) && machine.player.isGrounded) machine.SwitchState(machine.upKick);
        if (machine.player.wallColl) machine.SwitchState(machine.wallSlide);
        
        machine.player.RegulateSpeed();
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.player._rig.gravityScale = 1;
        machine.upActions.OnPressedUp -= UpKick;
        machine.downActions.OnPressedDown -= DownActs;
        machine.rightActions.OnPressedRight -= RightPierce;
        machine.leftActions.OnPressedLeft -= LeftPierce;
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        if (other.collider.CompareTag("Damager"))
        {
            if (machine.player.isGrounded)
            {
                machine.SwitchState(machine.collapse);
            }
            else
            {
                machine.SwitchState(machine.fall);
            }
        }
    }

    public override void OnDisableState(CharaStateManager machine)
    {
        machine.upActions.OnPressedUp -= UpKick;
        machine.downActions.OnPressedDown -= DownActs;
        machine.rightActions.OnPressedRight -= RightPierce;
        machine.leftActions.OnPressedLeft -= LeftPierce;
    }

    private void UpKick(CharaStateManager machine)
    {
        if (machine.player.isGrounded) machine.SwitchState(machine.upKick);
        else
        {
            if (machine.player.hasAirdodged == false)
            {
                machine.player._rig.AddForce(new Vector2(0f, 5), ForceMode2D.Impulse);
                machine.player.hasAirdodged = true;
                //machine.player.centerActions.arrowRenderers[2].sprite = machine.player.centerActions.options[8];
                machine.SwitchState(machine.aerialSweep);
            }
        }
    }
    
    private void DownActs(CharaStateManager machine)
    {
        if (machine.player.isGrounded)
        {
            machine.SwitchState(machine.dash);
        }
        else
        {
            machine.SwitchState(machine.aerialSweep);
        }
    }

    private void RightPierce(CharaStateManager machine)
    {
        if (machine.player.face == 1)
        {
            machine.SwitchState(machine.pierceKick);
        }
        else
        {
            machine.SwitchState(machine.reverseBoost);
        }
    }
    
    private void LeftPierce(CharaStateManager machine)
    {
        if (machine.player.face == -1)
        {
            machine.SwitchState(machine.pierceKick);
        }
        else
        {
            machine.SwitchState(machine.reverseBoost);
        }
    }
}
