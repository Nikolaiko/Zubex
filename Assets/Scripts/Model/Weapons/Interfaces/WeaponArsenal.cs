using UnityEngine;

public interface WeaponArsenal : BasicGameObject
{
    void initArsenal();
    void setWeaponTypeLevel(WeaponType weaponType, int levelValue);
    void upgradeWeapon(WeaponType weaponType);
    void downgradeWeapon(WeaponType weaponType);
    WeaponType prevWeapon();
    WeaponType nextWeapon();
    WeaponType getActiveWeaponType();
}
