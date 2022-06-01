using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class BaseEnemy : MonoBehaviour, BasicGameObject
{
    public delegate void EnemyDieEventHandler(BaseEnemy enemy);
    public event EnemyDieEventHandler EnemyDieEvent;

    public BoxCollider2D enemyCollider;

    protected bool appearedOnScreen = false;
    protected int health = 0;

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
        return enemyCollider.size * transform.lossyScale;
    }

    public virtual void Update()
    {
        if (ScreenHelper.isOnScreen(transform.position)) {
            appearedOnScreen = true;
        }

        if (ScreenHelper.isEnemyOutOfScreen(transform.position)) {
            EnemyDieEvent?.Invoke(this);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        ZubexGameCharacter heroObject = collision.collider.gameObject.GetComponent<ZubexGameCharacter>();
        if (heroObject != null) {
            heroObject.applyDamage(UtilConsts.OVERPOWERED_DAMAGE);
            EnemyDieEvent?.Invoke(this);
        }
    }

    public void destroyEnemy()
    {
        Destroy(gameObject);
    }

    public void applyDamage(int incomeDamage)
    {
        if (appearedOnScreen) {
            health -= incomeDamage;
            if (health <= 0) {
                EnemyDieEvent?.Invoke(this);
            }
        }        
    }
}
