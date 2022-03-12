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
        machine.upActions.OnPressedUp += LenaKick;
        machine.downActions.OnPressedDown += DownActs;
        machine.rightActions.OnPressedRight += PierceKick_EX;
        machine.leftActions.OnPressedLeft += PierceKick_EX;

    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (!machine.player.isBoosting) machine.SwitchState(machine.player.isGrounded?(CharaBaseState) machine._forwards:machine.suspended);
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) machine.SwitchState(machine.pierceKick);
        if (Input.GetKeyDown(KeyCode.DownArrow)) machine.SwitchState(machine.player.isGrounded?(CharaBaseState) machine.dash:machine.aerialSweep);
        if (Input.GetKeyDown(KeyCode.UpArrow) && machine.player.isGrounded) machine.SwitchState(machine.upKick);
        if (machine.player.wallColl) machine.SwitchState(machine.wallSlide);
        
        machine.transform.Translate(machine.player.face * (machine.player.speed * Time.deltaTime + machine.player.accel),0f,0f);
        machine.player.RegulateSpeed();
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.player._rig.gravityScale = 1;
        machine.upActions.OnPressedUp -= LenaKick;
        machine.downActions.OnPressedDown -= DownActs;
        machine.rightActions.OnPressedRight -= PierceKick_EX;
        machine.leftActions.OnPressedLeft -= PierceKick_EX;
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        
    }

    public override void OnEnable(CharaStateManager machine)
    {
        
    }

    public override void OnDisable(CharaStateManager machine)
    {
        machine.upActions.OnPressedUp -= LenaKick;
        machine.downActions.OnPressedDown -= DownActs;
        machine.rightActions.OnPressedRight -= PierceKick_EX;
        machine.leftActions.OnPressedLeft -= PierceKick_EX;
    }

    private void LenaKick(CharaStateManager machine)
    {
        if (machine.player.isGrounded) machine.SwitchState(machine.upKick);
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

    private void PierceKick_EX(CharaStateManager machine)
    {
        machine.SwitchState(machine.pierceKick);
    }
}
