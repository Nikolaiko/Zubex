using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPanel : MonoBehaviour
{
    public GUIWeaponIcon machineGunIcon;
    public GUIWeaponIcon railGunIcon;

    public Text livesCountText;
    
    private GUIWeaponIcon activeWeapon;

    public void Start()
    {
        machineGunIcon?.setActive(false);
        railGunIcon?.setActive(false);
    }

    public void activeWeaponChange(WeaponType newType)
    {
        activeWeapon?.setActive(false);
        switch(newType) {
            case WeaponType.MACHINE_GUN: {
                machineGunIcon?.setActive(true);
                activeWeapon = machineGunIcon;
                break;

            }
            case WeaponType.RAIL_GUN: {
                railGunIcon?.setActive(true);
                activeWeapon = railGunIcon;
                break;

            }
        }
    }

    public void setLivesCount(int count)
    {
        livesCountText.text = count.ToString();
    }
}
