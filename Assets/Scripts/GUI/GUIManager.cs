using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public GUIPanel guiPanel;

    public void activeWeaponChange(WeaponType newType)
    {        
        guiPanel.activeWeaponChange(newType);
    }

    public void setLiveCount(int count)
    {
        guiPanel.setLivesCount(count);
    }
}
