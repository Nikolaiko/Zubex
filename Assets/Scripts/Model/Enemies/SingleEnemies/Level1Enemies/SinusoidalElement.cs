using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalElement : BaseEnemy
{
    private const float VERTICAL_SPEED = 4.0f;
    private const float HORIZONTAL_SPEED = 3.0f;
    private const string DELAY_CALL_NAME = "launchElement";

    private bool isMoving = false;
    private float verticalCoff = 2.5f;
    private float timeSinceStartGame = 0.0f;
    private Vector3 verticalDirection = Vector3.zero;

    public void startMoving(Vector3 direction, float delay, float indexAdd)
    {
        timeSinceStartGame = -indexAdd;
        verticalDirection = direction;
        Invoke(DELAY_CALL_NAME, delay);
    }

    public override void Update()
    {
        base.Update();
        if (isMoving) {
            gameObject.transform.Translate(Vector3.left * HORIZONTAL_SPEED * Time.deltaTime);
            Vector3 mov = new Vector3(
                transform.position.x,
                Mathf.Sin(VERTICAL_SPEED * (Time.time + timeSinceStartGame)) * verticalCoff * verticalDirection.y,
                transform.position.z
            );
            transform.position = mov;
        }
    }

    private void launchElement()
    {
        isMoving = true;
    }
}
