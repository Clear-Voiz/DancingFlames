using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpKick_EX : CharaBaseState
{
    private float _secs;
    private GameObject PFX;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("UpKick");
        _secs = 0.4f;

        machine.player.isGrounded = false;
        machine.player._rig.AddForce(Vector2.up * (6f), ForceMode2D.Impulse);
        
        PFX = MonoBehaviour.Instantiate(machine.player.PS, machine.player.transform);
        
        machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[2].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[8];
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        if (!machine.player.wallColl)
        {
            machine.player._rig.velocity = new Vector2((machine.player.speed + machine.player.accel) * machine.player.face, machine.player._rig.velocity.y);
        }
        else
        {
            machine.player._rig.velocity = new Vector2(0f, machine.player._rig.velocity.y);
        }
    }

    public override void UpdateState(CharaStateManager machine)
    {
        //machine.transform.Translate(machine.player.face * (machine.player.speed * Time.deltaTime + machine.player.accel),0f,0f);

        Chronological(machine);
    }

    public override void ExitState(CharaStateManager machine)
    {
        if (PFX)
        {
            MonoBehaviour.Destroy(PFX);
        }
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        if (other.collider.CompareTag("Damager"))
        {
            machine.SwitchState(machine.fall);
        }
    }

    public override void OnDisableState(CharaStateManager machine)
    {
        
    }


    void Chronological(CharaStateManager machine)
    {
        if (_secs > 0f)
        {
            _secs -= Time.deltaTime;
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
