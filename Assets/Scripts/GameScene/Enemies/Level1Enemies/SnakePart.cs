using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePart : BaseEnemy
{
    private const float SNAKE_PART_SPEED = 3.0f;
    private const string START_MOVING_NAME = "startMoving";

    Vector3 direction = Vector3.right;
    private float snakeAbsPath = 0.0f;
    private bool isMoving = false;

    public void startMovingRepeatly(float pathPart, float delay = 0.0f)
    {
        snakeAbsPath = pathPart;        
        Invoke(START_MOVING_NAME, delay);
    }

    override public void Update()
    {
        base.Update();
        if (isMoving) {
            Vector3 currentLocal = transform.parent.InverseTransformPoint(transform.position);            
            if (currentLocal.x > snakeAbsPath) {                                
                direction = Vector3.left;
            } else if (currentLocal.x < -snakeAbsPath) {
                direction = Vector3.right;
            }
            gameObject.transform.Translate(direction * SNAKE_PART_SPEED * Time.deltaTime);
        }        
    }

    private void startMoving()
    {
        isMoving = true;
    }
}
