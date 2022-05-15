using UnityEngine;

public abstract class BasicBullet : MonoBehaviour, BasicGameObject
{
    public void activate()
    {
        gameObject.SetActive(true);
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }

    public virtual void Update()
    {
        if (ScreenHelper.isOutOfScreen(transform.position)) {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {        
        onCollideWithObject(collision.gameObject);
    }

    protected abstract void onCollideWithObject(GameObject collidedObject);
}
