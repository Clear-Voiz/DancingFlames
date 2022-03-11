using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forwards : CharaBaseState
{
    public Activate ring;
    bool pressed = true;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("forwards");
        machine.player.maxSpeed = 3f;
        machine.player.increment = 0.1f;
        machine.rightActions.OnPressedRight += ForwardsRightActs;
        machine.leftActions.OnPressedLeft += ForwardsLeftActs;
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
        machine.rightActions.OnPressedRight -= ForwardsRightActs;
        machine.leftActions.OnPressedLeft -= ForwardsLeftActs;
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (other.GetContact(0).normal == Vector2.right || other.GetContact(0).normal == Vector2.left)
            {
                machine.ReverseFace();
            }
        }  
    }

    public override void OnEnable(CharaStateManager machine)
    {
       
    }

    public override void OnDisable(CharaStateManager machine)
    {
        machine.rightActions.OnPressedRight -= ForwardsRightActs;
        machine.leftActions.OnPressedLeft -= ForwardsLeftActs;
    }

    public void ForwardsRightActs(CharaStateManager machine)
    {
        if (machine.player.face == 1f)
        {
            machine.SwitchState(machine.lenakick);
        }
        else
        {
            machine.ReverseFace();
        }
    }
    
    public void ForwardsLeftActs(CharaStateManager machine)
    {
        //if (pressed)

        {
            pressed = false;
            ring.alarm[0] = ring.Alarm(0.02f, rePressed);
            if (machine.player.face == -1f)
            {
                machine.SwitchState(machine.lenakick);
                Debug.Log("kicked");
            }
            else
            {
             machine.ReverseFace();
             Debug.Log("reversed");
            }
        
        }
        
        
    }

    public void rePressed()
    {
        Debug.Log("it works");
    }
}
