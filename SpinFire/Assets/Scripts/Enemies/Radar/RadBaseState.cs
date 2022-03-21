using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RadBaseState
{
   public abstract void EnterState(RadStateMachine machine);

   public abstract void UpdateState(RadStateMachine machine);

   public abstract void ExitState(RadStateMachine machine);

   public abstract void OnTriggerEnter2D(RadStateMachine machine, Collider2D other);

   public abstract void OnTriggerExit2D(RadStateMachine machine, Collider2D other);
}
