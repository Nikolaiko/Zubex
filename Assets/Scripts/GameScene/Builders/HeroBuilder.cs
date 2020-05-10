using System.Collections.Generic;
using UnityEngine;

public class HeroBuilder : MonoBehaviour, SceneHeroBuilder
{
    public GameObject heroPrototype;

    public ZubexGameCharacter buildHero() {
        GameObject newHeroInstance = Instantiate(heroPrototype, Vector3.zero, new Quaternion());
        Dictionary<WeaponType, int> weaponSettings = getUserSavedData();

        ZubexGameCharacter character = newHeroInstance.GetComponent<ZubexGameCharacter>();
        character.applyWeaponSettings(weaponSettings);

        return character;
    }

    private Dictionary<WeaponType, int> getUserSavedData() {
        Dictionary<WeaponType, int> weapons = new Dictionary<WeaponType, int>();
        if (UserSavedData.getInstance().haveSavedData()) {

        } else {
            weapons = buildDefaultWeaponsData();
        }
        return weapons;
    }

    private Dictionary<WeaponType, int> buildDefaultWeaponsData() {
        Dictionary<WeaponType, int> weapons = new Dictionary<WeaponType, int>();
        weapons.Add(WeaponType.MACHINE_GUN, 1);
        return weapons;
    }
}
