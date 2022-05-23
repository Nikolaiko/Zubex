using UnityEngine;

public class RailGun : BasicWeapon
{
    private GameObject parent;
    private GameObject ammoInstance;
    private float lastTimeShot = 0.0f;
    private float shotPeriod = 5.0f;

    public RailGun(GameObject parentGameObject, GameObject ammo) {
        weaponType = WeaponType.RAIL_GUN;
        parent = parentGameObject;
        ammoInstance = ammo;
    }

    public override void prepareWeapon() {}

    public override void updateWeapon(float deltaTime) {
        lastTimeShot += Time.deltaTime;
        if (lastTimeShot >= shotPeriod) {
            lastTimeShot = 0.0f;
            if (isWeaponActive()) {
                //GameObject.Instantiate(ammoInstance, parent.transform.position, new Quaternion());
            }
        }        
    }
}
