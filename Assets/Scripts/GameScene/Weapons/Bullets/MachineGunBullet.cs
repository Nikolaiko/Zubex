using UnityEngine;

public class MachineGunBullet : BasicBullet
{
    public Rigidbody2D bulletBody;

    public void Start() {
        bulletBody.velocity = new Vector2(15.0f, 0.0f);
    }
}
