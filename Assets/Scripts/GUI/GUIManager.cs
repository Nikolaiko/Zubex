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


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
