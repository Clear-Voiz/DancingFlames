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
    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (machine.player._rig.velocity.y <= 0f) machine.SwitchState(machine.suspended);
        
        if (machine.player.isBoosting) machine.SwitchState(machine.boost);

        if (machine.player.isGrounded) machine.SwitchState(machine.land);
        
        if (Input.GetKeyDown(KeyCode.DownArrow)) machine.SwitchState(machine.dive);
        
        machine.transform.Translate(machine.player.face * (machine.player.speed * Time.deltaTime + machine.player.accel),0f,0f);

    }

    public override void ExitState(CharaStateManager machine)
    {
        
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        
    }
    
}
