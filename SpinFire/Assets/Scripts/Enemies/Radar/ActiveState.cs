using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveState : RadBaseState
{
    public override void EnterState(RadStateMachine machine)
    {
        machine.anima.Play("Rad_Activate");
    }

    public override void UpdateState(RadStateMachine machine)
    {
        
    }

    public override void ExitState(RadStateMachine machine)
    {
        
    }

    public override void OnTriggerEnter2D(RadStateMachine machine, Collider2D other)
    {
        
    }

    public override void OnTriggerExit2D(RadStateMachine machine, Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            machine.SwitchState(machine.inactiveState);
        }
    }
}
