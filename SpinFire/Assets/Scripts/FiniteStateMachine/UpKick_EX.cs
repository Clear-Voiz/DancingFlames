using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpKick_EX : CharaBaseState
{
    private float _secs;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("UpKick");
        _secs = machine.player.anima.GetCurrentAnimatorStateInfo(0).length * 3f;

        machine.player.isGrounded = false;
        machine.player._rig.AddForce(Vector2.up * (6f), ForceMode2D.Impulse);
        
        MonoBehaviour.Instantiate(machine.player.PS, machine.player.transform.position, Quaternion.identity);
    }

    public override void UpdateState(CharaStateManager machine)
    {
        machine.transform.Translate(machine.player.face * (machine.player.speed * Time.deltaTime + machine.player.accel),0f,0f);
        Chronological(machine);
        if (machine.player.isGrounded) machine.SwitchState(machine.land);
    }

    public override void ExitState(CharaStateManager machine)
    {
        
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        
    }
    

    void Chronological(CharaStateManager machine)
    {
        if (_secs > 0f)
        {
            _secs -= Time.deltaTime;
            Debug.Log(_secs);
        }
        else
        {
            if (machine.player.isBoosting)
            {
                machine.SwitchState(machine.boost);
            }
            else
            { 
               machine.SwitchState(machine.player.isGrounded?(CharaBaseState) machine.land:machine.suspended);
            }
        }
    }
}
