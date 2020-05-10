using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunBullet : MonoBehaviour
{
    public Rigidbody2D bulletBody;

    public void Start() {
        bulletBody.velocity = new Vector2(1.0f, 0.0f);
    }
}
