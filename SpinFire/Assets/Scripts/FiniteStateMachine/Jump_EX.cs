using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_EX : CharaBaseState
{
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Jump");
        machine.player.isGrounded = false;
        machine.player._rig.AddForce(Vector2.up * (4f), ForceMode2D.Impulse);
        machine.rightActions.OnPressedRight += JumpRightActs;
        machine.leftActions.OnPressedLeft += JumpLeftActs;
        machine.downActions.OnPressedDown += Dive;
    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (machine.player._rig.velocity.y <= 0f) machine.SwitchState(machine.suspended);
        
        if (machine.player.isBoosting) machine.SwitchState(machine.boost);

        if (Input.GetKeyDown(KeyCode.DownArrow)) machine.SwitchState(machine.dive);
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) && machine.player.face == 1f) machine.ReverseFace();
        if (Input.GetKeyDown(KeyCode.RightArrow) && machine.player.face == -1f) machine.ReverseFace();
        
        machine.transform.Translate(machine.player.face * (machine.player.speed * Time.deltaTime + machine.player.accel),0f,0f);
        

    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.rightActions.OnPressedRight -= JumpRightActs;
        machine.leftActions.OnPressedLeft -= JumpLeftActs;
        machine.downActions.OnPressedDown -= Dive;
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
        machine.rightActions.OnPressedRight -= JumpRightActs;
        machine.leftActions.OnPressedLeft -= JumpLeftActs;
        machine.downActions.OnPressedDown -= Dive;
    }
    
    private void JumpRightActs(CharaStateManager machine)
    {
        if (machine.player.face == 1f)
        {
            //machine.SwitchState(machine.lenakick);
        }
        else
        {
            machine.ReverseFace();
        }
    }
    
    private void JumpLeftActs(CharaStateManager machine)
    {
        if (machine.player.face == -1f)
        {
            // machine.SwitchState(machine.lenakick);
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
