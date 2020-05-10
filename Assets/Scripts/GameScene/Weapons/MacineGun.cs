using UnityEngine;

public class MacineGun : BasicWeapon {
    private GameObject parent;
    private GameObject ammoInstance;
    private float lastTimeShot = 0.0f;
    private float shotPeriod = 5.0f;

    public MacineGun(GameObject parentGameObject, GameObject ammo) {
        weaponType = WeaponType.MACHINE_GUN;
        parent = parentGameObject;
        ammoInstance = ammo;
    }

    public override void prepareWeapon() {
        lastTimeShot = shotPeriod;
    }

    public override void updateWeapon(float deltaTime) {
        lastTimeShot += Time.deltaTime;
        if (lastTimeShot >= shotPeriod) {
            lastTimeShot = 0.0f;
            if (isWeaponActive()) {
                GameObject.Instantiate(ammoInstance, parent.transform.position, new Quaternion());
            }            
        }        
    }
}
