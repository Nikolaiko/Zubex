using UnityEngine;

public class MachineGunBullet : BasicBullet
{
    private static float SPEED = 15.0f;
    private static int DAMAGE = 10;

    public override void Update() {
        base.Update();
        gameObject.transform.Translate(Vector3.right * SPEED * Time.deltaTime);
    }

    protected override void onCollideWithObject(GameObject collidedObject)
    {
        BaseEnemy enemyObject = collidedObject.GetComponent<BaseEnemy>();
        if (enemyObject != null) {
            enemyObject.applyDamage(DAMAGE);
            Destroy(gameObject);
        }
    }
}
