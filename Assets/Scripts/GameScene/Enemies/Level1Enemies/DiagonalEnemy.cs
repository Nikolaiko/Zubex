using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalEnemy : BaseEnemy
{
    private const string DELAY_CALL_NAME = "launchElement";
    private const float SPEED = 14.0f;

    private bool isMoving = false;
    private Vector3 direction = Vector3.zero;

    public void startMoving(Vector3 enemyDirection, float delay)
    {
        direction = enemyDirection;
        Invoke(DELAY_CALL_NAME, delay);
    }

    override public void Update()
    {
        base.Update();
        if (isMoving) {
            gameObject.transform.Translate(direction * SPEED * Time.deltaTime);
        }
    }

    private void launchElement()
    {
        isMoving = true;
    }
}
