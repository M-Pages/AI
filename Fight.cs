using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class Fight : state<AI>
{
    private static Fight instance;

    private Fight()
    {
        if(instance != null)
            return;

        instance = this;
    }

    public static Fight Instance
    {
        get
        {
            if(instance == null)
                new Fight();

            return instance;
        }
    }

    public override void EnterState(AI owner)
    {
        owner.rb.velocity = Vector2.zero;
        owner.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public override void ExitState(AI owner)
    {

    }
    
    public override void UpdateState(AI owner)
    {

    }

    public override void FixedUpdateState(AI owner)
    { 
        owner.transform.position = Vector2.MoveTowards(owner.transform.position,owner.player.transform.position,owner.fightSpeed * Time.deltaTime);
        Debug.Log(owner.fightSpeed);
    }

}
