using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveState : RadBaseState
{
    public float secs = 0.5f;
    public override void EnterState(RadStateMachine machine)
    {
        machine.anima.Play("Rad_Activate");
    }

    public override void UpdateState(RadStateMachine machine)
    {
        Chronological(machine);
    }

    public override void ExitState(RadStateMachine machine)
    {
        secs = 0.4f;
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

    public void Chronological(RadStateMachine machine)
    {
        if (secs > 0f)
        {
            secs -= 1f*Time.deltaTime;
        }
        else
        {
            float xScale = (machine.transform.rotation.z == 0f)?((machine.eneStats.chara.player.transform.position.x-machine.transform.position.x>0f)?1f:-1f):((machine.eneStats.chara.player.transform.position.y-machine.transform.position.y>0f)?1f:-1f);
            var scaleFactor = new Vector3(xScale,1f,1f);
            GameObject shot;
            if (machine.transform.rotation.z == 0f)
            {
                shot = MonoBehaviour.Instantiate(machine.bullet,
                    new Vector3(machine._mainCol.bounds.center.x + machine._mainCol.bounds.extents.x * xScale,
                        machine._mainCol.bounds.center.y - 0.15f, machine.transform.position.z),
                    machine.transform.rotation);
            }
            else
            {
                shot = MonoBehaviour.Instantiate(machine.bullet,
                    new Vector3(machine._mainCol.bounds.center.x+0.15f, machine._mainCol.bounds.center.y + machine._mainCol.bounds.extents.y * xScale,
                        machine.transform.position.z),
                    machine.transform.rotation);
            }
            shot.transform.localScale = scaleFactor;
            ShotMov SM = shot.GetComponent<ShotMov>();
            SM._rad = machine;
            
            secs = 3f;
        }
    }
}
