using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class Patrol : state<AI> 
{
    private static Patrol instance;

    private Patrol()
    {
        if(instance != null)
            return;
        
        instance = this;
    }

    public static Patrol Instance
    {
        get
        {
            if(instance == null)
                new Patrol();

            return instance;
        }
    }

    public override void EnterState(AI owner)
    {
        owner.gameObject.GetComponent<Renderer>().material.color = Color.white;
        if(owner.transform.localScale.x > 0)
        {
            owner.speed = Mathf.Abs(owner.speed);
        }
        else if(owner.transform.localScale.x < 0)
        {
            owner.speed = -Mathf.Abs(owner.speed);
        }
    }

    public override void ExitState(AI owner)
    {

    }

    public override void UpdateState(AI owner)
    {
        owner.pRay = Physics2D.Raycast(owner.laserPos.position,owner.rayDirection,owner.pDistance);
    }

    public override void FixedUpdateState(AI owner)
    {
        owner.rb.velocity = new Vector2(owner.speed,owner.rb.velocity.y);

        if(owner.pRay.collider != null)
        {
            if(owner.pRay.collider.CompareTag(owner.wallTag) == true)
            {
                owner.speed *= -1.0f;
                owner.transform.localScale = new Vector2(-owner.transform.localScale.x,owner.transform.localScale.y);
                owner.rayDirection = new Vector2(owner.rayDirection.x * -1.0f,owner.rayDirection.y);
            }
        }

    }

}
