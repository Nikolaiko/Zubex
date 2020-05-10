using UnityEngine;

public interface WeaponArsenal : BasicGameObject
{
    void initArsenal();
    void setWeaponTypeLevel(WeaponType weaponType, int levelValue);
    void upgradeWeapon(WeaponType weaponType);
    void downgradeWeapon(WeaponType weaponType);
    void prevWeapon();
    void nextWeapon();
}
