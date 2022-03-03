using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land_EX : CharaBaseState
{
    private float secs;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Land");
        var time = machine.player.anima.GetCurrentAnimatorStateInfo(0);
        secs = time.length;
        
        machine.player.maxSpeed = 3f;
        machine.player.increment = 0.1f;
        if (machine.player.speed > machine.player.maxSpeed) machine.player.speed = machine.player.maxSpeed;
    }

    public override void UpdateState(CharaStateManager machine)
    {
        Chronological(machine);
        if (Input.GetKeyDown(KeyCode.UpArrow)) machine.SwitchState(machine.jump);
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision other)
    {
       
    }

    void Chronological(CharaStateManager machine)
    {
        if (secs > 0f)
        {
            secs -= Time.deltaTime;
        }
        else
        {
            machine.SwitchState(machine._forwards);
        }
    }
}
