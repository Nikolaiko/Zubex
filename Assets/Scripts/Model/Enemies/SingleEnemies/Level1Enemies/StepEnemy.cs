using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEnemy : BaseEnemy
{
    private const string DELAY_CALL_NAME = "makeStep";        
    private Vector3 direction = Vector3.zero;

    public void startMoving(Vector3 enemyDirection, float delay)
    {
        direction = enemyDirection;
        InvokeRepeating(DELAY_CALL_NAME, delay, 1.0f);
    }    

    private void makeStep()
    {
        Vector3 size = getSize();
        gameObject.transform.position = new Vector3(
            transform.position.x + (size.x * direction.x),
            transform.position.y + (size.y * direction.y),
            transform.position.z
        );
    }
}
