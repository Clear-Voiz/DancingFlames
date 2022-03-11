using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenaKick_EX : CharaBaseState
{
    private float secs;
    private float time;
    private bool activated = true;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("LenaKick");
       var aniEnd = machine.player.anima.GetCurrentAnimatorStateInfo(0);
       secs = aniEnd.length;
       machine.player.isAttacking = true;
       activated = true;
       time = 0.2f;
    }

    public override void UpdateState(CharaStateManager machine)
    {
        Chronological(machine);
        
        if (activated) ParticleTimer(machine);
    }

    public override void ExitState(CharaStateManager machine)
    {
        machine.player.isAttacking = false;
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        
    }

    public override void OnEnable(CharaStateManager machine)
    {
        
    }

    public override void OnDisable(CharaStateManager machine)
    {
        
    }

    void Chronological(CharaStateManager machine)
    {
        if (secs > 0)
        {
            secs -= Time.deltaTime;
        }
        else
        {
            if (machine.player.isBoosting)
            {
                machine.SwitchState(machine.boost);
                return;
            }
            if (!machine.player.isGrounded)
            {
                if (machine.player._rig.velocity.y > -1)
                {
                    machine.SwitchState(machine.player._rig.velocity.y > -1? (CharaBaseState) machine.suspended : machine.descend);
                    return;
                }
            }
            machine.SwitchState(machine._forwards);
        }
    }

    private void ParticleTimer(CharaStateManager machine)
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            MonoBehaviour.Instantiate(machine.player.KickFX, machine.player.transform);
            activated = false;
        }
        
    }
}
