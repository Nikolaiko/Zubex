using UnityEngine;

public class Meteor : BaseEnemy
{
    private const float METEOR_SPEED = 27.0f;
    private const string START_FLY_METHOD_NAME = "startFly";


    public Animator animator;

    private bool isFlying = false;    

    public void fly()
    {
        Invoke(START_FLY_METHOD_NAME, Random.Range(0.1f, 2.5f));
    }
    
    override public void Update()
    {
        base.Update();
        if (isFlying) {
            gameObject.transform.Translate(Vector3.left * METEOR_SPEED * Time.deltaTime);
        }
    }

    private void startFly()
    {
        isFlying = true;
        animator.enabled = true;
    }
}
