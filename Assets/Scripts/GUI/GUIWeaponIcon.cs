using UnityEngine;

public class GUIWeaponIcon : MonoBehaviour
{
    public WeaponType weaponType = WeaponType.NOT_SET;
    
    public void setActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public bool getActive()
    {
        return gameObject.activeSelf;
    }
}
