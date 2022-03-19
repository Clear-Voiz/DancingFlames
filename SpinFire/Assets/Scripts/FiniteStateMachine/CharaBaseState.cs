using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharaBaseState
{
    public abstract void EnterState(CharaStateManager machine);
    public abstract void FixedUpdateState(CharaStateManager machine);
    public abstract void UpdateState(CharaStateManager machine);
    public abstract void ExitState(CharaStateManager machine);
    public abstract void OnCollisionEnter(CharaStateManager machine, Collision2D other);
    public abstract void OnDisableState(CharaStateManager machine);



}
