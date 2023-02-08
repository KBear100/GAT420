using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(StateAgent owner) : base(owner)
    {
    }

    public override void OnEnter()
    {
        owner.movement.Stop();
        owner.timer.value = Random.Range(2, 3);
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
       //
    }
}
