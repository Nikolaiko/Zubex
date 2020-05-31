using UnityEngine;

public class MacineGun : BasicWeapon {
    private GameObject parent;
    private GameObject ammoInstance;
    private float lastTimeShot = 0.0f;
    private float shotPeriod = 1.5f;

    private float burstLastTimeShot = 0.0f;
    private float burstShotPeriod = 0.1f;
    private int burstShotCount = 0;

    public MacineGun(GameObject parentGameObject, GameObject ammo) {
        weaponType = WeaponType.MACHINE_GUN;
        parent = parentGameObject;
        ammoInstance = ammo;
    }

    public override void prepareWeapon() {}

    public override void updateWeapon(float deltaTime) {
        if (burstShotCount > 0) {
            burstLastTimeShot += deltaTime;
            if (burstLastTimeShot >= burstShotPeriod) {
                burstLastTimeShot = 0.0f;
                if (isWeaponActive()) {
                    GameObject.Instantiate(ammoInstance, parent.transform.position, new Quaternion());
                    burstShotCount--;
                }
            }
        }


        lastTimeShot += Time.deltaTime;
        if (lastTimeShot >= shotPeriod) {
            lastTimeShot = 0.0f;
            if (isWeaponActive()) {
                burstLastTimeShot = 0.0f;
                burstShotCount = getLevel() + 1;
                GameObject.Instantiate(ammoInstance, parent.transform.position, new Quaternion());                
            }            
        }                
    }  
}
