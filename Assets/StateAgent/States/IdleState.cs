using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private float timer;

    public IdleState(StateAgent owner) : base(owner)
    {
    }

    public override void OnEnter()
    {
        owner.movement.Stop();
        timer = Random.Range(2, 3);
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            owner.stateMachine.StartState(nameof(PatrolState));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
