using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land_EX : CharaBaseState
{
    private float secs;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Land");
        secs = 0.2f;
        
        machine.player.maxSpeed = 3f;
        machine.player.increment = 0.1f;
        if (machine.player.speed > machine.player.maxSpeed) machine.player.speed = machine.player.maxSpeed;

        machine.player.hasAirdodged = false;
        
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
        
    }

    public override void UpdateState(CharaStateManager machine)
    {
        Chronological(machine);
        if (Input.GetKeyDown(KeyCode.UpArrow)) machine.SwitchState(machine.jump);
        if (Input.GetKeyDown(KeyCode.RightArrow) && machine.player.face == -1f)
        {
            machine.ReverseFace();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && machine.player.face == 1f)
        {
            machine.SwitchState(machine.lenakick);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && machine.player.face == 1f)
        {
            machine.ReverseFace();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && machine.player.face == -1f)
        {
            machine.SwitchState(machine.lenakick);
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (machine.player.face == -1f) machine.SwitchState(machine.lenakick);
            else
            {
                machine.ReverseFace();
            }
        }
    }

    public override void ExitState(CharaStateManager machine)
    {
        
    }

    public override void OnCollisionEnter(CharaStateManager machine, Collision2D other)
    {
        if (other.collider.CompareTag("Damager"))
        {
            machine.SwitchState(machine.collapse);
        }
    }

    public override void OnDisableState(CharaStateManager machine)
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
            if (!machine.player.wallColl)
            {
                machine.SwitchState(machine._forwards);
            }
            else
            {
                machine.SwitchState(machine.stand);
            }
        }
    }
}
