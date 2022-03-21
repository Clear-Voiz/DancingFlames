using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveState : RadBaseState
{
    public override void EnterState(RadStateMachine machine)
    {
        machine.anima.Play("Rad_Inactive");
    }

    public override void UpdateState(RadStateMachine machine)
    {
        
    }

    public override void ExitState(RadStateMachine machine)
    {
        
    }

    public override void OnTriggerEnter2D(RadStateMachine machine, Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            machine.SwitchState(machine.activeState);
        }
    }

    public override void OnTriggerExit2D(RadStateMachine machine, Collider2D other)
    {
        
    }
}
