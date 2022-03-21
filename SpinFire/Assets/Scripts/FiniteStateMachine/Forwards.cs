using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forwards : CharaBaseState
{
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("forwards");
        machine.player.maxSpeed = 3f;
        machine.player.increment = 0.1f;
        machine.rightActions.OnPressedRight += ForwardsRightActs;
        machine.leftActions.OnPressedLeft += ForwardsLeftActs;
        machine.upActions.OnPressedUp += Jump;
        machine.downActions.OnPressedDown += Stop;

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
        machine.player._rig.velocity = new Vector2((machine.player.speed + machine.player.accel) * machine.player.face, machine.player._rig.velocity.y);
    }

    public override void UpdateState(CharaStateManager machine)
    {
        //machine.transform.Translate(machine.player.face * (machine.player.speed * Time.deltaTime + machine.player.accel),0f,0f);
        if (machine.player._rig.velocity.y < 0f) machine.SwitchState(machine.suspended);

        machine.player.RegulateSpeed();
        
        if (machine.player.wallColl) machine.SwitchState(machine.stand);

        /*if (machine.player.speed >= machine.player.speedGear)
            machine.SwitchState(machine.boost);*/

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (machine.player.face == 1f) machine.SwitchState(machine.lenakick);
            else
            {
                machine.ReverseFace();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (machine.player.face == -1f) machine.SwitchState(machine.lenakick);
            else
            {
                machine.ReverseFace();
            }
        }
        
        if (machine.player.isBoosting) machine.SwitchState(machine.boost);

        if (Input.GetKeyDown(KeyCode.UpArrow)) machine.SwitchState(machine.jump);
        
        
        //if(!machine.player.isGrounded) machine.SwitchState(machine.suspended);   convertir-ho a raycast
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.rightActions.OnPressedRight -= ForwardsRightActs;
        machine.leftActions.OnPressedLeft -= ForwardsLeftActs;
        machine.upActions.OnPressedUp -= Jump;
        machine.downActions.OnPressedDown -= Stop;
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        if (machine.player.wallColl) machine.SwitchState(machine.stand);

            if (other.collider.CompareTag("Damager"))
        {
            machine.SwitchState(machine.collapse);
        }
    }

    public override void OnDisableState(CharaStateManager machine)
    {
        machine.rightActions.OnPressedRight -= ForwardsRightActs;
        machine.leftActions.OnPressedLeft -= ForwardsLeftActs;
        machine.downActions.OnPressedDown -= Stop;
    }

    private void ForwardsRightActs(CharaStateManager machine)
    {
        if (machine.player.face == 1f)
        {
            machine.SwitchState(machine.lenakick);
        }
        else
        {
            machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[4];
            machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[1];
            machine.ReverseFace();
        }
    }
    
    private void ForwardsLeftActs(CharaStateManager machine)
    {
        if (machine.player.face == -1f)
        {
            machine.SwitchState(machine.lenakick);
        }
        else
        {
            machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[0];
            machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[5];
            machine.ReverseFace();
        }
    }

    private void Jump(CharaStateManager machine)
    {
        machine.SwitchState(machine.jump);
    }
    
    private void Stop(CharaStateManager machine)
    {
        machine.SwitchState(machine.stand);
    }
}
