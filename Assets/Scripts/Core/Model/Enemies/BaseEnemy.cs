using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BaseEnemy : MonoBehaviour, BasicGameObject
{
    public delegate void EnemyDieEventHandler(BaseEnemy enemy);
    public event EnemyDieEventHandler EnemyDieEvent;

    public BoxCollider2D enemyCollider;

    public void activate()
    {
        gameObject.SetActive(true);
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }

    public Vector2 getSize()
    {
        return enemyCollider.size;
    }

    public virtual void Update()
    {
        if (ScreenHelper.isEnemyOutOfScreen(transform.position)) {
            EnemyDieEvent?.Invoke(this);
        }
    }

    public void destroyEnemy()
    {
        Destroy(gameObject);
    }
}
