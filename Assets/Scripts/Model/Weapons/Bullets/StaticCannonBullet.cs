using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCannonBullet : BasicBullet
{
    private static float SPEED = 7.0f;
    private static int DAMAGE = 10;

    public override void Update()
    {
        base.Update();
        gameObject.transform.Translate(Vector3.left * SPEED * Time.deltaTime);
    }

    protected override void onCollideWithObject(GameObject collidedObject)
    {
        ZubexGameCharacter heroObject = collidedObject.GetComponent<ZubexGameCharacter>();
        if (heroObject != null) {
            heroObject.applyDamage(DAMAGE);
            Destroy(gameObject);
        }
    }
}
