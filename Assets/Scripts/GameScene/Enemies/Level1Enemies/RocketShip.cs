using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : BaseEnemy
{
    public delegate void RocketReadyDelegate();
    public event RocketReadyDelegate RocketReadyEvent;
    
    private static float INITIAL_SPEED = 0.02f;
    private static float ATTACKING_SPEED = 25.0f;
    private static float DISTANCE_ACCURACY = 0.001f;
    private static string ATTACK_METHOD_NAME = "startAttack";

    private Vector3 startingDestination = Vector3.zero;
    private Vector3 attackDestination = Vector3.zero;

    private bool movingToStartDestination = false;
    private bool attacking = false;
    private bool rocketReady = false;

    public void goToStartLocation(Vector3 location)
    {
        startingDestination = location;
        movingToStartDestination = true;
    }

    public override void Update()
    {
        base.Update();
        if (movingToStartDestination) {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, startingDestination, INITIAL_SPEED);
            transform.position = newPosition;

            if (Vector3.Distance(transform.position, startingDestination) <= DISTANCE_ACCURACY) {
                transform.position = startingDestination;
                movingToStartDestination = false;
                rocketReady = true;
                RocketReadyEvent?.Invoke();
            }
        }
        if (attacking) {
            gameObject.transform.Translate(Vector3.left * ATTACKING_SPEED * Time.deltaTime);
        }
    }

    public bool isRocketReady()
    {
        return rocketReady;
    }

    public void attack()
    {        
        Invoke(ATTACK_METHOD_NAME, Random.Range(0, 0.7f));
    }

    private void startAttack()
    {
        attacking = true;
    }
}
