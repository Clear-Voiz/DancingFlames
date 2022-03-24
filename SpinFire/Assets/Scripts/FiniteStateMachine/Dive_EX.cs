using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dive_EX : CharaBaseState
{
    private GameObject meteo;
    public override void EnterState(CharaStateManager machine)
    {
        machine.player.anima.Play("Dive");
        machine.player._rig.velocity = Vector2.zero;
        machine.player._rig.AddForce(Vector2.down * 6f, ForceMode2D.Impulse);
        
        machine.player.centerActions.arrowRenderers[0].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[1].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[2].sprite = machine.player.centerActions.options[8];
        machine.player.centerActions.arrowRenderers[3].sprite = machine.player.centerActions.options[8];
        meteo = MonoBehaviour.Instantiate(machine.player.MeteorFX, machine.transform);
    }

    public override void FixedUpdateState(CharaStateManager machine)
    {
        
    }

    public override void UpdateState(CharaStateManager machine)
    {
        if (machine.player.isGrounded) machine.SwitchState(machine.land);
        if (machine.player.isBoosting) machine.SwitchState(machine.boost);
    }

    public override void ExitState(CharaStateManager machine)
    {
        if (meteo != null)
        {
            MonoBehaviour.Destroy(meteo);
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
}
