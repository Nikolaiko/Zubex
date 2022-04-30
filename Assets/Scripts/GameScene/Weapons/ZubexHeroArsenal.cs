using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZubexHeroArsenal : MonoBehaviour, WeaponArsenal
{
    public GameObject machineGunBulletInstance;
    public GameObject railGunBulletInstance;

    private int cycleTotalCount = 0;

    private float shotPeriod = 0.3f;
    private float lastTimeShot = 0.0f;

    private bool isActivated = false;
    private List<BasicWeapon> weaponsArsenal = new List<BasicWeapon>();
    private int activeWeaponIndex;
 
    public void activate() {
        isActivated = true;
        weaponsArsenal[activeWeaponIndex].activate();
    }

    public void deactivate() {
        isActivated = false;
        foreach (BasicWeapon currentWeapon in weaponsArsenal) {
            currentWeapon.deactivate();
        }
    }

    public void initArsenal() {
        weaponsArsenal.Clear();
        weaponsArsenal.Add(new MacineGun(gameObject, machineGunBulletInstance));
        weaponsArsenal.Add(new RailGun(gameObject, railGunBulletInstance));

        lastTimeShot = 0.0f;
        cycleTotalCount = weaponsArsenal.Count * 2;

        weaponsArsenal[activeWeaponIndex].prepareWeapon();
    }

    public void upgradeWeapon(WeaponType weaponType) {        
        foreach(BasicWeapon currentWeapon in weaponsArsenal) {
            if (currentWeapon.getType() == weaponType) {
                currentWeapon.levelUp();
                break;
            }
        }
    }

    public void downgradeWeapon(WeaponType weaponType) {
        foreach (BasicWeapon currentWeapon in weaponsArsenal) {
            if (currentWeapon.getType() == weaponType) {
                currentWeapon.levelDown();
                break;
            }
        }
    }

    public void setWeaponTypeLevel(WeaponType weaponType, int levelValue) {
        foreach (BasicWeapon currentWeapon in weaponsArsenal) {
            if (currentWeapon.getType() == weaponType) {
                currentWeapon.setLevel(levelValue);
                break;
            }
        }
    }

    public WeaponType nextWeapon() {
        weaponsArsenal[activeWeaponIndex].deactivate();

        BasicWeapon findedWeapon = null;
        WeaponType findedWeaponType = WeaponType.NOT_SET;
        int cycleCount = 0;

        while (cycleCount < cycleTotalCount) {
            cycleCount++;
            activeWeaponIndex++;
            if (activeWeaponIndex == weaponsArsenal.Count) {
                activeWeaponIndex = 0;
            }            
            if (weaponsArsenal[activeWeaponIndex].getLevel() > 0) {
                findedWeapon = weaponsArsenal[activeWeaponIndex];
                break;
            }
        }

        if (findedWeapon != null) {
            findedWeapon.activate();
            findedWeapon.prepareWeapon();
            findedWeaponType = findedWeapon.getType();
        } else {
            //Error Loger
        }
        return findedWeaponType;
        
    }

    public WeaponType prevWeapon() {
        weaponsArsenal[activeWeaponIndex].deactivate();

        BasicWeapon findedWeapon = null;
        WeaponType findedWeaponType = WeaponType.NOT_SET;
        int cycleCount = 0;

        while (cycleCount < cycleTotalCount) {
            cycleCount++;
            activeWeaponIndex--;
            if (activeWeaponIndex < 0) {
                activeWeaponIndex = weaponsArsenal.Count - 1;
            }            
            if (weaponsArsenal[activeWeaponIndex].getLevel() > 0) {
                findedWeapon = weaponsArsenal[activeWeaponIndex];
                break;
            }
        }
        if (findedWeapon != null) {
            findedWeapon.activate();
            findedWeapon.prepareWeapon();
            findedWeaponType = findedWeapon.getType();
        } else {
            //Error Loger
        }
        return findedWeaponType;
    }

    public WeaponType getActiveWeaponType()
    {
        return weaponsArsenal[activeWeaponIndex].getType();
    }

    public void Update() {
        weaponsArsenal[activeWeaponIndex].updateWeapon(Time.deltaTime);        
    }   
}
