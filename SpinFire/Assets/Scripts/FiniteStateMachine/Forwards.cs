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
    }

    public override void UpdateState(CharaStateManager machine)
    {
        machine.transform.Translate(machine.player.face * (machine.player.speed * Time.deltaTime + machine.player.accel),0f,0f);
        
        machine.player.RegulateSpeed();
        
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
        
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        if (other.GetContact(0).normal == Vector2.right || other.GetContact(0).normal == Vector2.left)
        {
            machine.player.groundColDir = false;
            //machine.ReverseFace();
        }  
    }
    
}
