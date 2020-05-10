using UnityEngine;

public class RailGun : BasicWeapon
{
    private float lastTimeShot = 0.0f;
    private float shotPeriod = 5.0f;

    public RailGun() {
        weaponType = WeaponType.RAIL_GUN;
    }

    public override void prepareWeapon() {
        lastTimeShot = shotPeriod;
    }

    public override void updateWeapon(float deltaTime) {
        lastTimeShot += Time.deltaTime;
        if (lastTimeShot >= shotPeriod) {
            lastTimeShot = 0.0f;
            if (isWeaponActive()) {
                Debug.Log("Rail gun shot");
            }
        }        
    }
}
