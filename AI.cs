using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class AI : MonoBehaviour
{
    public StateMachine<AI> stateMachine {get;set;}

    //****************************************************/    components and valuse on the enemy object
    [HideInInspector] public Rigidbody2D rb;
    public Transform laserPos;
    public float speed;
    public string wallTag;
    public string playerTag;
    /***************************************************/
    //**************************************************/      values for statemachine process
    [HideInInspector] public RaycastHit2D ray;
    public float distance = 5.0f;
    [HideInInspector] public Vector2 rayDirection;
    /***************************************************/
    //**************************************************/      values for patrol state
    public float pDistance = 0.2f;
    [HideInInspector] public RaycastHit2D pRay;
    /***************************************************/
    //**************************************************/      values for fight state
    [HideInInspector] public RaycastHit2D fRay;
    public float fDistance;
    public float fightSpeed;
    [HideInInspector] public Transform player;
    /**************************************************/

    private void Start()
    {   
        rayDirection = Vector2.right;
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine<AI>(this);
        stateMachine.changeState(Patrol.Instance);
    }

    private void Update()
    {   
        colliderCheck();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    private void colliderCheck()
    {
        ray = Physics2D.Raycast(laserPos.position,rayDirection,distance);

        if(ray.collider == null)
            return;

        if(ray.collider.CompareTag(playerTag) == true)
        {
            player = ray.collider.transform;
            stateMachine.changeState(Fight.Instance);
        }
        else if(ray.collider.CompareTag(playerTag) == false && stateMachine.currentState == Fight.Instance)
        {
            player = null;
            stateMachine.changeState(Patrol.Instance);
        }
    }
}
